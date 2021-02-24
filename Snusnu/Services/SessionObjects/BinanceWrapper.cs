using Binance.Net;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Objects.Spot;
using Binance.Net.Objects.Spot.MarketData;
using Binance.Net.Objects.Spot.UserStream;
using Binance.Net.Objects.Spot.WalletData;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using Snusnu.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snusnu.Services.SessionObjects
{
    public class BinanceWrapper
    {
        #region Properties

        private readonly Stopwatch infosInitializeTime = new Stopwatch();

        private Session session;
        private BinanceClient client;
        private BinanceSocketClient socket;
        private string listenKey;
        private bool isLazyInitialized = false;
        private event Action OnLazyInitialized;
        private DateTime serverDateTimeStarted;

        private string ApiKey
        {
            get => session.Datastore.GetValue("apik");
            set => session.Datastore.SetValue("apik", value);
        }

        private string ApiSecret
        {
            get => session.Datastore.GetValue("apis");
            set => session.Datastore.SetValue("apis", value);
        }

        public readonly ObservableCollection<Wallet> Wallets = new ObservableCollection<Wallet>();
        public readonly ObservableCollection<Market> Markets = new ObservableCollection<Market>();

        public DateTime ServerTime => serverDateTimeStarted + infosInitializeTime.Elapsed;
        public bool IsCredentialsReady => !string.IsNullOrEmpty(ApiKey) && !string.IsNullOrEmpty(ApiSecret);
        public bool IsStarted => client != null;

        #endregion

        #region Initializers

        private BinanceWrapper() { }
        public static async Task<BinanceWrapper> Initialize(Session session)
        {
            return await Task.Run(delegate
            {
                var wrapper = new BinanceWrapper() { session = session };
                wrapper.LazyInitialize();
                return wrapper;
            });
        }

        private async void LazyInitialize()
        {
            await Task.Delay(Defaults.RetrySpan);
            session.Logger.AddLog(new Log(DateTime.Now, "Binance", "Starting . . .", LogType.Info));
            await InitializeInstances();
            await InitializeInfos();
            await InitializeStreamers();
            isLazyInitialized = true;
            OnLazyInitialized?.Invoke();
        }

        private async Task InitializeInstances()
        {
            while (true)
            {
                if (!IsCredentialsReady)
                {
                    session.Logger.AddLog(new Log(DateTime.Now, "Binance", "Credentials not ready", LogType.Error));
                    await Task.Delay(Defaults.RetrySpan);
                    continue;
                }
                var apiCredentials = new ApiCredentials(ApiKey, ApiSecret);
                var clientInstance = new BinanceClient(new BinanceClientOptions { ApiCredentials = apiCredentials });
                var userStream = await clientInstance.Spot.UserStream.StartUserStreamAsync();
                if (!userStream.Success)
                {
                    session.Logger.AddLog(new Log(DateTime.Now, "Binance", "User stream key request failed", LogType.Error));
                    await Task.Delay(Defaults.RetrySpan);
                    continue;
                }
                var streamListenKey = userStream.Data;
                var socketClient = new BinanceSocketClient(new BinanceSocketClientOptions { ApiCredentials = apiCredentials });

                client = clientInstance;
                listenKey = streamListenKey;
                socket = socketClient;

                break;
            }
        }

        private async Task InitializeInfos()
        {
            while (true)
            {
                infosInitializeTime.Restart();

                #region Fetch

                var systemInfo = await client.Spot.System.GetExchangeInfoAsync();
                if (!systemInfo.Success)
                {
                    session.Logger.AddLog(new Log(DateTime.Now, "Binance", "Server system exchange info failed", LogType.Error));
                    await Task.Delay(Defaults.RetrySpan);
                    continue;
                }
                var accountInfo = await client.General.GetAccountInfoAsync();
                if (!accountInfo.Success)
                {
                    session.Logger.AddLog(new Log(DateTime.Now, "Binance", "Server system fetch account info failed", LogType.Error));
                    await Task.Delay(Defaults.RetrySpan);
                    continue;
                }
                var wallets = await client.General.GetUserCoinsAsync();
                if (!wallets.Success)
                {
                    session.Logger.AddLog(new Log(DateTime.Now, "Binance", "Server system fetch wallet failed", LogType.Error));
                    await Task.Delay(Defaults.RetrySpan);
                    continue;
                }
                var tradeFees = await client.Spot.Market.GetTradeFeeAsync();
                if (!tradeFees.Success)
                {
                    session.Logger.AddLog(new Log(DateTime.Now, "Binance", "Server system fetch trade fees failed", LogType.Error));
                    await Task.Delay(Defaults.RetrySpan);
                    continue;
                }
                var prices = await client.Spot.Market.GetAllPricesAsync();
                if (!prices.Success)
                {
                    session.Logger.AddLog(new Log(DateTime.Now, "Binance", "Server system fetch prices failed", LogType.Error));
                    await Task.Delay(Defaults.RetrySpan);
                    continue;
                }

                #endregion

                #region DerivePrimitive

                serverDateTimeStarted = systemInfo.Data.ServerTime;

                foreach (var coin in wallets.Data)
                {
                    var existing = Wallets.FirstOrDefault(i => i.Code.Equals(coin.Coin));
                    if (existing == null)
                    {
                        existing = new Wallet();
                        Wallets.Add(existing);
                    }
                    existing.Session = session;
                    existing.Name = coin.Name;
                    existing.Code = coin.Coin;
                    existing.Balance = coin.Free;
                    existing.LastUpdated = DateTime.UtcNow;
                    existing.NotifyUpdate();
                }
                foreach (var wallet in new List<Wallet>(Wallets))
                {
                    if (!wallets.Data.Any(i => i.Coin.Equals(wallet.Code)))
                    {
                        Wallets.Remove(wallet);
                    }
                }

                foreach (var symbol in systemInfo.Data.Symbols)
                {
                    if (!symbol.OrderTypes.Any(i => i == OrderType.Limit || i == OrderType.Market)) continue;
                    var tradeFee = tradeFees.Data.FirstOrDefault(i => i.Symbol.Equals(symbol.Name));
                    if (tradeFee == null) continue;
                    var price = prices.Data.FirstOrDefault(i => i.Symbol.Equals(symbol.Name));
                    if (price == null) continue;
                    var baseWallet = Wallets.FirstOrDefault(i => i.Code.Equals(symbol.BaseAsset));
                    if (baseWallet == null) continue;
                    var quoteWallet = Wallets.FirstOrDefault(i => i.Code.Equals(symbol.QuoteAsset));
                    if (quoteWallet == null) continue;
                    var existing = Markets.FirstOrDefault(i => i.Symbol.Equals(symbol.Name));
                    if (existing == null)
                    {
                        existing = new Market();
                        Markets.Add(existing);
                    }
                    existing.Session = session;
                    existing.Symbol = symbol.Name;
                    existing.MinTradeAmount = symbol.LotSizeFilter.MinQuantity;
                    existing.MinPriceMovement = symbol.PriceFilter.MinPrice;
                    existing.MinOrderSize = symbol.MinNotionalFilter.MinNotional;
                    existing.OrderTypes = symbol.OrderTypes;
                    existing.TakerFeePercentage = tradeFee.TakerFee * 100;
                    existing.MakerFeePercentage = tradeFee.MakerFee * 100;
                    existing.Price = price.Price;
                    existing.LastUpdated = ServerTime;

                    baseWallet.Markets.Add(existing);
                    quoteWallet.Markets.Add(existing);
                    existing.BaseWallet = baseWallet;
                    existing.QuoteWallet = quoteWallet;
                    existing.NotifyUpdate();
                    baseWallet.NotifyUpdate();
                    quoteWallet.NotifyUpdate();
                }
                foreach (var market in new List<Market>(Markets))
                {
                    if (!systemInfo.Data.Symbols.Any(i => i.Name.Equals(market.Symbol)))
                    {
                        Markets.Remove(market);
                    }
                }

                var btc = Wallets.FirstOrDefault(i => i.Code.Equals("BTC"));
                var tree = btc.GetWalletTree();
                var s = 1;

                #endregion

                break;
            }
        }

        private async Task InitializeStreamers()
        {
            while (true)
            {
                var socketStream = socket.Spot.SubscribeToUserDataUpdates(listenKey, AccountInfoUpdate, OrderUpdate, OrderListUpdate, PositionsUpdate, BalanceUpdate);
                if (!socketStream.Success)
                {
                    session.Logger.AddLog(new Log(DateTime.Now, "Binance", "User update stream subscribe failed", LogType.Error));
                    await Task.Delay(Defaults.RetrySpan);
                    continue;
                }
                socketStream.Data.ConnectionLost += delegate { session.Logger.AddLog(new Log(DateTime.Now, "Binance", "Server connection lost", LogType.Info)); };
                socketStream.Data.ConnectionRestored += delegate { session.Logger.AddLog(new Log(DateTime.Now, "Binance", "Server connection restored", LogType.Info)); };
                socketStream.Data.ActivityPaused += delegate { session.Logger.AddLog(new Log(DateTime.Now, "Binance", "Server activity paused", LogType.Info)); };
                socketStream.Data.ActivityUnpaused += delegate { session.Logger.AddLog(new Log(DateTime.Now, "Binance", "Server activity unpaused", LogType.Info)); };
                session.Logger.AddLog(new Log(DateTime.Now, "Binance", "Ready", LogType.Info));
                StartRegularRefresher();
                break;
            }
        }

        #endregion

        #region RegularUpdater

        private void StartRegularRefresher()
        {
            Task.Run(async delegate
            {
                while (true)
                {
                    await Task.Delay(Defaults.RefreshSpan);
                    await InitializeInfos();
                }
            });
            Task.Run(async delegate
            {
                while (true)
                {
                    await client.Spot.UserStream.KeepAliveUserStreamAsync(listenKey);
                    await Task.Delay(TimeSpan.FromMinutes(30));
                }
            });
        }

        #endregion

        #region UserUpdater

        private void AccountInfoUpdate(BinanceStreamAccountInfo accountInfo)
        {
            foreach (var balance in accountInfo.Balances)
            {
                var wallet = Wallets.FirstOrDefault(i => i.Code.ToUpper().Equals(balance.Asset.ToUpper()));
                if (wallet != null)
                {
                    wallet.Balance = balance.Free;
                    wallet.LastUpdated = DateTime.UtcNow;
                    wallet.NotifyUpdate();
                }
            }
        }

        private void OrderUpdate(BinanceStreamOrderUpdate orderUpdate)
        {

        }

        private void OrderListUpdate(BinanceStreamOrderList orderListUpdate)
        {

        }

        private void PositionsUpdate(BinanceStreamPositionsUpdate positionsUpdate)
        {
            foreach (var balance in positionsUpdate.Balances)
            {
                var wallet = Wallets.FirstOrDefault(i => i.Code.ToUpper().Equals(balance.Asset.ToUpper()));
                if (wallet != null)
                {
                    wallet.Balance = balance.Free;
                    wallet.LastUpdated = DateTime.UtcNow;
                    wallet.NotifyUpdate();
                }
            }
        }

        private void BalanceUpdate(BinanceStreamBalanceUpdate balanceUpdate)
        {
            var wallet = Wallets.FirstOrDefault(i => i.Code.ToUpper().Equals(balanceUpdate.Asset.ToUpper()));
            if (wallet != null)
            {
                wallet.Balance = balanceUpdate.BalanceDelta;
                wallet.LastUpdated = DateTime.UtcNow;
                wallet.NotifyUpdate();
            }
        }

        #endregion

        #region Helpers



        #endregion

        #region Methods

        public bool TrySetApi(string apiKey, string apiSecret)
        {
            if (IsCredentialsReady) return false;
            ApiKey = apiKey;
            ApiSecret = apiSecret;
            return true;
        }

        public void SetOnLazyInitialized(Action action)
        {
            if (isLazyInitialized) action?.Invoke();
            OnLazyInitialized = action;
        }

        #endregion
    }
}

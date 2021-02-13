using Binance.Net;
using Binance.Net.Interfaces;
using Binance.Net.Objects.Spot;
using Binance.Net.Objects.Spot.MarketData;
using Binance.Net.Objects.Spot.UserStream;
using Binance.Net.Objects.Spot.WalletData;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using Snusnu.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snusnu.Services.SessionObjects
{
    public class BinanceWrapper
    {
        #region Properties

        private Session session;
        private BinanceClient client;
        private BinanceSocketClient socket;
        private string listenKey;

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
            while (true)
            {

                var socketStream = socket.Spot.SubscribeToUserDataUpdates(listenKey, AccountInfoUpdate, OrderUpdate, OrderListUpdate, PositionsUpdate, BalanceUpdate);
                if (!socketStream.Success)
                {
                    session.Logger.AddLog(new Log(DateTime.Now, "Binance", "User update stream subscribe failed", LogType.Error));
                    await Task.Delay(Defaults.RetrySpan);
                    continue;
                }
                var tickerStream = socket.Spot.SubscribeToAllSymbolTickerUpdates(SymbolTickerUpdates);
                if (!tickerStream.Success)
                {
                    session.Logger.AddLog(new Log(DateTime.Now, "Binance", "Ticker update stream subscribe failed", LogType.Error));
                    await Task.Delay(Defaults.RetrySpan);
                    continue;
                }
                tickerStream.Data.ConnectionLost += delegate { session.Logger.AddLog(new Log(DateTime.Now, "Binance", "Server connection lost", LogType.Info)); };
                tickerStream.Data.ConnectionRestored += delegate { session.Logger.AddLog(new Log(DateTime.Now, "Binance", "Server connection restored", LogType.Info)); };
                tickerStream.Data.ActivityPaused += delegate { session.Logger.AddLog(new Log(DateTime.Now, "Binance", "Server activity paused", LogType.Info)); };
                tickerStream.Data.ActivityUnpaused += delegate { session.Logger.AddLog(new Log(DateTime.Now, "Binance", "Server activity unpaused", LogType.Info)); };
                session.Logger.AddLog(new Log(DateTime.Now, "Binance", "Ready", LogType.Info));
                StartRegularRefresher();
                break;
            }
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

                #endregion

                var derivedWallets = wallets.Data.Select(i => Wallet.FromPrimitive(i)).Where(i => i != null);
                var derivedMarkets = systemInfo.Data.Symbols.Select(i => Market.FromPrimitive(i)).Where(i => i != null);

                #region PushToNotifier

                foreach (var wallet in derivedWallets)
                {
                    var existing = Wallets.FirstOrDefault(i => i.Code.Equals(wallet.Code));
                    if (existing == null)
                    {
                        existing = wallet;
                        Wallets.Add(existing);
                    }
                    else
                    {
                        existing.Update(wallet);
                    }
                    existing.NotifyUpdate();
                }
                foreach (var wallet in new List<Wallet>(Wallets))
                {
                    if (!derivedWallets.Any(i => i.Code.Equals(wallet.Code)))
                    {
                        Wallets.Remove(wallet);
                    }
                }
                foreach (var market in derivedMarkets)
                {
                    var existing = Markets.FirstOrDefault(i => i.Symbol.Equals(market.Symbol));
                    if (existing == null)
                    {
                        existing = market;
                        Markets.Add(existing);
                    }
                    else
                    {
                        existing.Update(market);
                    }
                    existing.NotifyUpdate();
                }
                foreach (var market in new List<Market>(Markets))
                {
                    if (!derivedMarkets.Any(i => i.Symbol.Equals(market.Symbol)))
                    {
                        Markets.Remove(market);
                    }
                }

                #endregion

                break;
            }
        }

        private async Task InitializeStreamers()
        {
            while (true)
            {
                var tickerStream = await socket.Spot.SubscribeToAllSymbolTickerUpdatesAsync(SymbolTickerUpdates);
                if (!tickerStream.Success)
                {
                    session.Logger.AddLog(new Log(DateTime.Now, "Binance", "Server market ticker subscribe failed", LogType.Error));
                    await Task.Delay(Defaults.RetrySpan);
                    continue;
                }
            }
        }

        public void Stop()
        {
            client.Spot.UserStream.StopUserStream(listenKey);
            session.Logger.AddLog(new Log(DateTime.Now, "Binance", "Stream key stopped", LogType.Info));
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

        private void SymbolTickerUpdates(IEnumerable<IBinanceTick> ticks)
        {
            foreach (var tick in ticks)
            {
                var market = Markets.FirstOrDefault(i => i.Symbol.Equals(tick.Symbol));
                if (market != null)
                {
                    market.Tick = tick;
                    market.TickLastUpdated = DateTime.UtcNow;
                    market.NotifyUpdate();
                }
            }
        }

        private void AccountInfoUpdate(BinanceStreamAccountInfo accountInfo)
        {
            foreach (var balance in accountInfo.Balances)
            {
                var wallet = Wallets.FirstOrDefault(i => i.Code.ToUpper().Equals(balance.Asset.ToUpper()));
                if (wallet != null)
                {
                    wallet.Balance = balance.Free;
                    wallet.BalanceLastUpdated = DateTime.UtcNow;
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
                    wallet.BalanceLastUpdated = DateTime.UtcNow;
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
                wallet.BalanceLastUpdated = DateTime.UtcNow;
                wallet.NotifyUpdate();
            }
        }

        #endregion

        #region Methods

        public bool TrySetApi(string apiKey, string apiSecret)
        {
            if (IsCredentialsReady) return false;
            ApiKey = apiKey;
            ApiSecret = apiSecret;
            return true;
        }

        private async Task Refresh()
        {

        }

        #endregion
        //public class BinanceSymbol : ICommonSymbol
        //{
        //    public BinanceSymbol();

        //    //
        //    // Summary:
        //    //     Filter for the max accuracy of the price for this symbol
        //    [JsonIgnore]
        //    public BinanceSymbolPriceFilter? PriceFilter { get; }
        //    //
        //    // Summary:
        //    //     Filter for the minimal size of an order for this symbol
        //    [JsonIgnore]
        //    public BinanceSymbolMinNotionalFilter? MinNotionalFilter { get; }
        //    //
        //    // Summary:
        //    //     Filter for max algorithmic orders for this symbol
        //    [JsonIgnore]
        //    public BinanceSymbolMaxAlgorithmicOrdersFilter? MaxAlgorithmicOrdersFilter { get; }
        //    //
        //    // Summary:
        //    //     Filter for max number of orders for this symbol
        //    [JsonIgnore]
        //    public BinanceSymbolMaxOrdersFilter? MaxOrdersFilter { get; }
        //    //
        //    // Summary:
        //    //     Filter for max accuracy of the quantity for this symbol, specifically for market
        //    //     orders
        //    [JsonIgnore]
        //    public BinanceSymbolMarketLotSizeFilter? MarketLotSizeFilter { get; }
        //    //
        //    // Summary:
        //    //     Filter for max accuracy of the quantity for this symbol
        //    [JsonIgnore]
        //    public BinanceSymbolLotSizeFilter? LotSizeFilter { get; }
        //    //
        //    // Summary:
        //    //     Filter for max amount of iceberg parts for this symbol
        //    [JsonIgnore]
        //    public BinanceSymbolIcebergPartsFilter? IceBergPartsFilter { get; }
        //    //
        //    // Summary:
        //    //     Filters for order on this symbol
        //    public IEnumerable<BinanceSymbolFilter> Filters { get; set; }
        //    //
        //    // Summary:
        //    //     Permissions types
        //    [JsonProperty(ItemConverterType = typeof(AccountTypeConverter))]
        //    public IEnumerable<AccountType> Permissions { get; set; }
        //    //
        //    // Summary:
        //    //     The precision of the quote asset commission
        //    public int QuoteCommissionPrecision { get; set; }
        //    //
        //    // Summary:
        //    //     The precision of the base asset commission
        //    public int BaseCommissionPrecision { get; set; }
        //    //
        //    // Summary:
        //    //     Whether or not it is allowed to specify the quantity of a market order in the
        //    //     quote asset
        //    [JsonProperty("quoteOrderQtyMarketAllowed")]
        //    public bool QuoteOrderQuantityMarketAllowed { get; set; }
        //    //
        //    // Summary:
        //    //     If OCO(One Cancels Other) orders are allowed
        //    public bool OCOAllowed { get; set; }
        //    //
        //    // Summary:
        //    //     Margin trading orders allowed
        //    public bool IsMarginTradingAllowed { get; set; }
        //    //
        //    // Summary:
        //    //     Spot trading orders allowed
        //    public bool IsSpotTradingAllowed { get; set; }
        //    //
        //    // Summary:
        //    //     Ice berg orders allowed
        //    public bool IceBergAllowed { get; set; }
        //    //
        //    // Summary:
        //    //     Allowed order types
        //    [JsonProperty(ItemConverterType = typeof(OrderTypeConverter))]
        //    public IEnumerable<OrderType> OrderTypes { get; set; }
        //    //
        //    // Summary:
        //    //     The precision of the quote asset
        //    [JsonProperty("quotePrecision")]
        //    public int QuoteAssetPrecision { get; set; }
        //    //
        //    // Summary:
        //    //     The quote asset
        //    public string QuoteAsset { get; set; }
        //    //
        //    // Summary:
        //    //     The precision of the base asset
        //    public int BaseAssetPrecision { get; set; }
        //    //
        //    // Summary:
        //    //     The base asset
        //    public string BaseAsset { get; set; }
        //    //
        //    // Summary:
        //    //     The status of the symbol
        //    [JsonConverter(typeof(SymbolStatusConverter))]
        //    public SymbolStatus Status { get; set; }
        //    //
        //    // Summary:
        //    //     The symbol
        //    [JsonProperty("symbol")]
        //    public string Name { get; set; }
        //    //
        //    // Summary:
        //    //     Filter for the maximum deviation of the price
        //    [JsonIgnore]
        //    public BinanceSymbolPercentPriceFilter? PricePercentFilter { get; }
        //    //
        //    // Summary:
        //    //     Filter for the maximum position on a symbol
        //    [JsonIgnore]
        //    public BinanceSymbolMaxPositionFilter? MaxPositionFilter { get; }
        //}
    }
}

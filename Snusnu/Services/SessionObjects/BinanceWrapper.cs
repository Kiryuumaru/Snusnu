using Binance.Net;
using Binance.Net.Objects.Spot;
using Binance.Net.Objects.Spot.MarketData;
using Binance.Net.Objects.Spot.UserStream;
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
        private ApiCredentials credentials;
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
                    session.Logger.AddLog(new Log(DateTime.Now, "Binance", "Stream key request failed", LogType.Error));
                    await Task.Delay(Defaults.RetrySpan);
                    continue;
                }
                var streamListenKey = userStream.Data;
                var socketClient = new BinanceSocketClient(new BinanceSocketClientOptions { ApiCredentials = apiCredentials });
                var socketStream = socketClient.Spot.SubscribeToUserDataUpdates(streamListenKey, AccountInfoUpdate, OrderUpdate, OrderListUpdate, PositionsUpdate, BalanceUpdate);
                if (!socketStream.Success)
                {
                    session.Logger.AddLog(new Log(DateTime.Now, "Binance", "User update stream subscribe failed", LogType.Error));
                    await Task.Delay(Defaults.RetrySpan);
                    continue;
                }
                socketStream.Data.ConnectionLost += delegate
                {
                    session.Logger.AddLog(new Log(DateTime.Now, "Binance", "Server connection lost", LogType.Info));
                };
                socketStream.Data.ConnectionRestored += delegate
                {
                    session.Logger.AddLog(new Log(DateTime.Now, "Binance", "Server connection restored", LogType.Info));
                };
                socketStream.Data.ActivityPaused += delegate
                {
                    session.Logger.AddLog(new Log(DateTime.Now, "Binance", "Server activity paused", LogType.Info));
                };
                socketStream.Data.ActivityUnpaused += delegate
                {
                    session.Logger.AddLog(new Log(DateTime.Now, "Binance", "Server activity unpaused", LogType.Info));
                };
                credentials = apiCredentials;
                client = clientInstance;
                listenKey = streamListenKey;
                socket = socketClient;
                session.Logger.AddLog(new Log(DateTime.Now, "Binance", "Ready", LogType.Info));
                StartRegularRefresher();
                break;
            }
        }

        public void Stop()
        {
            client.Spot.UserStream.StopUserStream(listenKey);
            session.Logger.AddLog(new Log(DateTime.Now, "Binance", "Stream key stopped", LogType.Info));
        }

        #endregion

        #region Updaters

        private void StartRegularRefresher()
        {
            Task.Run(async delegate
            {
                while (true)
                {
                    var systemInfo = await client.Spot.System.GetExchangeInfoAsync();
                    if (!systemInfo.Success)
                    {
                        session.Logger.AddLog(new Log(DateTime.Now, "Binance", "Server system exchange info failed", LogType.Error));
                        await Task.Delay(Defaults.RetrySpan);
                        continue;
                    }
                    var allPrices = await client.Spot.Market.GetAllPricesAsync();
                    if (!allPrices.Success)
                    {
                        session.Logger.AddLog(new Log(DateTime.Now, "Binance", "Server get markets failed", LogType.Error));
                        await Task.Delay(Defaults.RetrySpan);
                        continue;
                    }
                    List<(BinanceSymbol Symbol, BinancePrice Price)> symbols = new List<(BinanceSymbol Symbol, BinancePrice Price)>();
                    foreach (var symbol in systemInfo.Data.Symbols)
                    {
                        var price = allPrices.Data.FirstOrDefault(i => i.Symbol.Equals(symbol.Name));
                        if (price != null)
                        {
                            symbols.Add((symbol, price));
                        }
                    }
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

        private void AccountInfoUpdate(BinanceStreamAccountInfo accountInfo)
        {

        }

        private void OrderUpdate(BinanceStreamOrderUpdate orderUpdate)
        {

        }

        private void OrderListUpdate(BinanceStreamOrderList orderListUpdate)
        {

        }

        private void PositionsUpdate(BinanceStreamPositionsUpdate positionsUpdate)
        {

        }

        private void BalanceUpdate(BinanceStreamBalanceUpdate balanceUpdate)
        {

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
    }
}

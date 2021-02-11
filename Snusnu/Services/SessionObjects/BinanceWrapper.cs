using Binance.Net;
using Binance.Net.Objects.Spot;
using CryptoExchange.Net.Authentication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snusnu.Services.SessionObjects
{
    public class BinanceWrapper
    {
        #region Datastore Logic

        private Session session;
        private string contents = "";
        private bool isBusy = false;
        private int requestSave = 0;

        private BinanceWrapper() { }

        public static async Task<BinanceWrapper> Initialize(Session session)
        {
            var ret = new BinanceWrapper()
            {
                session = session
            };
            await Task.Run(delegate
            {
                ret.contents = File.Exists(session.AbsolutePath) ? File.ReadAllText(session.AbsolutePath) : "";
            });
            return ret;
        }

        private async void Set(string key, string value)
        {
            contents = CommonHelpers.BlobSetValue(contents, key, value);
            requestSave++;
            if (isBusy) return;
            isBusy = true;
            while (requestSave > 0)
            {
                try
                {
                    string contentsCopy = contents;
                    Directory.CreateDirectory(session.AbsolutePath);
                    File.WriteAllText(session.AbsolutePath, contentsCopy);
                }
                catch { }
                await Task.Delay(250);
                requestSave--;
            }
            isBusy = false;
        }

        private string Get(string key)
        {
            return CommonHelpers.BlobGetValue(contents, key);
        }

        #endregion

        #region Properties

        private BinanceClient client;

        private string ApiKey
        {
            get => Get("apik");
            set => Set("apik", value);
        }

        private string ApiSecret
        {
            get => Get("apis");
            set => Set("apis", value);
        }

        public bool IsCredentialsReady => !string.IsNullOrEmpty(ApiKey) && !string.IsNullOrEmpty(ApiSecret);
        public bool IsStarted => client != null;

        #endregion

        #region Methods

        public bool TrySetApi(string apiKey, string apiSecret)
        {
            if (IsCredentialsReady) return false;
            ApiKey = apiKey;
            ApiSecret = apiSecret;
            return true;
        }

        public async void Start()
        {
            if (!IsCredentialsReady) return;
            client = new BinanceClient(new BinanceClientOptions
            {
                ApiCredentials = new ApiCredentials(ApiKey, ApiSecret)
            });

            var startResult = await client.Spot.UserStream.StartUserStreamAsync();

            if (!startResult.Success)
                throw new Exception($"Failed to start user stream: {startResult.Error}");

            var socketClient = new BinanceSocketClient();

            var s = socketClient.Spot.SubscribeToUserDataUpdates(startResult.Data,
                accountUpdate =>
                { // Handle account info update 

                }, orderUpdate =>
                { // Handle order update

                }, ocoUpdate =>
                { // Handle oco order update

                }, positionUpdate =>
                { // Handle account position update

                }, balanceUpdate =>
                { // Handle balance update

                });
        }

        #endregion
    }
}

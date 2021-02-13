using Binance.Net.Interfaces;
using Binance.Net.Objects.Spot.MarketData;
using MvvmHelpers;
using Snusnu.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snusnu.Models
{
    public class Market : ObservableObject
    {
        #region UIElements

        private string strSymbol;
        public string StrSymbol
        {
            get => strSymbol;
            set => SetProperty(ref strSymbol, value);
        }

        private string strPrice;
        public string StrPrice
        {
            get => strPrice;
            set => SetProperty(ref strPrice, value);
        }

        #endregion

        #region Properties

        private string baseWalletCode;
        private string quoteWalletCode;

        public Session Session { get; private set; }
        public string Symbol { get; set; }
        public decimal BuyMin { get; set; }
        public decimal SellMin { get; set; }
        public decimal StepSize { get; set; }
        public IBinanceTick Tick { get; set; }
        public DateTime TickLastUpdated { get; set; } = DateTime.UtcNow;

        public Wallet BaseWallet => Session.BinanceWrapper.Wallets.FirstOrDefault(i => i.Code.Equals(baseWalletCode));
        public Wallet QuoteWallet => Session.BinanceWrapper.Wallets.FirstOrDefault(i => i.Code.Equals(quoteWalletCode));

        #endregion

        #region Initializers

        private Market() { }
        public static Market FromPrimitive(BinanceSymbol symbol)
        {
            return new Market()
            {
                baseWalletCode = symbol.BaseAsset,
                quoteWalletCode = symbol.QuoteAsset,
                Symbol = symbol.Name,
                BuyMin = symbol.MinNotionalFilter.MinNotional,
                SellMin = symbol.LotSizeFilter.MinQuantity,
                StepSize = symbol.LotSizeFilter.StepSize,
            };
        }

        public void Update(Market market)
        {
            Symbol = market.Symbol;
            BuyMin = market.BuyMin;
            SellMin = market.SellMin;
            StepSize = market.StepSize;
        }

        #endregion

        #region Methods

        public void NotifyUpdate()
        {
            StrSymbol = Symbol;
            StrPrice = Tick?.LastPrice.ToString("0.00000000");
        }

        #endregion
    }
}

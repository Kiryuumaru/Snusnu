using Binance.Net.Enums;
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

        public Session Session { get; set; }
        public string Symbol { get; set; }
        public decimal MinTradeAmount { get; set; }
        public decimal MinPriceMovement { get; set; }
        public decimal MinOrderSize { get; set; }
        public IEnumerable<OrderType> OrderTypes { get; set; }
        public decimal TakerFeePercentage { get; set; }
        public decimal MakerFeePercentage { get; set; }

        public string BaseWalletCode { get; set; }
        public string QuoteWalletCode { get; set; }
        public Wallet BaseWallet => Session.BinanceWrapper.Wallets.FirstOrDefault(i => i.Code.Equals(BaseWalletCode));
        public Wallet QuoteWallet => Session.BinanceWrapper.Wallets.FirstOrDefault(i => i.Code.Equals(QuoteWalletCode));

        public decimal Price { get; set; }
        public DateTime PriceLastUpdated { get; set; }

        #endregion

        #region Initializers

        public Market() { }

        #endregion

        #region Methods

        public void NotifyUpdate()
        {
            StrSymbol = Symbol;
            StrPrice = Price.ToString();
        }

        #endregion
    }
}

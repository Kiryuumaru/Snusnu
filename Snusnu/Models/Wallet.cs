using Binance.Net.Enums;
using Binance.Net.Objects.Spot.WalletData;
using MvvmHelpers;
using Snusnu.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snusnu.Models
{
    public class Wallet : ObservableObject
    {
        #region UIElements

        private string strName;
        public string StrName
        {
            get => strName;
            set => SetProperty(ref strName, value);
        }

        private string strCode;
        public string StrCode
        {
            get => strCode;
            set => SetProperty(ref strCode, value);
        }

        private string strBalance;
        public string StrBalance
        {
            get => strBalance;
            set => SetProperty(ref strBalance, value);
        }

        private string strBalanceValue;
        public string StrBalanceValue
        {
            get => strBalanceValue;
            set => SetProperty(ref strBalanceValue, value);
        }

        private string strPrice;
        public string StrPrice
        {
            get => strPrice;
            set => SetProperty(ref strPrice, value);
        }

        private string strPriceChanges;
        public string StrPriceChanges
        {
            get => strPriceChanges;
            set => SetProperty(ref strPriceChanges, value);
        }

        #endregion

        #region Properties

        public Session Session { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Balance { get; set; }
        public decimal Price { get; set; }
        public double PriceChanges { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
        public List<Market> Markets { get; set; } = new List<Market>();
        public IEnumerable<Market> BaseMarkets => Markets.Where(m => m.BaseWallet.Code.Equals(Code)).ToList();
        public IEnumerable<Market> QuoteMarkets => Markets.Where(m => m.QuoteWallet.Code.Equals(Code)).ToList();

        #endregion

        #region Initializers

        public Wallet() { }

        #endregion

        #region Methods

        public override string ToString()
        {
            return Code;
        }

        public void NotifyUpdate()
        {
            StrName = Name;
            StrCode = Code.ToUpper();
            StrBalance = Balance.ToString();
            StrBalanceValue = Balance.ToString();
        }

        public bool HasMarket(Market market)
        {
            return Markets.Any(i => i.Symbol.Equals(market.Symbol));
        }

        public bool HasWalletInMarkets(Wallet wallet)
        {
            return Markets.Any(i => i.HasWallet(wallet));
        }

        public bool TryGetConversion(Wallet wallet, out Market market, out OrderSide? side)
        {
            market = Markets.FirstOrDefault(i => i.HasWallet(wallet));
            side = null;
            if (market != null)
            {
                if (market.BaseWallet.Code.Equals(wallet.Code)) side = OrderSide.Buy;
                else side = OrderSide.Sell;
                return true;
            }
            else return false;
        }

        public List<(Wallet Wallet, Market Market, OrderSide Side)> GetConversions()
        {
            var conv = new List<(Wallet Wallet, Market Market, OrderSide Side)>();
            foreach (Market market in BaseMarkets)
            {
                if (market.OrderTypes.Any(i => i == OrderType.Market))
                    conv.Add((market.QuoteWallet, market, OrderSide.Sell));
            }
            foreach (Market market in QuoteMarkets)
            {
                if (market.OrderTypes.Any(i => i == OrderType.Market))
                    conv.Add((market.BaseWallet, market, OrderSide.Buy));
            }
            return conv;
        }

        public WalletTree GetWalletTree()
        {
            return WalletTree.Parse(this);
        }

        #endregion
    }
}

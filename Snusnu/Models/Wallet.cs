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
        public DateTime BalanceLastUpdated { get; set; } = DateTime.UtcNow;
        public decimal Price { get; set; }
        public double PriceChanges { get; set; }
        public List<Market> Markets { get; set; } = new List<Market>();

        #endregion

        #region Initializers

        public Wallet() { }

        #endregion

        #region Methods

        public void NotifyUpdate()
        {
            StrName = Name;
            StrCode = Code.ToUpper();
            StrBalance = Balance.ToString();
            StrBalanceValue = Balance.ToString();
        }

        #endregion
    }
}

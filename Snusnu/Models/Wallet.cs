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

        public string strName;
        public string StrName
        {
            get => strName;
            set => SetProperty(ref strName, value);
        }

        public string strBalance;
        public string StrBalance
        {
            get => strBalance;
            set => SetProperty(ref strBalance, value);
        }

        public string strBalanceValue;
        public string StrBalanceValue
        {
            get => strBalanceValue;
            set => SetProperty(ref strBalanceValue, value);
        }

        public string strPrice;
        public string StrPrice
        {
            get => strPrice;
            set => SetProperty(ref strPrice, value);
        }

        public string strPriceChanges;
        public string StrPriceChanges
        {
            get => strPriceChanges;
            set => SetProperty(ref strPriceChanges, value);
        }

        #endregion

        #region Properties

        public Session Session { get; private set; }
        public string Name { get; private set; }
        public string Currency { get; private set; }
        public decimal Balance { get; private set; }
        public decimal Price { get; private set; }
        public double PriceChanges { get; private set; }
        public List<Market> Markets { get; internal set; } = new List<Market>();
        public DateTime LastUpdated { get; internal set; }

        #endregion

        #region Initializers


        #endregion

        #region Methods

        public void NotifyUpdate()
        {
            StrName = Currency.ToUpper() + " " + Name;
            StrBalance = Balance.ToString("0.00000000");
            StrBalanceValue = Balance.ToString("0.00000000");
        }

        #endregion
    }
}

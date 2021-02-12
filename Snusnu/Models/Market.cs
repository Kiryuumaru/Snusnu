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
        public string Symbol { get; private set; }
        public decimal Price { get; private set; }
        public Wallet BaseWallet { get; private set; }
        public Wallet QuoteWallet { get; private set; }
        public DateTime LastUpdated { get; internal set; }

        #endregion

        #region Initializers


        #endregion

        #region Methods

        public void NotifyUpdate()
        {

        }

        #endregion
    }
}

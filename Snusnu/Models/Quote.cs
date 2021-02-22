using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snusnu.Models
{
    public class Quote : ObservableObject
    {
        private decimal highPrice;
        public decimal HighPrice
        {
            get { return highPrice; }
            set => SetProperty(ref highPrice, value);
        }

        private decimal lowPrice;
        public decimal LowPrice
        {
            get { return lowPrice; }
            set => SetProperty(ref lowPrice, value);
        }

        private decimal openPrice;
        public decimal OpenPrice
        {
            get { return openPrice; }
            set => SetProperty(ref openPrice, value);
        }

        private decimal closePrice;
        public decimal ClosePrice
        {
            get { return closePrice; }
            set => SetProperty(ref closePrice, value);
        }

        private DateTime openTime;
        public DateTime OpenTime
        {
            get { return openTime; }
            set => SetProperty(ref openTime, value);
        }

        private DateTime closeTime;
        public DateTime CloseTime
        {
            get { return closeTime; }
            set => SetProperty(ref closeTime, value);
        }
    }
}

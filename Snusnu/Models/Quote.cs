using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snusnu.Models
{
    public class Estimate : ObservableObject
    {
        private decimal price;
        public decimal Price
        {
            get { return price; }
            set => SetProperty(ref price, value);
        }
    }

    public class Quote : Estimate
    {
        private decimal priceChangePercent;
        public decimal PriceChangePercent
        {
            get { return priceChangePercent; }
            set => SetProperty(ref priceChangePercent, value);
        }

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

        private decimal volume;
        public decimal Volume
        {
            get { return volume; }
            set => SetProperty(ref volume, value);
        }

        private decimal tradeAmount;
        public decimal TradeAmount
        {
            get { return tradeAmount; }
            set => SetProperty(ref tradeAmount, value);
        }

        private decimal tradePrice;
        public decimal TradePrice
        {
            get { return tradePrice; }
            set => SetProperty(ref tradePrice, value);
        }

        public Quote(string symbol, decimal price)
        {

        }
    }
}

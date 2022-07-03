using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skender.Stock.Indicators;
namespace StockTest
{
    public class DealInfo
    {

        public Quote Buy { get; set; }

        public Quote Sell { get; set; }

        public List<double> RateOfDeal { get; set; }  //百分比 %

        public double InitMoney { get; set; }

        public int DealCount { get; set; } //交易次数


        public TimeSpan DealTimeDistance { get; set; } //交易时间跨度

    }
}

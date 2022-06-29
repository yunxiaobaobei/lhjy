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

        public List<double> RateOfDeal { get; set; }

        public float InitMoney { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTest
{
    public class OneMinuteDraw
    {

        public double MaxPrice { get; set; }

        public double MinPrice { get; set; }

        public List<double> Prices { get; set; }

        public List<double> Ave_prices { get; set; }

        public List<double>  Amount {get; set; }

        public List<double> Ave_amout { get; set; } //分时均量

        public List<int> KeyPointSel { get; set; } //分析的关键点

        public List<int> KeyPointBuy { get; set; } //分析的关键点

    }
}

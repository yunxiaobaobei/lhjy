using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTest
{
    public enum ShowKlineLevel
    {
         OneMinite = 1,

        //https://stock.xueqiu.com/v5/stock/chart/kline.json?symbol=SZ300087&begin=1655302389905&period=5m&type=before&count=-284&indicator=kline,pe,pb,ps,pcf,market_capital,agt,ggt,balance

        //https://stock.xueqiu.com/v5/stock/chart/kline.json?symbol=SZ300087&begin=1655302532523&period=60m&type=before&count=-284&indicator=kline,pe,pb,ps,pcf,market_capital,agt,ggt,balance
    }
}

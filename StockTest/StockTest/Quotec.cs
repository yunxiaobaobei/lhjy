using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StockTest
{
    public class Quotec
    {

        public int Error_code { get; set; }


        public string Error_description { get; set; }


        [JsonProperty("data")]
        public QuoteInfo[] Data { get; set; }
    }

    public class QuoteInfo
    {
        public string Symbol { get; set; }

        public float Current {get; set; }
        public float Percent { get; set; }
        public float Chg { get; set; }
        public long Timestamp { get; set; }
        public int Volume { get; set; }
        public double Amount { get; set; }
        public double Market_capital { get; set; }

        public double Float_market_capital { get; set; }

        public float Turnover_rate { get; set; }
        public float Amplitude { get; set; }

        public float Open { get; set; }
        public float Last_close { get; set; }
        public float High {get; set; }
        public float Low {get; set; }
        public float Avg_price {get; set; }
        public float Trade_volume {get; set; }
        public int Side  {get; set; }
        public bool Is_trade {get; set; }
        public int Level {get; set; }

        public string Trade_session {get; set; }
        public string Trade_type {get; set; }
        public float? Current_year_percent {get; set; }
        public string Trade_unique_id {get; set; }
        public int Type {get; set; }
        public string Bid_appl_seq_num { get; set; }
        public string Offer_appl_seq_num { get; set; }
        public string Traded_amount_ext { get; set; }
        public string Trade_type_v2 { get; set; }

    }


}

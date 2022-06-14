using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace StockTest
{
    public class Trade
    {
        public int Error_code { get; set; }


        public string Error_description { get; set; }

        [JsonProperty("data")]
        public TradeInfo Data  { get; set; }

    }

    public class TradeInfo
    {
        public string Symbol { get; set; }

        [JsonProperty("items")]
        public TradeDetails[] Items { get; set; }
    }


    public class TradeDetails
    {
        public string Symbol { get; set; }
        public long Timestamp { get; set; }
        public float Current { get; set; }
        public float Chg { get; set; }
        public float Percent { get; set; }
        public int  Trade_volume { get; set; }
        public int Side { get; set; }
        public int Level { get; set; }
        public string Trade_session { get; set; }
        public string Trade_type { get; set; }
        public string Trade_unique_id { get; set; }
        public string Bid_appl_seq_num { get; set; }
        public string Offer_appl_seq_num {get; set; }
        public string Trade_type_v2 { get; set; }
    }
}

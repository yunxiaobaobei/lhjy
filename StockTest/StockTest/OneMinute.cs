using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace StockTest
{
    public class OneMinute
    {
        [JsonProperty("data")]
        public Data data { get; set; }



        public int error_code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string error_description { get; set; }
    }


    public class Data
    {
        public double last_close { get; set; }


        [JsonProperty("after")]
        public List<After> after;

        [JsonProperty("items")]
        public List<Items> items;

        public int items_size { get; set; }
    }


    public class After
    {

            public double current { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public double volume { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public double avg_price { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public double chg { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public double percent { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public long timestamp { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public double amount { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string high { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string low { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string macd { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string kdj { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string ratio { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public Capital capital { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string volume_compare { get; set; }

    }


    public class Items
    {

        /// <summary>
        /// 
        /// </summary>
        public double current { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double volume { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double avg_price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string chg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double percent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long timestamp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double amount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string high { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string low { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string macd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string kdj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ratio { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Capital capital { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Volume_compare volume_compare { get; set; }


    }


    public class Volume_compare
    {
        /// <summary>
        /// 
        /// </summary>
        public double volume_sum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double volume_sum_last { get; set; }

    }

    public class Capital
    {
        /// <summary>
        /// 
        /// </summary>
        public double small { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double medium { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double large { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double xlarge { get; set; }

    }

}

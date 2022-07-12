using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StockTest
{
    public class QHquote
    {
       
            [JsonProperty("d")]
            public DateTime Date
            {
                get;
                set;
            }

            [JsonProperty("o")]
            public decimal Open
            {
                get;
                set;
            }

            [JsonProperty("h")]
            public decimal High
            {
                get;
                set;
            }

            [JsonProperty("l")]
            public decimal Low
            {
                get;
                set;
            }

            [JsonProperty("c")]
            public decimal Close
            {
                get;
                set;
            }

            [JsonProperty("v")]
            public decimal Volume
            {
                get;
                set;
            }



        }

    
}

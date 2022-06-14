using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StockTest
{
    public class Pankou
    {
        public  int Error_code { get; set; }


        public string Error_description { get; set; }

        [JsonProperty("data")]
        public PankouData Data { get; set; }

    }

    public class PankouData
    {
        public string Symbol { get; set; }

        public long timestamp { get; set; }

        public float? Current { get; set; }

        public float? Bp1 { get; set; }

        public float? Bp2 { get; set; }

        public float? Bp3 { get; set; }

        public float? Bp4 { get; set; }

        public float? Bp5 { get; set; }

        public int Bc1 { get; set; }
        public int Bc2 { get; set; }
        public int Bc3{ get; set; }
        public int Bc4 { get; set; }
        public int Bc5 { get; set; }

        public float? Sp1 { get; set; }

        public float? Sp2 { get; set; }

        public float? Sp3 { get; set; }

        public float? Sp4 { get; set; }

        public float? Sp5 { get; set; }

        public int Sc1 { get; set; }
        public int Sc2 { get; set; }
        public int Sc3 { get; set; }
        public int Sc4 { get; set; }
        public int Sc5 { get; set; }

        public float Buypct { get; set; }

        public float Sellpct { get; set; }

        public float Diff { get; set; }

        public float Ratio { get; set; }

        public int Level { get; set; }
    }

}

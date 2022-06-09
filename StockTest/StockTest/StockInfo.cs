using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace StockTest
{
    public class StockInfo
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Exchange { get; set; }
        public string Abbreviation { get; set; }

    }

    public class Record
    {
        [JsonProperty("RECORDS")]
        public List<StockInfo> stockInfos { get; set; }
    }

}

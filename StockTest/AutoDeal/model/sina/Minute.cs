using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AutoDeal.model.sina
{
    public class Minute
    {
        [JsonProperty("day")]
        public DateTime Date
        {
            get;
            set;
        }

        public decimal Open
        {
            get;
            set;
        }

        public decimal High
        {
            get;
            set;
        }

        public decimal Low
        {
            get;
            set;
        }

        public decimal Close
        {
            get;
            set;
        }

        public decimal Volume
        {
            get;
            set;
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPTVDirectoryApiCosmosDB.Core.Entities
{
    public  class JsonChannel
    {
        public string tv_id { get; set; }
        public string tv_name { get; set; }
        public string url { get; set; }
        public string logo { get; set; }
        public string country { get; set; }
        public string category { get; set; }
    }
}

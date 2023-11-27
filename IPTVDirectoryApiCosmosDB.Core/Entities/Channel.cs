
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace IPTVDirectoryApiCosmosDB.Core.Entities
{
    public record Channel : BaseEntity
    {
        public string tv_id { get; set; }
        public string tv_name { get; set; }
        public string url { get; set; }
        public string logo { get; set; }
        public string country { get; set; }
        public string category { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }

}

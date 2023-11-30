
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IPTVDirectoryApiCosmosDB.Core.Entities
{
    public record BaseEntity
    {
        [Key]
        [JsonPropertyName("id")]

        /*
            When we use the id part of document so [key] attribiute is necessary to 
            avoid findAsync method exception as it will return 2 key value are same  
             [JsonPropertyName("id")] is helpfule to avoid conversion from GUID -> String when returning object to request 
             parse from Json Value -> GUID is required when mapping json document to entity object 
        */
        public Guid id { get; set; }

    }
}

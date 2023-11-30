
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IPTVDirectoryApiCosmosDB.Core.Entities
{
    public record BaseEntity
    {
        [Key]
       public Guid id { get; set; }

    }
}

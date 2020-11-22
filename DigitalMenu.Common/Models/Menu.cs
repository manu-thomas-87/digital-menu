using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DigitalMenu.Common.Models
{
   public class Menu
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsPublished { get; set; }

        public string Locale { get; set; }

        [Required]
        public List<Dish> Dishes { get; set; }
    }
}

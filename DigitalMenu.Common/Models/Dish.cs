using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DigitalMenu.Common.Models
{
   public class Dish
    {
        [BsonId]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public byte[] ContentImage { get; set; }
        public int? Price { get; set; }
        public List<Prices> Prices { get; set; }
        public string IngredientsAndWarinng { get; set; }
        public int PreparationTime { get; set; }
        public string[] Tags { get; set; }

        public string Cuisine { get; set; }
        public string Catagory { get; set; }

        public string[] TimeOfTheday { get; set; }

        public string[] DayOfTheWeek { get; set; }

        public bool IsSoldOut { get; set; }

        public bool IsDeleted { get; set; }

        public int Calories { get; set; }
    }
}

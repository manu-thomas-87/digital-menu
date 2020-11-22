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
        /// <summary>
        /// Unique id of the dish
        /// </summary>
        [BsonId]
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the dish
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Short description of the dish 
        /// </summary>
        public string ShortDescription { get; set; }

        /// <summary>
        /// Binary of the image if avaiable
        /// </summary>
        public byte[] ContentImage { get; set; }

        /// <summary>
        /// Price of the dish to serve
        /// </summary>
        public Double? Price { get; set; }

        /// <summary>
        /// Price of the dish if item is avaiable in differet quqntity ie, Medium, Large..
        /// </summary>
        public List<Prices> Prices { get; set; }

        /// <summary>
        /// Main ingredients and allergy warning
        /// </summary>
        public string IngredientsAndWarinng { get; set; }

        /// <summary>
        /// Preparation time in minute
        /// </summary>
        public int PreparationTime { get; set; }

        /// <summary>
        /// Tags to search the dish
        /// </summary>
        public string[] Tags { get; set; }

        /// <summary>
        /// Cuisine
        /// </summary>
        public string Cuisine { get; set; }

        /// <summary>
        /// Dish category eg : starter, main course, desserts...etc
        /// </summary>
        public string Catagory { get; set; }

        /// <summary>
        /// Availability of the dish, eg: breakfast, dinner, weekdays, weekends ..etc
        /// </summary>
        public string[] Availablility { get; set; }

        /// <summary>
        /// True if the item is sold out
        /// </summary>
        public bool IsSoldOut { get; set; }

        /// <summary>
        /// True if the item is temporarily not avilable
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Total calorie per 100g 
        /// </summary>
        public int Calories { get; set; }
    }
}

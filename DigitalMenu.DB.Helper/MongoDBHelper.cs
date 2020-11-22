using DigitalMenu.Common.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace DigitalMenu.DB.Helper
{
    public class MongoDBHelper
    {
        private IMongoDatabase db;

        public MongoDBHelper(string database)
        {
            var client = new MongoClient();
            db = client.GetDatabase(database);
        }
        const string menuCollection = "MenuCard";

        /// <summary>
        /// Get all menu items
        /// </summary>
        /// <returns>List of Menu items from database </returns>
        public List<Menu> GetAllMenu()
        {
            var collection = db.GetCollection<Menu>(menuCollection);
            return collection.Find(new BsonDocument()).ToList();
        }

        /// <summary>
        /// Get the menu item matched with id and locale
        /// </summary>
        /// <param name="id">Menu item id</param>
        /// <param name="locale">Locale of the menu</param>
        /// <returns>Single menu item</returns>
        public Menu GetMenuByIdAndLocale(Guid id, string locale)
        {
            var collection = db.GetCollection<Menu>(menuCollection);
            var filter = Builders<Menu>.Filter.Eq("_id", id) & Builders<Menu>.Filter.Eq("Locale", locale);
            return collection.Find(filter).FirstOrDefault();
        }
        /// <summary>
        /// Create new menu utem in the database
        /// </summary>
        /// <param name="menuItem">Content of the menu to be created</param>
        public void InsertMenu(Menu menuItem)
        {
            var collection = db.GetCollection<Menu>(menuCollection);
            collection.InsertOne(menuItem);
        }

        /// <summary>
        /// Check the menu item is already present in the database
        /// </summary>
        /// <param name="menuName">Menu item name</param>
        /// <returns>True if menu is available else false</returns>
        public bool MenuExists(string menuName)
        {
            var collection = db.GetCollection<Menu>(menuCollection);
            var filter = Builders<Menu>.Filter.Eq("Name", menuName);
            return collection.Find(filter).SingleOrDefault() != null ? true : false;
        }

        /// <summary>
        /// Delete a a menu item
        /// </summary>
        /// <param name="id">Menu item id</param>
        /// <returns>True if item successfully deleted</returns>
        public bool DeleteMenu(Guid id)
        {
            var collection = db.GetCollection<Menu>(menuCollection);
            var filter = Builders<Menu>.Filter.AnyEq("_id", id);
            DeleteResult result = collection.DeleteOne(filter);
            return result.DeletedCount > 0 ? true : false;
        }

        /// <summary>
        /// Update the menu item
        /// </summary>
        /// <param name="menuItem">Item needs to be replaced</param>
        /// <param name="id">Menu item id</param>
        /// <returns>True if the menu item is succesfully updated</returns>
        public bool UpdateMenu(Menu menuItem, Guid id)
        {
            var collection = db.GetCollection<Menu>(menuCollection);
            var filter = Builders<Menu>.Filter.AnyEq("_id", id);
            var result = collection.ReplaceOne(filter, menuItem, new ReplaceOptions { IsUpsert = false });
            return result.ModifiedCount > 0 ? true : false; ;
        }
        /// <summary>
        /// Insert a new dish to the menu
        /// </summary>
        /// <param name="id">Menu item id</param>
        /// <param name="dishItem">Item to be added to the menu item</param>
        /// <returns>True if successfully inserted the dish</returns>

        public bool InsertDishes(Guid id, Dish dishItem)
        {
            var collection = db.GetCollection<Menu>(menuCollection);
            var update = Builders<Menu>.Update;
            var pushDish = update.Push(x => x.Dishes, dishItem);
            var filter = Builders<Menu>.Filter.AnyEq("_id", id);
            var result = collection.UpdateOne(filter, pushDish);
            return result.ModifiedCount > 0 ? true : false;
        }

        /// <summary>
        /// Update a dish item
        /// </summary>
        /// <param name="id">Menu item id</param>
        /// <param name="dishId">Dish item id</param>
        /// <param name="dishItem">Item needs to be replaced</param>
        /// <returns>True if the item is successfully updated</returns>
        public bool UpdateDishes(Guid id, Guid dishId, Dish dishItem)
        {
            var collection = db.GetCollection<Menu>(menuCollection);
            var menuFilter = Builders<Menu>.Filter;

            var dishFilter = menuFilter.And(menuFilter.Eq(x => x.Id, id), menuFilter.ElemMatch(x => x.Dishes, c => c.Id == dishId));
            var dishItemReplace = collection.Find(dishFilter).SingleOrDefault();

            var update = Builders<Menu>.Update;
            var courseLevelSetter = update.Set("Dishes.$", dishItem);
           var result = collection.UpdateOne(dishFilter, courseLevelSetter);
            return result.ModifiedCount > 0 ? true : false;
        }

        /// <summary>
        /// Delete a dish item
        /// </summary>
        /// <param name="dishId">Dish item id</param>
        /// <param name="id">Menu item id</param>
        /// <returns>True if the item is successfully deleted</returns>
        public bool DeleteDish(Guid dishId, Guid id)
        {
            var collection = db.GetCollection<Menu>(menuCollection);
            var filter = Builders<Menu>.Filter.AnyEq("_id", id);

            var update = Builders<Menu>.Update.PullFilter("Dishes",
                Builders<Dish>.Filter.AnyEq("_id", dishId));
            var result = collection.FindOneAndUpdate(filter, update);
            return result != null ? true : false;
        }
    }
}

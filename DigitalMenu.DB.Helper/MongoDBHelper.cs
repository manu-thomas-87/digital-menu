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

        public List<Menu> GetAllMenu()
        {
            var collection = db.GetCollection<Menu>(menuCollection);
            return collection.Find(new BsonDocument()).ToList();
        }

        public Menu GetMenuByIdAndLocale(Guid id, string locale)
        {
            var collection = db.GetCollection<Menu>(menuCollection);
            var filter = Builders<Menu>.Filter.Eq("_id", id) & Builders<Menu>.Filter.Eq("Locale", locale);
            return collection.Find(filter).FirstOrDefault();
        }
        public void InsertMenu(Menu menuItem)
        {
            var collection = db.GetCollection<Menu>(menuCollection);
          collection.InsertOne(menuItem);
        }

        public bool MenuExists(string menuName)
        {
            var collection = db.GetCollection<Menu>(menuCollection);
            var filter = Builders<Menu>.Filter.Eq("Name", menuName);
            return collection.Find(filter).SingleOrDefault() != null? true : false;
        }
        public void DeleteMenu(Guid id)
        {
            var collection = db.GetCollection<Menu>(menuCollection);
            var filter = Builders<Menu>.Filter.AnyEq("_id", id);
            collection.DeleteOne(filter);
        }

        public bool UpdateMenu(Menu menuItem, Guid id)
        {
            var collection = db.GetCollection<Menu>(menuCollection);
            var filter = Builders<Menu>.Filter.AnyEq("_id", id);
            var result = collection.ReplaceOne(filter, menuItem, new ReplaceOptions { IsUpsert = false });
            return result.IsModifiedCountAvailable;
        }

        public bool InsertDishes(Guid id, Dish dishItem)
        {
            var collection = db.GetCollection<Menu>(menuCollection);
            var update = Builders<Menu>.Update;
            var pushDish = update.Push(x => x.Dishes, dishItem);
            var filter = Builders<Menu>.Filter.AnyEq("_id", id);
            collection.UpdateOne(filter, pushDish);
            return true;
        }

        public bool UpdateDishes(Guid id, string dishUniqueName, Dish dishItem)
        {
            var collection = db.GetCollection<Menu>(menuCollection);
            var menuFilter = Builders<Menu>.Filter;

            var dishFilter = menuFilter.And(menuFilter.Eq(x => x.Id, id), menuFilter.ElemMatch(x => x.Dishes, c => c.UniqueName == dishUniqueName));
            var dishItemReplace = collection.Find(dishFilter).SingleOrDefault();

            var update = Builders<Menu>.Update;
            var courseLevelSetter = update.Set("Dishes.$", dishItem);
            collection.UpdateOne(dishFilter, courseLevelSetter);
            return true;
        }
        public void DeleteDish(string dishUniqueName, Guid id)
        {
            //var collection = db.GetCollection<Menu>(menuCollection);
            //var menuFilter = Builders<Menu>.Filter;
            //var dishFilter = menuFilter.And(menuFilter.Eq(x => x.Id, id), menuFilter.ElemMatch(x => x.Dishes, c => c.UniqueName == dishUniqueName));

            //var update = Builders<Menu>.Update;
            //var courseLevelSetter = update.PullFilter("Dishes.$", dishItem);

            //collection.DeleteOne(dishFilter);


            var collection = db.GetCollection<Menu>(menuCollection);
            var filter = Builders<Menu>.Filter.AnyEq("_id", id);

            var update = Builders<Menu>.Update.PullFilter("Dishes",
                Builders<Dish>.Filter.Eq("UniqueName", dishUniqueName));
            var result = collection.FindOneAndUpdate(filter, update);
        }
    }
}

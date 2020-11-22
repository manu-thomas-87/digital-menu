using DigitalMenu.Common.Models;
using DigitalMenu.DB.Helper;

using DigitalMenu.Web.API.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalMenu.Web.API.Services.Services.Implementations
{
    public class MenuService : IMenu
    {
        private MongoDBHelper db = new MongoDBHelper("Menu");
        public void CreateMenu(Menu menu)
        {
            if (db.MenuExists(menu.Name,menu.Locale))
            {
                throw new Exception($"Menu '{menu.Name}' already exists for the locale '{menu.Locale}' ");
            }

            _UpdateDishReferenceId(menu);
            db.InsertMenu(menu);
        }

        private void _UpdateDishReferenceId(Menu menu)
        {
            foreach(var dish in menu.Dishes)
            {
                _GenerateId(dish);
            }
        }

        public bool DeleteDish(Guid id, Guid dishId)
        {
            return db.DeleteDish(dishId, id);
        }

        public bool DeleteMenu(Guid id)
        {
          return  db.DeleteMenu(id);
        }

        public Menu GetMenu(string name, string locale)
        {
            return db.GetMenuByNameAndLocale(name, locale);
        }

        public List<Menu> GetAllMenu()
        {
            return db.GetAllMenu();
        }

        public bool InsertDishes(Guid id, Dish dish)
        {
            _GenerateId(dish);
          return  db.InsertDishes(id, dish);
        }

        public bool UpdateDishes(Guid id, Guid dishId, Dish dish)
        {
            _GenerateId(dish);
            return db.UpdateDishes(id, dishId, dish);
        }

        public bool UpdateMenu(Menu menu, Guid id)
        {
            _UpdateDishReferenceId(menu);
            return db.UpdateMenu(menu, id);
        }

        private Dish _GenerateId(Dish dishItem)
        {
            if (dishItem.Id == Guid.Empty)
            {
                var newGuid = Guid.NewGuid();
                dishItem.Id = newGuid;
            }
            return dishItem;
        }
    }
}

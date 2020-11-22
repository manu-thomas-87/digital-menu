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
            if (db.MenuExists(menu.Name))
            {
                throw new Exception($"Menu({menu.Name}) already exists ");
            }
        db.InsertMenu(menu);
        }

        public void DeleteDish(Guid id, string dishUniqueName)
        {
            db.DeleteDish(dishUniqueName, id);
        }

        public void DeleteMenu(Guid id)
        {
            db.DeleteMenu(id);
        }

        public Menu GetMenu(Guid id, string locale)
        {
            return db.GetMenuByIdAndLocale(id, locale);
        }

        public List<Menu> GetAllMenu()
        {
            return db.GetAllMenu();
        }

        public bool InsertDishes(Guid id, Dish dish)
        {
          return  db.InsertDishes(id, dish);
        }

        public bool UpdateDishes(Guid id, string dishId, Dish dish)
        {
           return db.UpdateDishes(id, dishId, dish);
        }

        public bool UpdateMenu(Menu menu, Guid id)
        {
            return db.UpdateMenu(menu, id);
        }
    }
}

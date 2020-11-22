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
        private MongoDBHelper db = new MongoDBHelper("Menu2");
        public void CreateMenu(Menu menu)
        {
            if (db.MenuExists(menu.Name))
            {
                throw new Exception($"Menu({menu.Name}) already exists ");
            }

            _UpdateDishReferenceId(menu);
            db.InsertMenu(menu);
        }

        private void _UpdateDishReferenceId(Menu menu)
        {
            for (int i = 0; i < menu.Dishes.Count; i++)
            {
                var tmpDishItem = menu.Dishes[i];
                tmpDishItem = _GenerateId(tmpDishItem);
                menu.Dishes[i] = tmpDishItem;
            }
        }

        public void DeleteDish(Guid id, Guid dishId)
        {
            db.DeleteDish(dishId, id);
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
          return  db.InsertDishes(id, _GenerateId(dish));
        }

        public bool UpdateDishes(Guid id, Guid dishId, Dish dish)
        {
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

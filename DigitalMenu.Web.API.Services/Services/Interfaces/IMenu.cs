using DigitalMenu.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalMenu.Web.API.Services.Services.Interfaces
{
   public interface IMenu
    {
        List<Menu> GetAllMenu();

        Menu GetMenu(Guid id, string locale);

        void DeleteMenu(Guid id);

        void CreateMenu(Menu menu);

        bool UpdateMenu(Menu menu, Guid id);

        bool InsertDishes(Guid id, Dish dish);
        bool UpdateDishes(Guid menuId, Guid dishId, Dish dish);
        void DeleteDish( Guid id, Guid dishId);

    }
}

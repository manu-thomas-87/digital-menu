using DigitalMenu.Common.Models;
using DigitalMenu.Web.API.FilterAttribute;
using DigitalMenu.Web.API.Services.Services.Interfaces;
using System;
using System.Net;
using System.Web.Http;

namespace DigitalMenu.Web.API.Controllers
{
    public class MenuController : ApiController
    {

        IMenu menuService { get; set; }

        public MenuController(IMenu service)
        {
            menuService = service;
        }
        // GET: api/Menu
        public IHttpActionResult Get()
        {
            return Content(HttpStatusCode.OK, menuService.GetAllMenu());
        }

        // GET:  api/Menu/{id}?locale={locale}
        public IHttpActionResult Get(Guid id, string locale)
        {
            var menu = menuService.GetMenu(id, locale);
            return Content(HttpStatusCode.OK, menuService.GetMenu(id, locale));
        }

        // POST: api/Menu
        [ValidateModel]
        public IHttpActionResult Post([FromBody] Menu menu)
        {
            menuService.CreateMenu(menu);
            return base.Created("Menu", menu);
        }

        // PUT: api/Menu/{id}
        [ValidateModel]
        public void Put(Guid id, [FromBody] Menu menu)
        {
          bool bSuccess =   menuService.UpdateMenu(menu, id);
        }


        // DELETE: api/Menu/{id}
        public void Delete(Guid id)
        {
            menuService.DeleteMenu(id);
        }

        #region dish
        [HttpPut]
        [ValidateModel]
        [Route("api/Menu/{id}/dish/{dishUniqueName}")]
        public void Put(Guid id, string dishUniqueName, [FromBody] Dish dish)
        {
            menuService.UpdateDishes(id, dishUniqueName, dish);
        }

        [HttpPost]
        [ValidateModel]
        [Route("api/Menu/{id}/dish")]
        public void Post(Guid id, [FromBody] Dish dish)
        {
            menuService.InsertDishes(id, dish);
        }

        [HttpDelete]
        [ValidateModel]
        [Route("api/Menu/{id}/dish/{dishUniqueName}")]
        public void Put(Guid id, string dishUniqueName)
        {
            menuService.DeleteDish(id, dishUniqueName);
        }
        #endregion
    }
}

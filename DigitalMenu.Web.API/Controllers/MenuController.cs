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
            var lstMenu = menuService.GetAllMenu();
            if (lstMenu.Count < 1)
            {
                return NotFound();
            }
            return Content(HttpStatusCode.OK, menuService.GetAllMenu());
        }

        // GET:  api/Menu/{id}?locale={locale}
        public IHttpActionResult Get(Guid id, string locale)
        {
            var menu = menuService.GetMenu(id, locale);
            if(menu==null)
            {
                return NotFound();
            }
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
        public IHttpActionResult Put(Guid id, [FromBody] Menu menu)
        {
          bool bSuccess =   menuService.UpdateMenu(menu, id);
            if (!bSuccess)
            {
                return Content(HttpStatusCode.BadRequest, $"Menu {id} is not found");
            }
            return Ok();
        }


        // DELETE: api/Menu/{id}
        public IHttpActionResult Delete(Guid id)
        {
            bool bSuccess = menuService.DeleteMenu(id);
            if (!bSuccess)
            {
                return Content(HttpStatusCode.BadRequest, $"Menu {id} is not found");
            }
            return Ok();
        }

        #region dish
        [HttpPut]
        [ValidateModel]
        [Route("api/Menu/{id}/dish/{dishId}")]
        public IHttpActionResult Put(Guid id, Guid dishId, [FromBody] Dish dish)
        {
            bool bSuccess = menuService.UpdateDishes(id, dishId, dish);
            if (!bSuccess)
            {
                return Content(HttpStatusCode.BadRequest, $"Menu {id} and dish {dishId} is not found");
            }
            return Ok();
        }

        [HttpPost]
        [ValidateModel]
        [Route("api/Menu/{id}/dish")]
        public IHttpActionResult Post(Guid id, [FromBody] Dish dish)
        {
            bool bSuccess = menuService.InsertDishes(id, dish);
            if (!bSuccess)
            {
                return Content(HttpStatusCode.BadRequest, $"Menu {id} is not found");
            }
            return Ok();
        }

        [HttpDelete]
        [ValidateModel]
        [Route("api/Menu/{id}/dish/{dishId}")]
        public IHttpActionResult Delete(Guid id, Guid dishId)
        {
          bool bSuccess=   menuService.DeleteDish(id, dishId);
            if (!bSuccess)
            {
                return Content(HttpStatusCode.BadRequest, $"Menu {id} and dish {dishId} is not found");
            }
            return Ok();
        }
        #endregion
    }
}

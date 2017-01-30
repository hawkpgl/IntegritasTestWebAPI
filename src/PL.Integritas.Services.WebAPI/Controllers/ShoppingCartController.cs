using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PL.Integritas.Application.ViewModels;
using PL.Integritas.Services.WebAPI.Models;
using PL.Integritas.Application.Interfaces;

namespace PL.Integritas.Services.WebAPI.Controllers
{
    public class ShoppingCartController : ApiController
    {
        private IShoppingCartAppService _shoppingCartAppService;

        public ShoppingCartController(IShoppingCartAppService shoppingCartAppService)
        {
            _shoppingCartAppService = shoppingCartAppService;
        }

        // GET: api/ShoppingCart/5
        [ResponseType(typeof(ShoppingCartViewModel))]
        public IHttpActionResult GetShoppingCartViewModel(int id)
        {
            IEnumerable<ShoppingCartItemViewModel> items = _shoppingCartAppService.GetItemsByCartNumber(id);

            if (items == null)
            {
                return NotFound();
            }

            return Ok(items);
        }

        // PUT: api/ShoppingCart/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutShoppingCartViewModel(int id, ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _shoppingCartAppService.AddItemToCart(productViewModel.CartNumber, id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_shoppingCartAppService.Search(x => x.Number == id).Any())
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_shoppingCartAppService != null)
                {
                    _shoppingCartAppService.Dispose();
                }
            }

            base.Dispose(disposing);
        }
    }
}
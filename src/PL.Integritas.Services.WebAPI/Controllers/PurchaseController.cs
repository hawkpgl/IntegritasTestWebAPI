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
    public class PurchaseController : ApiController
    {
        private IPurchaseAppService _purchaseAppService;

        public PurchaseController(IPurchaseAppService purchaseAppService)
        {
            _purchaseAppService = purchaseAppService;
        }

        public IEnumerable<PurchaseViewModel> GetPurchaseViewModel()
        {
            return _purchaseAppService.GetAll();
        }
        
        // POST: api/ProductTeste
        [ResponseType(typeof(ProductViewModel))]
        public IHttpActionResult PostPurchaseViewModel(PurchaseViewModel purchaseViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _purchaseAppService.Add(purchaseViewModel);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return CreatedAtRoute("DefaultApi", new { id = purchaseViewModel.Id }, purchaseViewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_purchaseAppService != null)
                {
                    _purchaseAppService.Dispose();
                }
            }

            base.Dispose(disposing);
        }
    }
}
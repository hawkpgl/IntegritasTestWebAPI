using PL.Integritas.Application.Interfaces;
using PL.Integritas.Application.ViewModels;
using System.Collections.Generic;
using System.Web.Http;

namespace PL.Integritas.Services.WebAPI.Controllers
{
    public class ProductController : ApiController
    {
        private IProductAppService _productAppService;

        public ProductController(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<ProductViewModel> Get()
        {
            IEnumerable<ProductViewModel> products = _productAppService.GetAll();

            return products;
        }

        // GET api/values/5
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            ProductViewModel product = _productAppService.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
    }
}

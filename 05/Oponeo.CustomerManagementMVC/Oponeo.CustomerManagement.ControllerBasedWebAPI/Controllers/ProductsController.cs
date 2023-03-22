using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oponeo.CustomerManagementMVC.Domain.Models;
using Oponeo.CustomerManagementMVC.Services.Products;

namespace Oponeo.CustomerManagement.ControllerBasedWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;
        public ProductsController(ProductService productService)
        {
            this._productService = productService;
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
        public IActionResult Get()
        {
            return Ok(this._productService.Get());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        public IActionResult Get(int id)
        {
            return Ok(this._productService.GetById(id));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Product))]
        public IActionResult Post(Product product)
        {
            this._productService.AddOrUpdate(product);
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(Product product)
        {
            if (this._productService.GetById(product.Id) == null)
            {
                return NotFound();
            }

            this._productService.AddOrUpdate(product);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int id)
        {
            this._productService.Remove(id);
            return NoContent();
        }
    }
}

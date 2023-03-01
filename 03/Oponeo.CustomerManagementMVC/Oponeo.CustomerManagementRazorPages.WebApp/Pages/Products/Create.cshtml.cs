using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Oponeo.CustomerManagementMVC.Domain;
using Oponeo.CustomerManagementMVC.Domain.Models;
using Oponeo.CustomerManagementMVC.Services.Products;

namespace Oponeo.CustomerManagementRazorPages.WebApp.Pages.Products
{
    public class CreateModel : PageModel
    {
        private ProductService _productService;

        [BindProperty]
        public Product Product { get; set; } = new Product();

        [BindProperty]
        public IEnumerable<SelectListItem> ProductTypes { get; set; }

        public CreateModel(ProductService productService)
        {
            _productService = productService;
        }

        public void OnGet()
        {
            ProductTypes = _productService.GetProductTypes().Select(x => new SelectListItem
            {
                Text = x.TypeName,
                Value = x.Id.ToString()
            });
        }

        public IActionResult OnPost(Product product)
        {
            this._productService.AddOrUpdate(product);
            return RedirectToPage("Index");
        }
    }
}

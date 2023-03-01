using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Oponeo.CustomerManagementMVC.Domain.Models;
using Oponeo.CustomerManagementMVC.Services.Products;

namespace Oponeo.CustomerManagementRazorPages.WebApp.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly ProductService _productService;
        [BindProperty]
        public IList<Product> Products { get; set; }

        public IndexModel(ProductService productService)
        {
            _productService = productService;
        }

        public void OnGet()
        {
            Products = _productService.Get().ToList();
        }
    }
}

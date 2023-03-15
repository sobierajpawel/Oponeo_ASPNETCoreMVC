using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Oponeo.CustomerManagementMVC.Domain.Models;
using Oponeo.CustomerManagementMVC.Services.Products;

namespace Oponeo.CustomerManagementRazorPages.WebApp.Pages.Products
{
    public class DetailModel : PageModel
    {
        private readonly ProductService _productService;
        [BindProperty(SupportsGet =true)]
        public int Id { get; set; }

        [BindProperty]
        public Product Product { get; set; }
        public DetailModel(ProductService productService)
        {
            this._productService = productService;
        }
        public void OnGet()
        {
            Product = _productService.GetById(Id);
        }
    }
}

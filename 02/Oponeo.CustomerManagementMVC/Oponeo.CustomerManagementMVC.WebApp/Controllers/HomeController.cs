using Microsoft.AspNetCore.Mvc;
using Oponeo.CustomerManagementMVC.Domain.Models;
using Oponeo.CustomerManagementMVC.Services.Products;
using Oponeo.CustomerManagementMVC.WebApp.Models;
using System.Diagnostics;

namespace Oponeo.CustomerManagementMVC.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductService _productService;

        public HomeController(ILogger<HomeController> logger, ProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                Products = _productService.Get().Select(x=> new ProductViewModel
                {
                    Description = x.Description,
                    Name = x.Name,
                    Id = x.Id,
                    Price = x.Price
                }),
                Customer = new()
                {
                    FirstName = "Test",
                    LastName = "Test",
                }
            };
            return View(homeViewModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
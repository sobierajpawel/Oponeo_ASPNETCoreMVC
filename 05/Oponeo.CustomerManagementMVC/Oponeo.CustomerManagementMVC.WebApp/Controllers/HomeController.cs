using Microsoft.AspNetCore.Mvc;
using Oponeo.CustomerManagementMVC.Domain.Models;
using Oponeo.CustomerManagementMVC.Services.Products;
using Oponeo.CustomerManagementMVC.Services.Statistics;
using Oponeo.CustomerManagementMVC.WebApp.Models;
using System.Diagnostics;

namespace Oponeo.CustomerManagementMVC.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductService _productService;
        private readonly ProductStatisticService _productStatisticService;

        public HomeController(ILogger<HomeController> logger, ProductService productService,
            ProductStatisticService productStatisticService)
        {
            _logger = logger;
            _productService = productService;
            _productStatisticService = productStatisticService;
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

        public IActionResult GetGroupedProductTypes()
        {
            var result = _productStatisticService.GetCountedProductTypes();
            return Json(result.Select(x => new
            {
                Key = x.Item1,
                Value = x.Item2
            }));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Oponeo.CustomerManagementMVC.WebApp.Data;
using Oponeo.CustomerManagementMVC.WebApp.Models;
using System.Diagnostics;

namespace Oponeo.CustomerManagementMVC.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly OponeoCustomerManagementMVCWebAppContext _context;

        public HomeController(ILogger<HomeController> logger, OponeoCustomerManagementMVCWebAppContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                Products = _context.Product.Take(3).ToList(),
                Customer = new Customer
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
using Microsoft.AspNetCore.Mvc;
using Oponeo.CustomerManagementMVC.WebApp.Models;

namespace Oponeo.CustomerManagementMVC.WebApp.Controllers
{
    public class CustomerController : Controller
    {

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            return View("Created", customer);
        }

       public IActionResult Details()
       {
            var customer = new Customer
            {
                FirstName = "Adam",
                LastName = "Testowy"
            };
            return View(customer);
       }

        public IActionResult GetClients()
        {
            var list = new List<Customer>
            {
                new Customer
                {
                    FirstName = "Janek",
                    LastName = "Testowy"
                },
                new Customer
                {
                    FirstName = "Adam",
                    LastName = "Testowy"
                }
            };

            return View(list);
        }
    }
}

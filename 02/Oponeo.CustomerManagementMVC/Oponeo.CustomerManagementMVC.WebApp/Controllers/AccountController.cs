using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Oponeo.CustomerManagementMVC.WebApp.Models;
using System.Security.Claims;

namespace Oponeo.CustomerManagementMVC.WebApp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl = null)
        {
            if (ValidateLogin(loginViewModel.Login, loginViewModel.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim("user", loginViewModel.Login),
                    new Claim("role", "creator")
                };

                await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies", "user", "role")));

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return Redirect("/");
                }
            }

            return View();

        }

        private bool ValidateLogin(string userName, string password)
        {
            if (userName == "test@test.pl" && password == "123456")
                return true;
            return false;
        }

        public IActionResult AccessDenied(string returnUrl = null)
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}

## Task 3

### Authorization and authentication in ASP.NET Core MVC

1. Write cookie-based-authentication. Follow the given code and write your own validation. You can create a new domain object and store user logins in the database.

```cs
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
```

2. Create coresponding views, viewmodel for login and access denied. Follow the routing principals.

3. Register authentication in the `Progam.cs` file.

```cs

builder.Services.AddAuthentication("Cookie-authentication-scheme") // Sets the default scheme to cookies
         .AddCookie("Cookie-authentication-scheme", options =>
         {
             options.AccessDeniedPath = "/account/accessdenied";
             options.LoginPath = "/account/login";
         });
```

4. Add Authorization and authentication middlewares

```cs

app.UseAuthentication();
app.UseAuthorization();
```

5. Add `[Authorize]` attribute to controler and particular actions. Try to use ` [Authorize(Roles= "creator")]` and assign different roles to your user. Check how it works.

6. Create a link to log out from application. It should be available for logged users

```html
  @if (User.Identity.IsAuthenticated)
{
<li class="nav-item">
  <a class="nav-link text-dark" asp-area="" asp-controller="Account" asp-action="Logout">Logout</a>
</li>
}
```
                        

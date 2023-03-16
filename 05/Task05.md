## Task 5

###  Create a policy based authorization

1. Create a new requirement to check the minimum age of a user.

```cs
 public class MinimumAgeRequirements : IAuthorizationRequirement
    {
        public int MinimumAge { get; set; }
        public MinimumAgeRequirements(int minimumAge)
        {
            MinimumAge = minimumAge;
        }
    }
```

2. Create handler to deal with this requirement.

```cs
 public class MinimumAgeHandler :
            AuthorizationHandler<MinimumAgeRequirements>
    {
        protected override Task HandleRequirementAsync(
               AuthorizationHandlerContext context,
               MinimumAgeRequirements requirement)
        {
            var user = context.User;
            var claim = context.User.FindFirst("dateBirth");
            if (claim != null)
            {
                if (DateTime.TryParse(claim.Value, out DateTime birthDate))
                {
                    var age = (DateTime.Now - birthDate).TotalDays / 365;
                    if (age >= requirement.MinimumAge)
                        context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }
    }
```

3. Register the handler as a singleton.

```cs
builder.Services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();
```

4. Add a policy to handle different requirements/claims like this.

```cs
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireCreator", policy =>
         policy.RequireRole("creator").RequireClaim("allowedModules","products").
         Requirements.Add(new MinimumAgeRequirements(5)));
});
```

5. Add all claims in your authentication process.

```cs
var claims = new List<Claim>
{
    new Claim("user", loginViewModel.Login),
    new Claim("role", "creator"),
    new Claim("dateBirth",new DateTime(1990,12,29).ToString()),
    new Claim("allowedModules", "products")
 };
```

6. Add autorization attribute and verify how it works.

```cs
[Authorize(Policy = "RequireCreator")]
```

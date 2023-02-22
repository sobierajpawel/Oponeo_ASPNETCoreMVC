## Task 4

### Create Razor Pages project

1. Create a new project and name it as 'Oponeo.CustomerManagement.RazorPages.WebApp'

2. Inspect if the new project builds and runs properly.

3. Add similar module for products (or other connected with the service layer) and create views for CRUD operations. Similar like in the MVC web app project.

4. Remember about registering dependencies and db context like in the following example:

```cs
builder.Services.AddDbContext<OponeoCustomerManagementMVCWebAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OponeoCustomerManagementMVCWebAppContext") 
    ?? throw new InvalidOperationException("Connection string 'OponeoCustomerManagementMVCWebAppContext' not found.")));


builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProductStatisticService>();
```

5. Create views and code-behind classes. The following examples contain logic for get and create product. Use application services like in the MVC web app project.

```cs
 public class IndexModel : PageModel
    {
        private readonly ProductService _productService;

        [BindProperty]
        public IList<Product> Products { get; set; } = default!;
        public IndexModel(ProductService productService)
        {
            this._productService = productService;
        }

        public void OnGet()
        {
            Products = this._productService.Get().ToList();
        }

    }
```

```cs
  public class CreateModel : PageModel
    {
        private readonly ProductService _productService;
        [BindProperty]
        public Product Product { get; set; } = default!;

        public CreateModel(ProductService productService)
        {
            this._productService = productService;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost(Product product)
        {
            if (ModelState.IsValid)
            {
                this._productService.AddOrUpdate(product);
                return RedirectToPage("Products/Index");
            }
            return Page();
        }
    }
```

6. Remember about adding proper navigation for module using `layout.chtml` template.

```html
 <div class="navbar-collapse collapse d-sm-inline-flex justify-content-between">
    <ul class="navbar-nav flex-grow-1">
      <li class="nav-item">
         <a class="nav-link text-dark" asp-area="" asp-page="/Index">Home</a>
      </li>
      <li class="nav-item">
         <a class="nav-link text-dark" asp-area="" asp-page="Products/Index">Products</a>
      </li>
    </ul>
</div>
```

## Task 1

###  Using filters and viewBag

1. Create another type which will be related 1:N with your main entity (for which one you have CRUD operation like products). It might be called `ProductType`.

```cs
public class ProductType
{
    [Key]
    public int Id { get; set; }

    public string Type { get; set; }
}
```

```cs
public class Product
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int ProductTypeId { get; set; }

        public ProductType ProductType { get; set; }
    }
```

2. Create a db set in the EF database context add relation in onModelCreating method.

```cs
  public DbSet<ProductType> ProductType { get; set; } = default!;

  protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductType)
                .WithMany();
            base.OnModelCreating(modelBuilder);
        }
```


3. Add a migration and check if corresponding table will be added to db.

4. Add repository to gain new collection of objects - it should contain only get method.

5. Use service class to retrieve product types from repository.

6. Add ProductTypeId in corresponding viewModel pass it to the entity.

7. In the controller section use ViewBag to provide list of object. Map the collection into `SelectListItem` class.

```cs
  public IActionResult Create()
        {
            ViewBag.ProductTypes = _productService.GetTypes().Select(x => new SelectListItem(x.Type, x.Id.ToString()));
            return View();
        }
```

8. Update a view with select component to choose type of product. Apply it to create and edit view

```html
 <div class="form-group">
  <label asp-for="ProductTypeId" class="control-label"></label>
  <select id="prdTypesList" class="form-control" asp-for="ProductTypeId" asp-items="@ViewBag.ProductTypes">
       <option value="">--Choose product type--</option>
  </select>
</div>
```

9. Add a filter class wchich will be log information when action has been executed like in the following example.

```cs
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ActionLogFilter : Attribute, IActionFilter
    {
        private readonly ILogger _logger;

        public ActionLogFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("ActionLogger");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Do something before the action executes.
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation($"Action '{context.ActionDescriptor.DisplayName}' executed at {DateTime.UtcNow}");
        }
    }
```

10. Register it as a scoped in IoC container and add attribute to some action methods. Go through the website and check how it works.

```cs
   [ServiceFilter(typeof(ActionLogFilter))]
   public async Task<IActionResult> Index()
   {
      ...
   }
```

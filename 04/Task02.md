## Task 2

###  Create a simple WEBAPI project for CRUD operation in 

1. Create ASP.NET Core WEBAPI project. Choose controller-based API to create.

2. Create CRUD operation for `Product` or other entity. You can use the following example

```cs
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly ProductService _productService; 
        public ProductsController(ProductService productService)
        {
            this._productService = productService;
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
        public async Task<IActionResult> Get()
        {
            return Ok(_productService.Get());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(_productService.GetById(id));
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(Product product)
        {
            _productService.AddOrUpdate(product);
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(Product product)
        {
            _productService.AddOrUpdate(product);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            _productService.Remove(id);
            return NoContent();
        }
    }
```

3. Register all necessary dependencies in the built-in IoC container.

```cs
builder.Services.AddDbContext<OponeoCustomerManagementMVCWebAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OponeoCustomerManagementMVCWebAppContext") ?? throw new InvalidOperationException("Connection string 'OponeoCustomerManagementMVCWebAppContext' not found.")));
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
```

4. Try to make PUT/POST with different values (you need to have a foreign key and existed relationship in the EF) and think why using entity is not the best option here.

5. Try to use `DTO` class instead of using entity directly. Use `AutoMapper` or write your mapping classes.

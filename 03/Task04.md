## Task 4

###  Using caching mechanism in ASP.NET MVC

1. Add `Response` cache to your application, first apply proper method in the request pipeline and register a proper service.

```cs
builder.Services.AddResponseCaching();
...
app.UseResponseCaching();
```

2. Add similar header to the one of action methods and add breakpoints inside. Inspect how it will be behave.

```cs
[ResponseCache(Location = ResponseCacheLocation.Client, Duration = 30, VaryByQueryKeys = new[] { "id" })]
```

3. Inject `IMemoryCache` into the controller by using a particular constructor. 

```cs
    private readonly IMemoryCache _memoryCache;

        public ProductsController(ProductService productService, IMemoryCache memoryCache)
        {
            _productService = productService;
            _memoryCache = memoryCache;
        }
```

4. Use `IMemoryCache` in one of action methods like for displaying all data from the database.

```cs
        public async Task<IActionResult> Index()
        {
            if (!_memoryCache.TryGetValue("product_list", out IList<ProductViewModel> productsViewModels))
            {
                productsViewModels = new List<ProductViewModel>();
                foreach (var product in _productService.Get())
                {
                    productsViewModels.Add(new ProductViewModel
                    {
                        Description = product.Description,
                        Name = product.Name,
                        Price = product.Price,
                        Id = product.Id
                    });
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(30));

                _memoryCache.Set("product_list", productsViewModels, cacheEntryOptions);
            }

            return View(productsViewModels);
        }
```

5. Inspect how the whole page will behave.

## Task 3

###  Create a minimal API

1. Create ASP.NET Core WEBAPI project. Uncheck controller-based API to create.

2. Create CRUD operation for `Product` or other entity. You can use the following example

```cs
app.MapGet("/products", (ProductService productService) =>
{
    return productService.Get();
});


app.MapGet("/products/{id}", (int id, ProductService productService) =>
{
    return productService.GetById(id);
})
.WithName("GetProductById");

app.MapPost("/products", (Product product, ProductService productService) =>
{
    productService.AddOrUpdate(product);
    return Results.CreatedAtRoute("GetProductById", new{ Id = product.Id},product);
});

app.MapPut("/products", (Product product, ProductService productService) =>
{
    productService.AddOrUpdate(product);
    return Results.Ok(product);
});

app.MapDelete("/products/{id}", (int id, ProductService productService) =>
{
    productService.Remove(id);
    return Results.NoContent();
});
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

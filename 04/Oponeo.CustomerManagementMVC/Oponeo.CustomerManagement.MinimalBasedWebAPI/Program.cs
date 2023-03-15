using Microsoft.EntityFrameworkCore;
using Oponeo.CustomerManagementMVC.Domain.Models;
using Oponeo.CustomerManagementMVC.Domain.Repositories;
using Oponeo.CustomerManagementMVC.Persistence.Repositories;
using Oponeo.CustomerManagementMVC.Services.Products;
using Oponeo.CustomerManagementMVC.WebApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<OponeoCustomerManagementMVCWebAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OponeoCustomerManagementMVCWebAppContext") ?? throw new InvalidOperationException("Connection string 'OponeoCustomerManagementMVCWebAppContext' not found.")));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
builder.Services.AddScoped<ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/products", (ProductService productService) =>
{
    return productService.Get();
});

app.MapGet("/products/{id}", (int id, ProductService productService) =>
{
    return productService.GetById(id);
}).WithName("GetIdProduct");

app.MapPost("/products", (Product product, ProductService productService) =>
{
    productService.AddOrUpdate(product);
    return Results.CreatedAtRoute("GetIdProduct", new { id = product.Id }, product);
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
}).Produces(204);

app.Run();


using Microsoft.EntityFrameworkCore;
using Oponeo.CustomerManagementMVC.Domain.Repositories;
using Oponeo.CustomerManagementMVC.Persistence.Repositories;
using Oponeo.CustomerManagementMVC.Services.Products;
using Oponeo.CustomerManagementMVC.WebApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<OponeoCustomerManagementMVCWebAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OponeoCustomerManagementMVCWebAppContext") ?? throw new InvalidOperationException("Connection string 'OponeoCustomerManagementMVCWebAppContext' not found.")));


builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductTypeRepository, ProductTypeRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

using Microsoft.EntityFrameworkCore;
using Oponeo.CustomerManagementMVC.Domain.Repositories;
using Oponeo.CustomerManagementMVC.Persistence.Repositories;
using Oponeo.CustomerManagementMVC.Services.Products;
using Oponeo.CustomerManagementMVC.Services.Statistics;
using Oponeo.CustomerManagementMVC.WebApp.Data;
using Oponeo.CustomerManagementMVC.WebApp.Infrastructure;
using Oponeo.CustomerManagementMVC.WebApp.Middleware;
using Oponeo.CustomerManagementMVC.WebApp.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<OponeoCustomerManagementMVCWebAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OponeoCustomerManagementMVCWebAppContext") ?? throw new InvalidOperationException("Connection string 'OponeoCustomerManagementMVCWebAppContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IScopedService, SomeService>();
builder.Services.AddTransient<ITransientService, SomeService>();
builder.Services.AddSingleton<ISingletonService, SomeService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
builder.Services.AddScoped<LoggingFilter>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProductStatisticService>();

builder.Services.AddAuthentication("Cookie-authentication-scheme") // Sets the default scheme to cookies
         .AddCookie("Cookie-authentication-scheme", options =>
         {
             options.AccessDeniedPath = "/account/accessdenied";
             options.LoginPath = "/account/login";
         });

var app = builder.Build();

// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseMiddleware<CustomMiddleware>();

app.Run();

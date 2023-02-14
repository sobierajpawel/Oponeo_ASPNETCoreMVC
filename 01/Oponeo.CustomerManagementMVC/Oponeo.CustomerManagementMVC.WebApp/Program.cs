using Oponeo.CustomerManagementMVC.WebApp.Middleware;
using Oponeo.CustomerManagementMVC.WebApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Oponeo.CustomerManagementMVC.WebApp.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<OponeoCustomerManagementMVCWebAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OponeoCustomerManagementMVCWebAppContext") ?? throw new InvalidOperationException("Connection string 'OponeoCustomerManagementMVCWebAppContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddScoped<IScopedService, SomeService>();
builder.Services.AddTransient<ITransientService, SomeService>();
builder.Services.AddSingleton<ISingletonService, SomeService>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseMiddleware<CustomMiddleware>();

app.Run();

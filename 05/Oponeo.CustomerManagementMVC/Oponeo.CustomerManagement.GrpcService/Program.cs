using Microsoft.EntityFrameworkCore;
using Oponeo.CustomerManagement.GrpcService.Services;
using Oponeo.CustomerManagementMVC.Domain.Repositories;
using Oponeo.CustomerManagementMVC.Persistence.Repositories;
using Oponeo.CustomerManagementMVC.Services.Products;
using Oponeo.CustomerManagementMVC.WebApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();

builder.Services.AddGrpcReflection();

builder.Services.AddDbContext<OponeoCustomerManagementMVCWebAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OponeoCustomerManagementMVCWebAppContext") ?? throw new InvalidOperationException("Connection string 'OponeoCustomerManagementMVCWebAppContext' not found.")));


builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
builder.Services.AddScoped<ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<ProductGrpcService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

IWebHostEnvironment environment = app.Environment;
if(environment.IsDevelopment())
{
    app.MapGrpcReflectionService();
}

app.Run();

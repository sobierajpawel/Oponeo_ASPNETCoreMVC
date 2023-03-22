using Microsoft.EntityFrameworkCore;
using Oponeo.CustomerManagementMVC.Domain;
using Oponeo.CustomerManagementMVC.Domain.Models;

namespace Oponeo.CustomerManagementMVC.WebApp.Data
{
    public class OponeoCustomerManagementMVCWebAppContext : DbContext
    {
        public OponeoCustomerManagementMVCWebAppContext (DbContextOptions<OponeoCustomerManagementMVCWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; } = default!;
        public DbSet<ProductType> ProductType { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductType)
                .WithMany();
            base.OnModelCreating(modelBuilder);
        }
    }
}

using Microsoft.EntityFrameworkCore;
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
    }
}

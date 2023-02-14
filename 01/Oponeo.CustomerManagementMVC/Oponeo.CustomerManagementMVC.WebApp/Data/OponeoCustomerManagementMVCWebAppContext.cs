using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Oponeo.CustomerManagementMVC.WebApp.Models;

namespace Oponeo.CustomerManagementMVC.WebApp.Data
{
    public class OponeoCustomerManagementMVCWebAppContext : DbContext
    {
        public OponeoCustomerManagementMVCWebAppContext (DbContextOptions<OponeoCustomerManagementMVCWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<Oponeo.CustomerManagementMVC.WebApp.Models.Product> Product { get; set; } = default!;
    }
}

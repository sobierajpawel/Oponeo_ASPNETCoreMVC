using Oponeo.CustomerManagementMVC.Domain;
using Oponeo.CustomerManagementMVC.Domain.Repositories;
using Oponeo.CustomerManagementMVC.WebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oponeo.CustomerManagementMVC.Persistence.Repositories
{
    public class ProductTypeRepository : IProductTypeRepository
    {
        private OponeoCustomerManagementMVCWebAppContext _context;

        public ProductTypeRepository(OponeoCustomerManagementMVCWebAppContext context)
        {
            this._context = context;
        }

        public IEnumerable<ProductType> GetAll()
        {
            return this._context.ProductType.ToList();
        }
    }
}

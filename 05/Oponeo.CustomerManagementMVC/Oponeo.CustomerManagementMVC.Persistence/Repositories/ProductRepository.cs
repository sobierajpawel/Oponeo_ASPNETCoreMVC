using Microsoft.EntityFrameworkCore;
using Oponeo.CustomerManagementMVC.Domain.Models;
using Oponeo.CustomerManagementMVC.Domain.Repositories;
using Oponeo.CustomerManagementMVC.WebApp.Data;

namespace Oponeo.CustomerManagementMVC.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly OponeoCustomerManagementMVCWebAppContext _context;

        public ProductRepository(OponeoCustomerManagementMVCWebAppContext context)
        {
            this._context = context;
        }

        public void Add(Product product)
        {
            this._context.Add(product);
            this._context.SaveChanges();
        }

        public void Delete(Product product)
        {
            this._context.Remove(this._context.Product.Where(x => x.Id == product.Id).FirstOrDefault());
            this._context.SaveChanges();
        }

        public IEnumerable<Product> GetAll()
        {
            return this._context.Product.Include(x => x.ProductType);
        }

        public Product GetById(int id)
        {
            return this._context.Product.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public void Update(Product product)
        {
            this._context.Entry<Product>(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this._context.SaveChanges();
        }
    }
}

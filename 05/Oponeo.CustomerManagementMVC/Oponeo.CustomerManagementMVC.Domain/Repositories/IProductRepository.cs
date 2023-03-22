using Oponeo.CustomerManagementMVC.Domain.Models;

namespace Oponeo.CustomerManagementMVC.Domain.Repositories
{
    public interface IProductRepository
    {
        void Add(Product product);
        void Update(Product product);
        IEnumerable<Product> GetAll();

        Product GetById(int id);
        void Delete(Product product);

    }
}

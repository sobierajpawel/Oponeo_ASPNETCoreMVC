using Oponeo.CustomerManagementMVC.Domain.Models;
using Oponeo.CustomerManagementMVC.Domain.Repositories;

namespace Oponeo.CustomerManagementMVC.Services.Products
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService()
        {

        }

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository; 
        }

        public void AddOrUpdate(Product product)
        {
            if (product.Id == 0)
            {
                _productRepository.Add(product);
            }
            else
            {
                _productRepository.Update(product);
            }
        }

        public virtual IEnumerable<Product> Get()
        {
            return _productRepository.GetAll();
        }

        public Product GetById(int id)
        {
            return _productRepository.GetById(id);
        }

        public void Remove(int id)
        {
            _productRepository.Delete(new Product
            {
                Id = id
            });
        }

    }
}

using Oponeo.CustomerManagementMVC.Domain;
using Oponeo.CustomerManagementMVC.Domain.Models;
using Oponeo.CustomerManagementMVC.Domain.Repositories;

namespace Oponeo.CustomerManagementMVC.Services.Products
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductTypeRepository _productTypeRepository;

        public ProductService()
        {

        }

        public ProductService(IProductRepository productRepository, IProductTypeRepository productTypeRepository)
        {
            _productRepository = productRepository;
            _productTypeRepository = productTypeRepository;
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

        public IEnumerable<Product> FindByFilter(string filter)
        {
            var products = _productRepository.GetAll();

            return products.Where(x => x.Name == filter || x.Description == filter);
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

        public IEnumerable<ProductType> GetProductTypes()
        {
            return _productTypeRepository.GetAll();
        }
    }
}

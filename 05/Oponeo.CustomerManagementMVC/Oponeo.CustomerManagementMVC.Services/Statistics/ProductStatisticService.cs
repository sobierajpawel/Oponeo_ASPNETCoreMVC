using Oponeo.CustomerManagementMVC.Domain.Repositories;

namespace Oponeo.CustomerManagementMVC.Services.Statistics
{
    public class ProductStatisticService
    {
        private readonly IProductRepository _productRepository;
        public ProductStatisticService(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        public double GetAveragePrice()
        {
            var products = this._productRepository.GetAll();
            if (products.Any())
            {
                return Math.Round(products.Average(x => x.Price),2);
            }
            return 0;
        }

        public IEnumerable<(string, int)> GetCountedProductTypes()
        {
            var products = this._productRepository.GetAll();

            return products.GroupBy(x => x.ProductType.TypeName)
                .Select(c => (c.Key, c.Count()))
                .ToList();
        }

        public int GetTotalProducts()
        {
            return this._productRepository.GetAll().Count();
        }
    }
}

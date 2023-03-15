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
                return products.Average(x => x.Price);
            }
            return 0;
        }

        public int GetTotalProducts()
        {
            return this._productRepository.GetAll().Count();
        }
    }
}

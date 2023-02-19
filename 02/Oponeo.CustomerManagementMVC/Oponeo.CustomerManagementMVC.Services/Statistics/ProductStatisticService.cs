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
            return this._productRepository.GetAll().Average(x => x.Price);
        }

        public int GetTotalProducts()
        {
            return this._productRepository.GetAll().Count();
        }
    }
}

namespace Oponeo.CustomerManagementMVC.Domain.Repositories
{
    public interface IProductTypeRepository
    {
        IEnumerable<ProductType> GetAll();
    }
}

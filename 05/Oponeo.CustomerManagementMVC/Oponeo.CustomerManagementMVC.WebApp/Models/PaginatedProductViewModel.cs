namespace Oponeo.CustomerManagementMVC.WebApp.Models
{
    public class PaginatedProductViewModel
    {
        public IList<ProductViewModel> ProductViewModels { get; set; } = new List<ProductViewModel>();

        public int TotalPages { get; set; }
    }
}

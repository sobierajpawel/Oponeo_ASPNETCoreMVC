using Oponeo.CustomerManagementMVC.Domain.Models;

namespace Oponeo.CustomerManagementMVC.WebApp.Models
{
    public class HomeViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; }

        public Customer Customer { get; set; }
    }
}

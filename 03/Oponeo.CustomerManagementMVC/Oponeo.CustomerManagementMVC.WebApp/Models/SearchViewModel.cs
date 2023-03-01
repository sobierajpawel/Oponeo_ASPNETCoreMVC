using System.ComponentModel;

namespace Oponeo.CustomerManagementMVC.WebApp.Models
{
    public class SearchViewModel
    {
        [DisplayName("Searched value")]
        public string SearchString { get; set; }
        public IEnumerable<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();
    }
}

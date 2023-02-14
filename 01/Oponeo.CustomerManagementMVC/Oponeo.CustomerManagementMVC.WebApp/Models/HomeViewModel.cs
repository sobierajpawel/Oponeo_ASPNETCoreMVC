namespace Oponeo.CustomerManagementMVC.WebApp.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Product> Products { get; set; }

        public Customer Customer { get; set; }
    }
}

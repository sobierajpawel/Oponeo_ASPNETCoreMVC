using System.ComponentModel.DataAnnotations;

namespace Oponeo.CustomerManagementMVC.WebApp.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}

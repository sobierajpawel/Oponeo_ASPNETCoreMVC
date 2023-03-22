using System.ComponentModel.DataAnnotations;

namespace Oponeo.CustomerManagementMVC.Domain.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }
    }
}

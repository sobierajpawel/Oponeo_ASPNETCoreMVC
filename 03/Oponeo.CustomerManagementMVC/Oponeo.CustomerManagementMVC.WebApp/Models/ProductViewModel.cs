using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Oponeo.CustomerManagementMVC.WebApp.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [StringLength(50, MinimumLength=5)]
        [Required]
        public string Name { get; set; }

        [RegularExpression(@"^[A-Za-z]+$")]
        public string Description { get; set; }

        [DisplayName("Product Price")]
        [Range(1,1000)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public double Price { get; set; }

        public int ProductTypeId { get; set; }

    }
}

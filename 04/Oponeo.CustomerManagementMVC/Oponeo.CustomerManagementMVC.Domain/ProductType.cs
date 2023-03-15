using System.ComponentModel.DataAnnotations;

namespace Oponeo.CustomerManagementMVC.Domain
{
    public class ProductType
    {
        [Key]
        public int Id { get; set; }

        public string TypeName { get; set; }
    }
}

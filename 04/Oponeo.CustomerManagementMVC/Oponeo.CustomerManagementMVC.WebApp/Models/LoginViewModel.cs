using System.ComponentModel.DataAnnotations;

namespace Oponeo.CustomerManagementMVC.WebApp.Models
{
    public class LoginViewModel
    {
        [DataType(DataType.EmailAddress)]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

namespace Oponeo.CustomerManagementMVC.WebApp.Models
{
    public class IocTest
    {
        public Guid ScopedGuid1 { get; set; }
        public Guid ScopedGuid2 { get; set; }

        public Guid TransientGuid1 { get; set; }
        public Guid TransientGuid2 { get; set; }

        public Guid SingletonGuid1 { get; set; }
        public Guid SingletonGuid2 { get; set; }

    }
}

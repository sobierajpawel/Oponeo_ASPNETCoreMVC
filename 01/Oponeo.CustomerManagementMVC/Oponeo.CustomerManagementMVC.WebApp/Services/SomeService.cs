namespace Oponeo.CustomerManagementMVC.WebApp.Services
{
    public class SomeService : ISingletonService, IScopedService, ITransientService
    {
        private Guid guid;
        public SomeService()
        {
            guid = Guid.NewGuid();
        }

        public Guid Guid => guid;
    }
}

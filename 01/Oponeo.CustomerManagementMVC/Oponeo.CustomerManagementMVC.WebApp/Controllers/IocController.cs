using Microsoft.AspNetCore.Mvc;
using Oponeo.CustomerManagementMVC.WebApp.Models;
using Oponeo.CustomerManagementMVC.WebApp.Services;

namespace Oponeo.CustomerManagementMVC.WebApp.Controllers
{
    public class IocController : Controller
    {
        private readonly IScopedService _scopedService1;
        private readonly IScopedService _scopedService2;

        private readonly ITransientService _transientService1;
        private readonly ITransientService _transientService2;

        private readonly ISingletonService _singletonService1;
        private readonly ISingletonService _singletonService2;
        public IocController(IScopedService scopedService1, IScopedService scopedService2, ITransientService transientService1,
            ITransientService transientService2, ISingletonService singletonService1, ISingletonService singletonService2)
        {
            _scopedService1 = scopedService1;
            _scopedService2 = scopedService2;
            _transientService1 = transientService1;
            _transientService2 = transientService2;
            _singletonService1 = singletonService1;
            _singletonService2 = singletonService2;
        }

        public IActionResult Test()
        {
            return View(new IocTest
            {
                ScopedGuid1 = _scopedService1.Guid,
                ScopedGuid2 = _scopedService2.Guid,
                TransientGuid1 = _transientService1.Guid,
                TransientGuid2 = _transientService2.Guid,
                SingletonGuid1 = _singletonService1.Guid,
                SingletonGuid2 = _singletonService2.Guid,
            });
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

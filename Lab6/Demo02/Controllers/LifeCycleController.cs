using Demo02.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo02.Controllers
{
    public class LifeCycleController : Controller
    {
        private readonly ITransientService _transient1;
        private readonly ITransientService _transient2;
        private readonly IScopedService _scoped1;
        private readonly IScopedService _scoped2;
        private readonly ISingletonService _singleton1;
        private readonly ISingletonService _singleton2;

        public LifeCycleController(
            ITransientService transient1, ITransientService transient2,
            IScopedService scoped1, IScopedService scoped2,
            ISingletonService singleton1, ISingletonService singleton2)
        {
            _transient1 = transient1;
            _transient2 = transient2;
            _scoped1 = scoped1;
            _scoped2 = scoped2;
            _singleton1 = singleton1;
            _singleton2 = singleton2;
        }

        public IActionResult Index()
        {
            ViewBag.Transient1 = _transient1.Id;
            ViewBag.Transient2 = _transient2.Id;

            ViewBag.Scoped1 = _scoped1.Id;
            ViewBag.Scoped2 = _scoped2.Id;

            ViewBag.Singleton1 = _singleton1.Id;
            ViewBag.Singleton2 = _singleton2.Id;

            return View();
        }
    }
}

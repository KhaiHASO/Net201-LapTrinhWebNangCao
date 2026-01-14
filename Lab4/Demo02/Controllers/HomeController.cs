using Microsoft.AspNetCore.Mvc;

namespace Demo02.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

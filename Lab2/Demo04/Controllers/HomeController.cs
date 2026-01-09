using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Demo04.Models;

namespace Demo04.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    // Demo Custom Model Binding: ?Ids=1,2,3,4,5
    public IActionResult GetDetails([ModelBinder(typeof(Demo04.Binders.CommaSeparatedModelBinder))] List<int> Ids)
    {
        return Ok(Ids); // Returns JSON array [1, 2, 3, 4, 5]
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

using Demo02.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo02.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public IActionResult Index()
        {
            var activeCustomers = _customerService.GetActiveCustomers();
            return View(activeCustomers);
        }

        [HttpPost]
        public IActionResult UpdateStatus(int id, string status)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                TempData["Error"] = "Status cannot be empty.";
                return RedirectToAction(nameof(Index));
            }

            _customerService.UpdateCustomerStatus(id, status);
            TempData["Success"] = $"Updated customer {id} status to '{status}'.";
            return RedirectToAction(nameof(Index));
        }
    }
}

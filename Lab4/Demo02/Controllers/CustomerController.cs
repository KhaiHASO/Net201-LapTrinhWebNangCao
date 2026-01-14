using Demo02.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo02.Controllers
{
    public class CustomerController : Controller
    {
        private readonly RelationshipContext _context;

        public CustomerController(RelationshipContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Customers.ToListAsync());
        }
    }
}

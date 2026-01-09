using Lab2ModelBinding.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab2ModelBinding.Controllers
{
    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Orders/Filter
        public async Task<IActionResult> Filter([FromQuery] OrderFilterModel filter)
        {
            var query = _context.Orders.Include(o => o.OrderDetails).AsQueryable();

            if (filter.StartDate.HasValue)
            {
                query = query.Where(o => o.OrderDate >= filter.StartDate.Value);
            }

            if (filter.EndDate.HasValue)
            {
                query = query.Where(o => o.OrderDate <= filter.EndDate.Value);
            }

            if (!string.IsNullOrEmpty(filter.Status))
            {
                query = query.Where(o => o.Status == filter.Status);
            }

            var orders = await query.ToListAsync();
            ViewBag.FilterModel = filter;
            return View(orders);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Order order)
        {
            // Manual validation for demonstration if needed, but Model Binding handles nested lists if named correctly
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Filter));
            }
            return View(order);
        }
    }
}

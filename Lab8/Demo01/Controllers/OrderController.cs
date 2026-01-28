using Demo01.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo01.Controllers;

public class OrderController : Controller
{
    private readonly AppDbContext _context;

    public OrderController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // DEMO EAGER LOADING (Slide 7 + 8)
        // Load Orders -> Customer -> Address
        var orders = await _context.Orders
                                   .Include(o => o.Customer)
                                       .ThenInclude(c => c.Addresses) // <--- THEN INCLUDE HERE
                                   .ToListAsync();

        return View(orders);
    }
}

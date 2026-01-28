using Demo03.Data;
using Microsoft.AspNetCore.Mvc;

namespace Demo03.Controllers;

public class CustomerController : Controller
{
    private readonly AppDbContext _context;

    public CustomerController(AppDbContext context)
    {
        _context = context;
    }

    // LIST
    public IActionResult Index()
    {
        // NO Include() required here.
        // Lazy loading is configured, but we only load Customer info here, so NO extra SQL yet.
        var list = _context.Customers.ToList();
        return View(list);
    }

    // DETAILS (Demonstrating LAZY LOADING)
    public IActionResult Details(int id)
    {
        var customer = _context.Customers.Find(id); // Only loads Customer
        // When the View accesses customer.Orders, EF Core Proxy triggers a SQL query to load orders.
        return View(customer);
    }
}

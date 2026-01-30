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

    // DANH SÁCH
    public IActionResult Index()
    {
        // KHÔNG yêu cầu Include() ở đây.
        // Lazy loading đã được cấu hình, nhưng ta chỉ load thông tin Customer ở đây, nên CHƯA CÓ SQL phụ được sinh ra.
        var list = _context.Customers.ToList();
        return View(list);
    }

    // CHI TIẾT (Minh họa LAZY LOADING)
    public IActionResult Details(int id)
    {
        var customer = _context.Customers.Find(id); // Chỉ load Customer
        // Khi View truy cập customer.Orders, EF Core Proxy sẽ kích hoạt một câu lệnh SQL để load orders.
        return View(customer);
    }
}

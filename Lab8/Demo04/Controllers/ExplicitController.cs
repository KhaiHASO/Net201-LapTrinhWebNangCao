using Microsoft.AspNetCore.Mvc;
using Demo04.Data;

namespace Demo04.Controllers
{
    public class ExplicitController : Controller
    {
        private readonly AppDbContext _context;

        public ExplicitController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // CHỈ tải Customers (Khách hàng). Không dùng Include.
            // Explicit Loading yêu cầu chúng ta tải dữ liệu liên quan thủ công sau đó.
            var customers = _context.Customers.ToList();
            return View(customers);
        }

        public IActionResult LoadOrders(int id)
        {
            var customer = _context.Customers.Find(id);

            if (customer == null)
            {
                return NotFound();
            }

            // *** EXPLICIT LOADING (Tải rõ ràng) ***
            // Sử dụng Entry() để lấy entry của thực thể, sau đó dùng Collection() cho thuộc tính điều hướng kiểu collection.
            // Cuối cùng dùng .Load() để thực thi truy vấn lấy dữ liệu liên quan.
            
            _context.Entry(customer)
                    .Collection(c => c.Orders)
                    .Load();

            // Sau dòng này, customer.Orders sẽ có dữ liệu.
            
            return View("Details", customer);
        }
    }
}

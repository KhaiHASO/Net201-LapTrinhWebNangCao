using Demo02.Models;
using Demo02.Services.Calculators;
using Microsoft.AspNetCore.Mvc;

namespace Demo02.Controllers
{
    public class ProductController : Controller
    {
        // Khai báo Interface, không khai báo Class cụ thể (Loose Coupling)
        private readonly ICalculatorService _calculatorService;

        // Constructor Injection
        public ProductController(ICalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }
        

        public IActionResult Index()
        {
            // 1. Tạo dữ liệu giả
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Laptop Gaming FPT", Price = 20000000, Quantity = 1 },
                new Product { Id = 2, Name = "Chuột Không Dây", Price = 500000, Quantity = 2 },
                new Product { Id = 3, Name = "Bàn Phím Cơ", Price = 1500000, Quantity = 1 }
            };

            // 2. Tính tổng tiền bằng Service được Inject
            decimal totalAmount = 0;
            foreach (var p in products)
            {
                totalAmount += _calculatorService.CalculateTotal(p.Price, p.Quantity);
            }

            // 3. Truyền dữ liệu sang View
            ViewBag.TotalAmount = totalAmount;
            ViewBag.PromotionName = _calculatorService.GetPromotionName();

            return View(products);
        }
    }
}

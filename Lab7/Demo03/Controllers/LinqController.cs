using Demo03.Data;
using Demo03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Demo03.Controllers
{
    public class ProductViewModel
    {
        public string Name { get; set; } = "";
        public string Category { get; set; } = "";
        public decimal FormattedPrice { get; set; }
    }

    public class GroupViewModel
    {
        public string CategoryName { get; set; } = "";
        public int ProductCount { get; set; }
        public decimal AveragePrice { get; set; }
    }

    public class LinqController : Controller
    {
        private readonly AppDbContext _context;

        public LinqController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // 1. BasicQuery (Filter & Sort)
        public IActionResult BasicQuery()
        {
            // Price > 100, Order by Name
            var products = _context.Products
                .Where(p => p.Price > 100)
                .OrderBy(p => p.ProductName)
                .ToList();
            
            ViewBag.Title = "Basic Filter (> 100) & Sort";
            return View("ProductList", products);
        }

        // 2. SingleResult (FirstOrDefault)
        public IActionResult SingleResult(int id = 1)
        {
            // Find by ID, handle null
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);

            // Null coalescing operator demo
            // If product is null, create a dummy one (or usually return NotFound)
            var result = product ?? new Product { ProductName = "Not Found", Price = 0 };

            return View(result);
        }

        // 3. Relationships (Include)
        public IActionResult Relationships()
        {
            // Eager loading Category
            var products = _context.Products
                .Include(p => p.Category)
                .ToList();
            
            ViewBag.Title = "Relationships (Include Category)";
            return View("ProductList", products);
        }

        // 4. Grouping
        public IActionResult Grouping()
        {
            var groups = _context.Products
                .GroupBy(p => p.Category.CategoryName)
                .Select(g => new GroupViewModel
                {
                    CategoryName = g.Key,
                    ProductCount = g.Count(),
                    AveragePrice = g.Average(p => p.Price)
                })
                .ToList();

            return View(groups);
        }

        // 5. Projection (Select)
        public IActionResult Projection()
        {
            var viewModels = _context.Products
                .Include(p => p.Category)
                .Select(p => new ProductViewModel
                {
                    Name = p.ProductName,
                    Category = p.Category.CategoryName,
                    FormattedPrice = p.Price // In realapp maybe format string here or view
                })
                .ToList();

            return View(viewModels);
        }

        // 6. Pagination
        public IActionResult Pagination(int pageIndex = 1, int pageSize = 3)
        {
            if (pageIndex < 1) pageIndex = 1;

            var query = _context.Products.OrderBy(p => p.ProductId);
            
            var totalItems = query.Count();
            var products = query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.PageIndex = pageIndex;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = totalItems;
            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            ViewBag.Title = $"Pagination (Page {pageIndex})";

            return View("ProductList", products);
        }

        // 7. Search
        public IActionResult Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return View("ProductList", new List<Product>());
            }

            var products = _context.Products
                .Include(p => p.Category)
                .Where(p => p.ProductName.Contains(keyword))
                .ToList();

            ViewBag.Title = $"Search Result for '{keyword}'";
            return View("ProductList", products);
        }
    }
}

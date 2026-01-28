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

        // 1. BasicQuery (Lọc & Sắp xếp)
        // Ý nghĩa: Tìm các sản phẩm có giá > 100 và sắp xếp theo tên.
        public IActionResult BasicQuery()
        {
            // Câu truy vấn LINQ:
            var products = _context.Products
                .Where(p => p.Price > 100)      // Bước 1: Lọc (Filter) - Chỉ lấy sản phẩm có Price > 100
                .OrderBy(p => p.ProductName)    // Bước 2: Sắp xếp (Sort) - Xếp theo tên sản phẩm tăng dần
                .ToList();                      // Bước 3: Thực thi (Execute) - Chuyển kết quả về danh sách List<Product>
            
            ViewBag.Title = "Basic Filter (> 100) & Sort";
            return View("ProductList", products);
        }

        // 2. SingleResult (Lấy 1 kết quả duy nhất)
        // Ý nghĩa: Tìm sản phẩm theo ID, xử lý trường hợp không tìm thấy.
        public IActionResult SingleResult(int id = 1)
        {
            // Bước 1: Tìm sản phẩm đầu tiên có ProductId trùng với id truyền vào.
            // FirstOrDefault trả về null nếu không tìm thấy.
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);

            // Bước 2: Sử dụng toán tử null-coalescing (??).
            // Nếu product là null (không tìm thấy), tạo một đối tượng giả để hiển thị.
            var result = product ?? new Product { ProductName = "Not Found", Price = 0 };

            return View(result);
        }

        // 3. Relationships (Truy vấn dữ liệu liên quan)
        // Ý nghĩa: Sử dụng Eager Loading để lấy dữ liệu bảng Category cùng với Product.
        public IActionResult Relationships()
        {
            // Câu truy vấn LINQ:
            var products = _context.Products
                .Include(p => p.Category) // Bước 1: Include (JOIN) - Yêu cầu tải kèm dữ liệu Category cho mỗi Product
                .ToList();                // Bước 2: Thực thi
            
            ViewBag.Title = "Relationships (Include Category)";
            return View("ProductList", products);
        }

        // 4. Grouping (Nhóm dữ liệu)
        // Ý nghĩa: Thống kê số lượng và giá trung bình theo từng danh mục.
        public IActionResult Grouping()
        {
            var groups = _context.Products
                .GroupBy(p => p.Category.CategoryName) // Bước 1: Nhóm các sản phẩm theo tên danh mục (CategoryName)
                .Select(g => new GroupViewModel        // Bước 2: Chiếu (Projection) kết quả của từng nhóm ra GroupViewModel
                {
                    CategoryName = g.Key,              // Key của nhóm (chính là CategoryName)
                    ProductCount = g.Count(),          // Đếm số sản phẩm trong nhóm
                    AveragePrice = g.Average(p => p.Price) // Tính giá trung bình trong nhóm
                })
                .ToList(); // Bước 3: Thực thi

            return View(groups);
        }

        // 5. Projection (Chọn lọc/Chuyển đổi dữ liệu)
        // Ý nghĩa: Chỉ lấy các cột cần thiết và map sang ViewModel (Pattern rất hay dùng để tối ưu).
        public IActionResult Projection()
        {
            var viewModels = _context.Products
                .Include(p => p.Category)      // (Có thể bỏ nếu EF Core đủ thông minh, nhưng an toàn thì cứ để)
                .Select(p => new ProductViewModel // Bước 1: Select - Chuyển đổi từ Product entity sang ProductViewModel
                {
                    Name = p.ProductName,            // Lấy tên
                    Category = p.Category.CategoryName, // Lấy tên danh mục
                    FormattedPrice = p.Price         // Lấy giá
                })
                .ToList(); // Bước 2: Thực thi. Câu SQL sinh ra chỉ SELECT các cột này, không SELECT * (tối ưu).

            return View(viewModels);
        }

        // 6. Pagination (Phân trang)
        // Ý nghĩa: Chia nhỏ dữ liệu ra nhiều trang để hiển thị.
        public IActionResult Pagination(int pageIndex = 1, int pageSize = 3)
        {
            if (pageIndex < 1) pageIndex = 1; // Kiểm tra trang đầu tiên

            // Bước 1: Tạo câu truy vấn cơ sở và sắp xếp (bắt buộc phải Sort trước khi Skip/Take)
            var query = _context.Products.OrderBy(p => p.ProductId);      
            // Bước 2: Đếm tổng số để tính số trang
            var totalItems = query.Count();

            // Bước 3: Áp dụng công thức phân trang
            //lấy ra trang số 2, => bỏ qua trang số 1 =>bỏ đi 1*3 phần tử
            //lấy ra trang số 10, => bỏ qua 9 trang
            //=> số phấn tử bỏ qua là 9*3 
            var products = query
                .Skip((pageIndex - 1) * pageSize) // Bỏ qua (pageIndex - 1) * pageSize bản ghi đầu
                .Take(pageSize)                   // Lấy pageSize bản ghi tiếp theo
                .ToList();                        // Thực thi

            ViewBag.PageIndex = pageIndex;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = totalItems;
            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            ViewBag.Title = $"Pagination (Page {pageIndex})";

            return View("ProductList", products);
        }

        // 7. Search (Tìm kiếm)
        // Ý nghĩa: Tìm sản phẩm theo từ khoá.
        public IActionResult Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return View("ProductList", new List<Product>());
            }

            var products = _context.Products
                .Include(p => p.Category)
                .Where(p => p.ProductName.Contains(keyword)) // Bước 1: Lọc sản phẩm có tên chứa từ khoá (LIKE %keyword%)
                .ToList();
            
            ViewBag.Title = $"Search Result for '{keyword}'";
            return View("ProductList", products);
        }
    }
}

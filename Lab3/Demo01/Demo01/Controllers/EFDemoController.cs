using Demo01.Data;
using Demo01.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text;

namespace Demo01.Controllers
{
    public class EFDemoController : Controller
    {
        public IActionResult Index()
        {
            var sb = new StringBuilder();
            sb.AppendLine("=== Demo 01: EF Core in MVC ===");

            // 1. Khởi tạo Context
            using (var context = new SchoolContext()) // Tự khởi tạo vì dùng OnConfiguring
            {
                // Reset DB cho sạch
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated(); // Tạo mới DB dựa trên DbSet
                sb.AppendLine("Database (re)created.");

                // 2. Insert (Thêm)
                sb.AppendLine("\n--- Demo ADD ---");
                var s1 = new Student { Name = "Nguyen Van A" };
                var s2 = new Student { Name = "Tran Thi B" };
                
                context.Students.Add(s1);
                context.Add(s2);

                context.SaveChanges();
                sb.AppendLine($"Added Student 1 ID: {s1.Id}");
                sb.AppendLine($"Added Student 2 ID: {s2.Id}");

                // 3. Find (Tìm kiếm)
                sb.AppendLine("\n--- Demo FIND ---");
                var found = context.Students.Find(s1.Id);
                sb.AppendLine($"Found Student ID {s1.Id}: {found.Name}");

                // 4. Update (Sửa)
                sb.AppendLine("\n--- Demo UPDATE ---");
                if (found != null)
                {
                    found.Name = "Nguyen Van A (Updated)";
                    context.SaveChanges(); // Tự phát hiện thay đổi
                    sb.AppendLine("Updated Student Name.");
                }

                // 5. Remove (Xoá)
                sb.AppendLine("\n--- Demo REMOVE ---");
                context.Students.Remove(found);
                context.SaveChanges();
                sb.AppendLine("Removed Student.");

                // 6. Kiểm tra lại
                var count = context.Students.Count();
                sb.AppendLine($"Remaining students count: {count}");
            }

            // Trả về kết quả dạng text đơn giản để dễ xem
            return Content(sb.ToString(), "text/plain", Encoding.UTF8);
        }
    }
}

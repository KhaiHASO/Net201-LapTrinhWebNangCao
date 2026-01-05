using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers
{
    public class AccountController : Controller
    {
        // 2. Thử nghiệm trả về ContentResult, JsonResult
        public IActionResult ContentResultDemo()
        {
            return Content("Đây là ví dụ về ContentResult. Trả về một chuỗi văn bản thuần túy.", "text/plain; charset=utf-8");
        }

        public IActionResult JsonResultDemo()
        {
            var data = new
            {
                Id = 1,
                Name = "Nguyễn Văn A",
                Email = "nguyenvana@example.com",
                Date = DateTime.Now
            };
            return Json(data);
        }

        // 3. Tạo Action chuyển hướng trang (Redirect)
        public IActionResult RedirectDemo()
        {
            // Chuyển hướng đến Action ContentResultDemo
            return RedirectToAction("ContentResultDemo");
        }

        // 4. Thực hành trả về File ảnh từ thư mục wwwroot
        public IActionResult FileDemo()
        {
            // Đảm bảo có file image.png trong thư mục wwwroot/images/
            string filePath = "~/images/sample.png";
            return File(filePath, "image/png");
        }
    }
}

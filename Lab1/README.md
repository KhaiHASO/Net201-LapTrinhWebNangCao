# Lab 1 - ASP.NET Core MVC Basics

Bài tập Lab 1 môn Lập Trình Web Nâng Cao.

## Yêu cầu
1. Tạo một ASP.NET Core MVC Project mới.
2. Thử nghiệm trả về `ContentResult`, `JsonResult`.
3. Tạo Action chuyển hướng trang (`Redirect`).
4. Thực hành trả về File ảnh từ thư mục `wwwroot`.

## Hướng dẫn chi tiết

### 1. Tạo Project
Project được tạo bằng lệnh:
```bash
dotnet new mvc -n Lab1
```

### 2. Các Action trong `AccountController`
File: `Controllers/AccountController.cs`

#### a. ContentResultDemo
Trả về một chuỗi văn bản thuần túy (plain text).
- URL: `/Account/ContentResultDemo`
- Code:
```csharp
public IActionResult ContentResultDemo()
{
    return Content("Đây là ví dụ về ContentResult. Trả về một chuỗi văn bản thuần túy.", "text/plain");
}
```

#### b. JsonResultDemo
Trả về dữ liệu dạng JSON. Thường dùng cho các API.
- URL: `/Account/JsonResultDemo`
- Code:
```csharp
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
```

#### c. RedirectDemo
Chuyển hướng người dùng sang một Action khác.
- URL: `/Account/RedirectDemo`
- Kết quả: Chuyển hướng sang `/Account/ContentResultDemo`
- Code:
```csharp
public IActionResult RedirectDemo()
{
    return RedirectToAction("ContentResultDemo");
}
```

#### d. FileDemo
Trả về file ảnh từ thư mục `wwwroot/images/`.
- URL: `/Account/FileDemo`
- Code:
```csharp
public IActionResult FileDemo()
{
    string filePath = "~/images/sample.png";
    return File(filePath, "image/png");
}
```

### 3. Cấu trúc thư mục
- `Controllers/AccountController.cs`: Chứa các action demos.
- `wwwroot/images/sample.png`: Ảnh mẫu để test FileDemo.

## Cách chạy
1. Mở terminal tại thư mục `Lab1`.
2. Chạy lệnh: `dotnet run`
3. Truy cập các đường dẫn trên trình duyệt (ví dụ: `http://localhost:5000/Account/ContentResultDemo`).

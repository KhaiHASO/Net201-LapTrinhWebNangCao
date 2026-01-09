# Demo01 - Model Binding trong ASP.NET Core MVC

Dự án này là tài liệu minh họa (Demo) cho kỹ thuật **Model Binding** trong ASP.NET Core MVC, cụ thể là cách binding dữ liệu từ Form HTML sang Model C# mạnh mẽ và chính xác.

## Mục Tiêu

Demo này giúp hiểu rõ:
1.  **Model Binding**: Tự động ánh xạ dữ liệu từ HTTP Request (Form data) vào Object `User`.
2.  **[FromForm]**: Chỉ định rõ nguồn dữ liệu binding là từ body của request (form post).
3.  **Naming Convention**: Quy tắc đặt tên `name` trong HTML phải khớp với Property trong Model.
4.  **Validation**: Sử dụng Data Annotations và `ModelState.IsValid`.
5.  **Tag Helpers**: Sử dụng `asp-for` để sinh HTML và binding tự động.

## Cấu Trúc Dự Án

- **Models/User.cs**: Class đại diện cho dữ liệu người dùng. Chứa các quy tắc Validation (Required, EmailAddress...).
- **Controllers/UsersController.cs**:
    - `Create (GET)`: Chuẩn bị dữ liệu (ViewBag) và hiển thị form.
    - `Create (POST)`: Xử lý dữ liệu gửi lên. Sử dụng `[FromForm]` và kiểm tra `ModelState.IsValid`.
- **Views/Users/Create.cshtml**: Giao diện đăng ký sử dụng Bootstrap 5.

## Hướng Dẫn Chạy Demo

Đảm bảo  đã cài đặt .NET SDK (tương thích .NET 6.0 trở lên).

1.  Mở terminal tại thư mục `Demo01`.
2.  Chạy lệnh build để khôi phục các gói và biên dịch:
    ```bash
    dotnet build
    ```
3.  Chạy ứng dụng:
    ```bash
    dotnet run
    ```
4.  Truy cập địa chỉ hiển thị trên terminal (thường là `http://localhost:5xxx` hoặc `https://localhost:7xxx`) và điều hướng tới:
    ```
    /Users/Create
    ```

## Chi Tiết Kỹ Thuật (Lưu Ý Quan Trọng)

### 1. Thuộc tính `[FromForm]`
Trong `UsersController.cs`:
```csharp
[HttpPost]
public IActionResult Create([FromForm] User user)
{
    // ...
}
```
Attribute `[FromForm]` báo cho hệ thống biết rằng dữ liệu của `user` sẽ được lấy từ **Form Data** của request body. Điều này giúp tránh nhầm lẫn nguồn dữ liệu (như Query String hay Route Data).

### 2. Quy Tắc Ánh Xạ Tên (Naming Convention)
Để Model Binding hoạt động, thuộc tính `name` của thẻ HTML Input phải khớp chính xác với tên Property trong Model.
Ví dụ:
- Model: `public List<string> Hobbies { get; set; }`
- View (HTML):
  ```html
  <input type="checkbox" name="Hobbies" value="Reading" />
  ```
Nếu tên không khớp (ví dụ `name="hobby"`), List Hobbies sẽ rỗng.

### 3. Kiểm Tra Tính Hợp Lệ (Validation)
Luôn kiểm tra `ModelState.IsValid` trước khi xử lý logic nghiệp vụ:
```csharp
if (ModelState.IsValid) {
    // Dữ liệu OK, lưu vào DB...
}
```
Hệ thống tự động kiểm tra các attribute như `[Required]`, `[EmailAddress]` trên Model `User` và điền lỗi vào `ModelState` nếu có.

### 4. Xử Lý List Checkbox
Demo minh họa cách bind một danh sách các checkbox vào `List<string>`. Chỉ các checkbox được tích chọn mới gửi giá trị `value` về server, và Model Binding sẽ gom chúng vào List.

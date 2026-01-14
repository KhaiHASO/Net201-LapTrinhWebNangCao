# Demo 03: Xây dựng chức năng CRUD (Thêm, Xóa, Sửa) với EF Core Code First và Razor View

## Yêu cầu môi trường
- Visual Studio 2022 hoặc .NET SDK tương thích
- SQL Server LocalDB

## Kịch bản Demo từng bước

### Bước 1: Cài đặt gói NuGet
Trong Visual Studio Package Manager Console hoặc `dotnet add package`:
- `Microsoft.EntityFrameworkCore.SqlServer`
- `Microsoft.EntityFrameworkCore.Tools`

### Bước 2: Định nghĩa Model và DbContext
- Tạo `Models/Product.cs` với các thuộc tính `Id`, `Name`, `Price`, `Quantity`, `Status`.
- Tạo `Models/CompanyContext.cs` kế thừa `DbContext`, khai báo `DbSet<Product> Products`.
- Cấu hình chuỗi kết nối trong `appsettings.json`:
  ```
  Server=(localdb)\\mssqllocaldb;Database=Slide4Demo03;Trusted_Connection=True;MultipleActiveResultSets=true
  ```

### Bước 3: Tạo database bằng Migration
Trong Package Manager Console:
1. `Add-Migration InitialCreate`
2. `Update-Database`
Kết quả sẽ tạo database `Slide4Demo03` từ mô hình Code First.

### Bước 4: Giải thích nhanh controller CRUD
- `Index`: lấy danh sách `_context.Products.ToList()` và hiển thị bảng.
- `Add` (GET/POST): hiển thị form; khi submit, thêm bản ghi bằng `_context.Products.Add(product); _context.SaveChanges();`.
- `Edit` (GET/POST): tìm bản ghi với `_context.Products.Find(id)`; cập nhật với `_context.Products.Update(product); _context.SaveChanges();`.
- `Delete` (GET/POST): nạp bản ghi cần xóa và thực hiện `_context.Products.Remove(product); _context.SaveChanges();`.

### Bước 5: Chạy ứng dụng và kiểm thử
1. Thiết lập dự án khởi chạy `demo03`.
2. Chạy ứng dụng (`F5` hoặc `dotnet run`).
3. Kiểm tra lần lượt:
   - Thêm sản phẩm mới tại `/Product/Add`.
   - Sửa sản phẩm tại `/Product/Edit/{id}`.
   - Xóa sản phẩm tại `/Product/Delete/{id}`.
   - Xem danh sách tại `/Product/Index`.

## Khắc phục sự cố thường gặp
- **Sai chuỗi kết nối**: Đảm bảo đúng server LocalDB và tên DB `Slide4Demo03`.
- **Migration lỗi**: Kiểm tra đã cài đủ gói Tools; chạy lại `Add-Migration` sau khi xóa thư mục `Migrations` cũ nếu cần.
- **Không áp dụng thay đổi DB**: Chạy lại `Update-Database` và chắc chắn dự án đang trỏ đúng `appsettings.json`.


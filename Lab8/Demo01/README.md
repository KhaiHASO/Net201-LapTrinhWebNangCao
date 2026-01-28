# Demo01: Eager Loading (NET201)

Dự án Demo cho bài giảng Slide 8 môn NET201 (Lập trình Web Nâng cao) tại FPT Polytechnic.

## 1. Giới thiệu
Ứng dụng minh họa kỹ thuật **Eager Loading** trong Entity Framework Core, sử dụng method `.Include()` để load dữ liệu bảng liên quan (Customer) khi truy vấn bảng chính (Order).

- **Công nghệ**: ASP.NET Core MVC (.NET 10/8), EF Core, SQL Server.
- **Giao diện**: Bootstrap 5.

## 2. Chuẩn bị
- Visual Studio 2022+ hoặc VS Code.
- SQL Server (LocalDB mặc định).
- .NET 8 SDK (hoặc mới hơn).

## 3. Cách chạy dự án
Ứng dụng đã được cấu hình **Auto-Migration**, bạn không cần chạy lệnh update database thủ công.

1.  Mở terminal tại thư mục `Demo01`.
2.  Chạy lệnh:
    ```bash
    dotnet run
    ```
3.  Truy cập browser tại: `http://localhost:5xxx` (Port hiển thị trên terminal).

## 4. Tự tạo Migration (Nếu cần)
Nếu bạn thay đổi Database Model, hãy chạy lệnh sau để cập nhật migration:

```bash
dotnet ef migrations add <TenMigration>
dotnet ef database update
```
(Tuy nhiên code hiện tại đã tự động gọi `Migrate()` khi khởi động).

## 5. Cấu trúc Source Code
- **Models/Customer.cs**: Entity Khách Hàng.
- **Models/Order.cs**: Entity Đơn Hàng.
- **Models/Address.cs**: Entity Địa Chỉ (Quan hệ 1-N với Customer).
- **Data/AppDbContext.cs**: Cấu hình Seed Data (2 Customer, 3 Addresses, 4 Orders).
- **Controllers/OrderController.cs**: Chứa logic `.Include(x => x.Customer).ThenInclude(x => x.Addresses)`.
- **Views/Order/Index.cshtml**: Hiển thị bảng dữ liệu, bao gồm thông tin Address.

## 6. Kịch bản Demo (Scenario)

### Ý nghĩa của Eager Loading & ThenInclude
- **Include**: Load bảng liên quan trực tiếp (Order -> Customer).
- **ThenInclude**: Load bảng liên quan cấp tiếp theo (Customer -> Address).

### Các bước kiểm chứng
1.  **Chạy ứng dụng** (`dotnet run`).
2.  Nhìn cột **"Tên Khách Hàng"**: Hiển thị đựoc nhờ `.Include(o => o.Customer)`.
3.  Nhìn cột **"Địa Chỉ"**: Hiển thị được (Ví dụ: "Hà Nội", "Đà Nẵng") nhờ `.ThenInclude(c => c.Addresses)`.

### Thử nghiệm ngược lại (Optional)
1.  Vào `OrderController.cs`, xóa bỏ `.ThenInclude(...)`.
2.  Chạy lại ứng dụng.
3.  Cột "Địa Chỉ" sẽ không hiển thị gì (hoặc N/A) do `Addresses` collection của Customer bị rỗng (null).

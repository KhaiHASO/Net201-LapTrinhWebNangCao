# Hướng Dẫn Kịch Bản Demo 01: EF Core Code First & Migrations

Dự án này là một ví dụ minh họa cách sử dụng Entity Framework Core với phương pháp Code-First trong ASP.NET Core MVC.

## 1. Tổng Quan
Dự án **demo01** xây dựng một ứng dụng quản lý thông tin công ty đơn giản với các chức năng:
- Tạo cơ sở dữ liệu từ Code (Code-First).
- Quản lý Migration.
- CRUD cơ bản (Xem danh sách, Thêm mới).

## 2. Yêu Cầu Cài Đặt (Prerequisites)
Để chạy demo này, Sinh viên cần chuẩn bị:
- **Visual Studio 2026** (hoặc VS Code).
- **.NET SDK 10** (hoặc mới hơn).
- **SQL Server LocalDB** (được cài đặt mặc định cùng Visual Studio).

## 3. Kịch Bản Demo (Step-by-Step Script)

Sinh viên thực hiện các bước sau để minh họa:

### Bước 1: Cài đặt NuGet Packages
Mở **Package Manager Console** (hoặc Terminal) và chạy lệnh sau để cài đặt các thư viện cần thiết cho EF Core và SQL Server:

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

*Giải thích:* 
- `Microsoft.EntityFrameworkCore.SqlServer`: Provider cho SQL Server.
- `Microsoft.EntityFrameworkCore.Tools`: Các công cụ để chạy lệnh Migration.

### Bước 2: Tạo và Chạy Migrations
Sau khi viết code Model và Context, thực hiện lệnh tạo Migration đầu tiên:

1. **Tạo Migration:**
   ```bash
   dotnet ef migrations add InitialCreate
   ```
   *Giải thích:* Lệnh này sẽ quét các `DbSet` trong `CompanyContext` và tạo ra các file C# mô tả sự thay đổi của Database.

2. **Cập nhật Database:**
   ```bash
   dotnet ef database update
   ```
   *Giải thích:* Lệnh này thực thi các file Migration để tạo bảng thực tế trong SQL Server LocalDB.

### Bước 3: Chạy Ứng Dụng và Kiểm Tra
1. **Chạy ứng dụng:**
   Nhấn **F5** hoặc chạy lệnh:
   ```bash
   dotnet run
   ```

2. **Truy cập:**
   Mở trình duyệt và vào đường dẫn: `https://localhost:7086/Information` (Port có thể thay đổi, kiểm tra console).

3. **Kiểm tra dữ liệu:**
   - Thêm mới một bản ghi Công ty.
   - Mở **SQL Server Object Explorer** trong Visual Studio.
   - Kết nối tới `(localdb)\mssqllocaldb`.
   - Tìm Database `Slide4Demo01` -> Tables -> `Informations`.
   - Chuột phải -> **View Data** để chứng minh dữ liệu đã được lưu xuống DB.

## 4. Troubleshooting (Khắc phục lỗi)

**Lỗi Kết Nối Database (Connection String):**
Nếu gặp lỗi khi chạy lệnh `update-database`:
- Kiểm tra lại Connection String trong `appsettings.json`.
- Đảm bảo LocalDB đã được cài đặt. Có thể kiểm tra bằng lệnh CMD: `sqllocaldb info`.
- Nếu LocalDB chưa chạy, hãy start nó: `sqllocaldb start mssqllocaldb`.

**Lỗi "Build Failed":**
- Đảm bảo đã lưu tất cả các file.
- Chạy lệnh `dotnet build` để xem chi tiết lỗi biên dịch.

# Demo 02: Tạo Web với Entity Framework Core (Database First)

Demo này hướng dẫn quy trình xây dựng ứng dụng ASP.NET Core MVC làm việc với cơ sở dữ liệu có sẵn (Database First).

## 1. Mục tiêu
*   Hiểu cách sử dụng **Database First** để sinh code từ Database có sẵn.
*   Biết cách dùng lệnh `dotnet ef dbcontext scaffold` (Reverse Engineering).
*   Thực hành tạo chức năng CRUD (Thêm, Xem, Sửa, Xoá) cho bảng `Products`.

## 2. Chuẩn bị (Database) (Quan trọng)
Demo này yêu cầu bạn phải có sẵn Database. Hãy chạy Script SQL dưới đây trong **SQL Server Management Studio (SSMS)**:

```sql
USE master;
GO
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Inventory')
BEGIN
    CREATE DATABASE Inventory;
END
GO
USE Inventory;
GO
CREATE TABLE Products (
    ProductId BIGINT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(100),
    Category VARCHAR(100),
    Color VARCHAR(20),
    UnitPrice DECIMAL(18, 2),
    AvailableQuantity BIGINT,
    CreatedDate DATETIME DEFAULT GETDATE()
);

-- Thêm dữ liệu mẫu
INSERT INTO Products (Name, Category, Color, UnitPrice, AvailableQuantity)
VALUES 
('Laptop Dell XPS 13', 'Computers', 'Silver', 25000000, 10),
('MacBook Pro M2', 'Computers', 'Space Gray', 45000000, 5),
('Iphone 15 Pro Max', 'Phones', 'Titanium', 30000000, 20);
```

## 3. Tạo Project & Cài đặt (Hướng dẫn lại các bước đã làm)

### Bước 1: Tạo Project MVC
```bash
dotnet new mvc -n Demo02
```

### Bước 2: Cài đặt thư viện (NuGet)
Để chạy được các lệnh `dotnet ef`, bạn cần cài đặt:
1.  `Microsoft.EntityFrameworkCore.SqlServer` (Provider cho SQL)
2.  `Microsoft.EntityFrameworkCore.Tools` (Chứa các lệnh CLI)
3.  `Microsoft.EntityFrameworkCore.Design` (Hỗ trợ Design-time)
4.  `Microsoft.VisualStudio.Web.CodeGeneration.Design` (Hỗ trợ sinh Controller)

Lệnh cài đặt:
```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
```

## 4. Hướng dẫn Scaffolding (Quan trọng)

Đây là bước giúp bạn sinh code (Model Classes + DbContext) từ Database.

### Lệnh Scaffold-DbContext (Reverse Engineering)
Mở Terminal tại thư mục gốc của project (Demo02) và chạy lệnh sau:

```bash
dotnet ef dbcontext scaffold "Server=(localdb)\MSSQLLocalDB;Database=Inventory;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer --output-dir Models --context InventoryContext --force
```

**Giải thích:**
*   `"Server=..."`: Chuỗi kết nối tới SQL Server của bạn.
*   `Microsoft.EntityFrameworkCore.SqlServer`: Chỉ định dùng SQL Server Provider.
*   `--output-dir Models`: Sinh các file class (`Product.cs`, `InventoryContext.cs`) vào thư mục `Models`.
*   `--context InventoryContext`: Đặt tên cho DbContext là `InventoryContext`.
*   `--force`: Ghi đè file nếu đã tồn tại.

## 5. Cấu hình Chuỗi Kết Nối & DI

Sau khi scaffold, bạn cần chuyển connection string vào `appsettings.json` để bảo mật và quản lý dễ hơn.

1.  **appsettings.json**:
    ```json
    "ConnectionStrings": {
      "InventoryDatabase": "Server=(localdb)\\MSSQLLocalDB;Database=Inventory;Trusted_Connection=True;TrustServerCertificate=True"
    }
    ```

2.  **Program.cs**: Đăng ký DbContext vào hệ thống.
    ```csharp
    using Demo02.Models;
    using Microsoft.EntityFrameworkCore;

    // ...
    builder.Services.AddDbContext<InventoryContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("InventoryDatabase")));
    ```

## 6. Tạo Controller & Giao diện (Scaffolding Controller)

Bạn có thể dùng công cụ sinh code để tạo nhanh Controller + Views CRUD:

```bash
dotnet aspnet-codegenerator controller -name ProductsController -m Product -dc InventoryContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries
```
*(Nếu gặp lỗi chưa cài tool, hãy cài: `dotnet tool install -g dotnet-aspnet-codegenerator`)*

**Hoặc dùng Visual Studio:**
1.  Chuột phải folder **Controllers** -> **Add** -> **Controller**.
2.  Chọn **MVC Controller with views, using Entity Framework**.
3.  Model class: `Product`.
4.  Data context class: `InventoryContext`.

## 7. Chạy Demo
1.  Mở Terminal.
2.  Chạy lệnh: `dotnet run`.
3.  Truy cập: `http://localhost:5xxx/Products`.
4.  Bạn sẽ thấy danh sách sản phẩm lấy từ Database. Hãy thử thêm, sửa, xoá để kiểm chứng.

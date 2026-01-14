# Demo 02: EF Core Conventions & Relationships

Dự án này là bài thực hành minh họa về **Code First Conventions** (Quy tắc ngầm định) và **Mối quan hệ giữa các bảng** (Relationships) trong Entity Framework Core 10.

## 1. Nội Dung Kiến Thức
Dự án minh họa các khái niệm sau:

### a. Conventions (Quy tắc ngầm định)
- **Primary Key:** EF Core tự động nhận diện thuộc tính `ID` hoặc `[ClassName]ID` là khóa chính (VD: `CustomerID` trong class `Customer`).
- **Foreign Key:** Nếu có thuộc tính tên là `[NavigationPropertyName]ID` (VD: `DepartmentID` trong class `Employee`), EF Core tự động hiểu đó là Khóa ngoại trỏ tới bảng `Department`.
- **Navigation Property:** Các thuộc tính như `Department.Employees` (1-N) hay `Employee.Department` (N-1) giúp truy vấn dữ liệu chéo giữa các bảng.

### b. Relationships (Quan hệ)
- **One-to-Many (1-N):** Một Phòng ban (`Department`) có nhiều Nhân viên (`Employee`), nhưng một Nhân viên chỉ thuộc về một Phòng ban.

## 2. Yêu Cầu Cài Đặt
- **Visual Studio 2026** (hoặc VS Code).
- **.NET SDK 10**.
- **SQL Server LocalDB**.

## 3. Hướng Dẫn Kịch Bản Demo

### Bước 1: Cài đặt NuGet Packages
Nếu chưa có (dự án này đã cài sẵn), chạy lệnh:
```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

### Bước 2: Tạo Migration & Database
Mở Terminal tại thư mục gốc của dự án (`Demo02`) và chạy lệnh:

1.  **Tạo Migration:**
    ```bash
    dotnet ef migrations add RelationDB
    ```
    *Giải thích:* Lệnh này tạo ra code C# để ánh xạ các class `Customer`, `Department`, `Employee` thành các bảng SQL. Hãy mở file Migration vừa tạo để cho sinh viên thấy EF Core đã tự nhận diện FK và PK như thế nào.

2.  **Cập nhật Database:**
    ```bash
    dotnet ef database update
    ```
    *Giải thích:* Lệnh này thực thi Migration và chèn dữ liệu mẫu (Seeding Data) đã được viết trong hàm `OnModelCreating`.

### Bước 3: Chạy và Kiểm thử
1.  Chạy ứng dụng:
    ```bash
    dotnet run
    ```
2.  Truy cập trang chủ (Dashboard).
3.  Vào mục **"Khách Hàng"**: Giải thích việc `CustomerID` tự động là PK.
4.  Vào mục **"Phòng Ban & Nhân Viên"**:
    - Chỉ cho sinh viên thấy dữ liệu Nhân viên được gom nhóm theo Phòng ban.
    - Giải thích code `Index` trong `DepartmentController` đã dùng `.Include(d => d.Employees)` để lấy dữ liệu liên quan (Eager Loading).
    - Nếu không có `.Include`, danh sách nhân viên sẽ bị rỗng (null).

## 4. Xử Lý Sự Cố
- **Lỗi Migration:** Nếu báo lỗi build, hãy đảm bảo bạn đã lưu tất cả các file.
- **Lỗi SQL:** Kiểm tra chuỗi kết nối trong `appsettings.json`.

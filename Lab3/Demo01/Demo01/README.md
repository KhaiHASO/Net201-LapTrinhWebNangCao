# Demo 01: Tạo EF Core Project và Phân tích DbContext, DbSet

Demo này tích hợp trực tiếp vào dự án Web MVC để minh hoạ các thao tác cơ bản của Entity Framework Core.

## 1. Cài đặt

Dự án đã được cài hai gói NuGet:
*   `Microsoft.EntityFrameworkCore.SqlServer`
*   `Microsoft.EntityFrameworkCore.Tools`

Bạn có thể kiểm tra trong file `Demo01.csproj`.

## 2. Các thành phần Code Demo

Bộ code demo nằm rải rác trong cấu trúc MVC như sau:

*   **Models (`/Models`)**: Chứa 2 Entity đơn giản là `Student` và `Course`.
*   **Context (`/Data/SchoolContext.cs`)**:
    *   Kế thừa từ `DbContext`.
    *   Khai báo `DbSet<Student>` và `DbSet<Course>`.
    *   Sử dụng `OnConfiguring` để kết nối tới SQL Server (LocalDB).
*   **Hoạt động (`/Controllers/EFDemoController.cs`)**:
    *   Thay vì dùng Console.WriteLine, controller này thực hiện các bước Add, Find, Update, Remove và trả về kết quả dạng Text ngay trên trình duyệt.

## 3. Phân tích Khái Niệm

### DbContext là gì?
`SchoolContext` đóng vai trò là **unit of work** cho cơ sở dữ liệu.
*   **Change Tracking**: Khi bạn lấy `found` student bằng `Find`, Context theo dõi nó. Việc gán `found.Name = ...` sẽ tự động được phát hiện.
*   **Persisting**: `SaveChanges()` là lệnh duy nhất thực sự gửi SQL xuống database.

### DbSet là gì?
`DbSet` giống như một collection trong bộ nhớ, nhưng đại diện cho bảng trong Database.
*   `context.Students.Add(...)` thêm vào bảng Students.
*   `context.Students.Remove(...)` xoá khỏi bảng Students.

## 4. Cách chạy Demo
1.  Chạy dự án web (`dotnet run`).
2.  Truy cập vào đường dẫn: `/EFDemo` trên trình duyệt.
3.  Bạn sẽ thấy log chi tiết quá trình tạo DB và thao tác dữ liệu.

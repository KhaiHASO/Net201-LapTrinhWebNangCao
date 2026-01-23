# Demo02 - Stored Procedures in Entity Framework Core (N-Tier Architecture)

Dự án này minh họa cách gọi Stored Procedures trong EF Core thông qua mô hình kiến trúc phân tầng (Repository Pattern & Service Pattern).
Hệ thống quản lý trạng thái khách hàng (Customer Status) sử dụng 2 Stored Procedures: `GetActiveCustomers` và `UpdateCustomer`.

## 1. Yêu cầu hệ thống
- .NET SDK (phiên bản mới nhất)
- SQL Server (LocalDB)

## 2. Hướng dẫn cài đặt

### Bước 1: Cấu hình Connection String
Mở file `appsettings.json` và kiểm tra chuỗi kết nối:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=Net201Slide7Demo02;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

### Bước 2: Tạo Database và Stored Procedures
Chạy lệnh sau để thực thi Migration. Migration này đã bao gồm lệnh SQL để tạo 2 SPs.
```bash
dotnet ef database update --project Demo02 --startup-project Demo02
```

Sau khi chạy xong, hãy mở **SQL Server Object Explorer** để kiểm tra:
- Database `Net201Slide7Demo02` đã được tạo.
- Bảng `Customers` có dữ liệu mẫu.
- Trong thư mục **Programmability > Stored Procedures** có `dbo.GetActiveCustomers` và `dbo.UpdateCustomer`.

### Bước 3: Chạy ứng dụng
```bash
dotnet run --project Demo02
```

---

## 3. Kiến trúc dự án
Dự án được triển khai theo mô hình N-Tier (3-Layer):
1.  **Repository Layer (`CustomerRepository`)**: Chịu trách nhiệm giao tiếp trực tiếp với DB.
    -   Sử dụng `_context.Customers.FromSqlRaw(...)` để gọi SP `SELECT`.
    -   Sử dụng `_context.Database.ExecuteSqlRaw(...)` với `SqlParameter` để gọi SP `UPDATE`.
2.  **Service Layer (`CustomerService`)**: Chứa logic nghiệp vụ, gọi xuống Repository.
3.  **Presentation Layer (`CustomersController`)**: Gọi Service để lấy dữ liệu và hiển thị lên View.

---

## 4. Kịch bản Demo (Script cho Giảng viên)

### Bước 1: Kiểm tra Database
- Mở SQL Server, query bảng `Customers` để thấy có cả user `Active`, `Inactive`, `New`.
- (Tùy chọn) Chạy thử SP trong SQL: `EXEC GetActiveCustomers`.

### Bước 2: Demo Chức năng `GetActiveCustomers`
- Truy cập `https://localhost:5001/Customers` (hoặc bấm menu **"Demo SP (Customers)"**).
- **Quan sát:** Danh sách chỉ hiển thị những khách hàng có `IsActive = 1`.
- **Giải thích code (`CustomerRepository.cs`):**
  ```csharp
  return _context.Customers
      .FromSqlRaw("EXECUTE dbo.GetActiveCustomers")
      .ToList();
  ```

### Bước 3: Demo Chức năng `UpdateCustomer`
- Tại giao diện danh sách, bấm nút **"Update Status"** của một khách hàng bất kỳ.
- Một Modal hiện ra, nhập Status mới (ví dụ: "VIP Customer").
- Bấm **Save changes**.
- Hệ thống báo thành công "Updated customer X status to 'VIP Customer'".
- **Giải thích code (`CustomerRepository.cs`):**
  - Chú ý việc sử dụng `SqlParameter` để truyền tham số an toàn, tránh SQL Injection.
  ```csharp
  var pId = new SqlParameter("@CustomerId", id);
  var pStatus = new SqlParameter("@NewStatus", newStatus);
  _context.Database.ExecuteSqlRaw("EXECUTE dbo.UpdateCustomer @CustomerId, @NewStatus", pId, pStatus);
  ```

## 5. Testing
Dự án bao gồm Unit Test cho Service Layer (`Demo02.Tests`) sử dụng thư viện **Moq**.
Để chạy test:
```bash
dotnet test Demo02.Tests
```

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
Dự án được triển khai theo mô hình N-Tier (3-Layer) để tách biệt trách nhiệm:

### 3.1. Repository Pattern (`CustomerRepository`)
- **Vai trò:** Lớp này chịu trách nhiệm duy nhất là **truy cập dữ liệu**. Nó giao tiếp trực tiếp với DatabaseContext của EF Core.
- **Nhiệm vụ trong Demo này:**
  - Ẩn giấu sự phức tạp của việc gọi thực thi SQL trần (Raw SQL) hay Stored Procedure.
  - Cung cấp các hàm sạch sẽ (`GetActiveCustomers`, `UpdateCustomerStatus`) cho lớp Service sử dụng mà không cần biết bên dưới là SQL hay EF Core thuần.
  - **Code:**
    - Sử dụng `_context.Customers.FromSqlRaw(...)` để gọi SP `SELECT`.
    - Sử dụng `_context.Database.ExecuteSqlRaw(...)` với `SqlParameter` để gọi SP `UPDATE`.

### 3.2. Service Pattern (`CustomerService`)
- **Vai trò:** Lớp này chứa **logic nghiệp vụ (Business Logic)** của ứng dụng.
- **Tại sao cần lớp này?**
  - Giúp Controller gọn nhẹ (Fat Model, Skinny Controller).
  - Tách biệt logic nghiệp vụ khỏi logic truy cập dữ liệu. Ví dụ: Nếu sau này đổi từ SQL Server sang file JSON, chỉ cần sửa Service/Repository mà không ảnh hưởng Controller.
  - Trong dự án thực tế, Service sẽ thực hiện validate dữ liệu, tính toán, gửi email, ghi log... trước khi gọi Repository. Trong demo này, Service chỉ đơn giản là gọi chuyển tiếp xuống Repository để minh họa cấu trúc.

### 3.3. Presentation Layer (`CustomersController`)
- **Vai trò:** Tiếp nhận request từ người dùng (HTTP GET/POST), gọi Service để xử lý, và trả về View tương ứng.

---

## 4. Tại sao chạy Migration lại có Stored Procedure?
Thông thường, EF Core Migration chỉ tạo bảng (Table) dựa trên các class Model. Tuy nhiên, trong dự án này, khi bạn chạy `dotnet ef database update`, Database lại có sẵn 2 Stored Procedures.

**Lý do:**
- Trong file migration `Migrations/...FirstCreate.cs`, chúng ta có thể chèn các lệnh SQL tùy ý.
- Tác giả đã sử dụng lệnh `migrationBuilder.Sql(@"CREATE PROCEDURE ...")` để tự động tạo SP ngay khi khởi tạo Database.
- **Lợi ích:** Giúp việc triển khai (Deploy) dễ dàng hơn. Bạn không cần chạy script SQL thủ công bên ngoài; chỉ cần lệnh update database của EF Core là đủ tất cả cấu trúc DB.

---

## 5. Kịch bản Demo (Script cho Giảng viên)

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

## 6. Testing
Dự án bao gồm Unit Test cho Service Layer (`Demo02.Tests`) sử dụng thư viện **Moq**.
Để chạy test:
```bash
dotnet test Demo02.Tests
```

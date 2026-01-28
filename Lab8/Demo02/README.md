# NET201 Side 8 Demo 02: Eager Loading (Left Join & Inner Join)

Dự án này minh họa sự khác biệt giữa **Inner Join** và **Left Join** trong Entity Framework Core thông qua Eager Loading (`.Include()`).

## 1. Yêu cầu Hệ thống
- .NET 8 SDK (hoặc mới hơn).
- SQL Server (LocalDB).

## 2. Cài đặt & Chạy
1. Mở thư mục dự án: `Demo02/`.
2. Chạy ứng dụng:
   ```powershell
   dotnet run
   ```
3. Ứng dụng sẽ **tự động** tạo Database và Seed Data khi khởi động.
4. Truy cập: `https://localhost:7044` (hoặc port hiển thị trong console).

## 3. Giải thích Kỹ thuật

### Mô hình (Entities)
Chúng ta có 3 thực thể chính:
1. **Student** (Sinh viên).
2. **Branch** (Chuyên ngành) - Quan hệ **Required** (1-N).
3. **Address** (Địa chỉ) - Quan hệ **Optional/Nullable** (1-1).

### Logic Eager Loading (`StudentController.cs`)
```csharp
var students = _context.Students
                       .Include(s => s.Branch)  // -> Sinh ra INNER JOIN (do BranchId bắt buộc)
                       .Include(s => s.Address) // -> Sinh ra LEFT JOIN (do AddressId có thể null)
                       .ToList();
```

### Kết quả trên View
- **Nguyen Van A**: Có cả Branch và Address -> Hiển thị đầy đủ.
- **Tran Van B**: Có Branch nhưng **KHÔNG** có Address (`AddressId = NULL`).
  - Nhờ **Left Join**, SV này vẫn hiển thị trong danh sách.
  - Cột Address sẽ hiển thị badge vàng: "Chưa có địa chỉ (Left Join)".

## 4. Cấu trúc Project
- `Models/Entities.cs`: Định nghĩa `Student`, `Branch`, `Address`.
- `Data/AppDbContext.cs`: Cấu hình Seed Data (1 Branch, 2 Students...).
- `Controllers/StudentController.cs`: Gọi `.Include()`.
- `Views/Student/Index.cshtml`: Hiển thị bảng dữ liệu.

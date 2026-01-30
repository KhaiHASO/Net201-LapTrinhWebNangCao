# Demo 04: Explicit Loading trong Entity Framework Core

Dự án này minh họa kỹ thuật **Explicit Loading** (Tải rõ ràng/thủ công) dữ liệu quan hệ trong Entity Framework Core.

## Mục tiêu học tập
- Hiểu cách hoạt động của Explicit Loading.
- Phân biệt với Eager Loading (Include) và Lazy Loading (Virtual).
- Sử dụng phương thức `Entry().Collection().Load()` hoặc `Entry().Reference().Load()`.

## Cấu trúc dự án
- **Models/Entities.cs**: Customer và Order (Quan hệ 1-N).
- **Controllers/ExplicitController.cs**:
  - `Index()`: Load Customer nhưng KHÔNG load Orders.
  - `LoadOrders(int id)`: Tìm Customer, sau đó gọi lệnh Explicit Load để lấy Orders.
- **Views**: Minh họa trạng thái trước và sau khi load.

## Hướng dẫn chạy Demo
1. Chạy ứng dụng:
   ```bash
   dotnet run
   ```
2. Database (`Demo04`) sẽ tự động được tạo và seed dữ liệu.
3. Trang chủ sẽ hiển thị danh sách Customer.
   - Để ý cột **Orders Count** là **0** (dù trong database có data).
4. Bấm nút **"Explicit Load Orders"**.
5. Hệ thống sẽ:
   - Truy vấn Customer (nếu chưa có).
   - Chạy lệnh SQL riêng biệt để lấy Orders của Customer đó.
   - Hiển thị trang Details với đầy đủ danh sách Orders.

## Code Snippet quan trọng
```csharp
// 1. Lấy Customer (Chưa có Orders)
var customer = _context.Customers.Find(id);

// 2. Explicit Load Orders
_context.Entry(customer)
        .Collection(c => c.Orders)
        .Load();
```

***Lưu ý:*** Explicit Loading hữu ích khi bạn đã có object cha trong bộ nhớ và muốn tải thêm dữ liệu con tại một thời điểm cụ thể sau đó, hoặc khi muốn filter/sort dữ liệu con trước khi tải (sử dụng `.Query()`).

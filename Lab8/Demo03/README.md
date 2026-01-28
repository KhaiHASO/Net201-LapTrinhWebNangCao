# Demo03: Lazy Loading (NET201)

Dự án Demo cho bài giảng Slide 17-26 môn NET201 (Lập trình Web Nâng cao) tại FPT Polytechnic.

## 1. Giới thiệu
Ứng dụng minh họa kỹ thuật **Lazy Loading** trong Entity Framework Core, sử dụng `Microsoft.EntityFrameworkCore.Proxies`.

- **Khái niệm Lazy Loading**: Dữ liệu liên quan (Related Data) sẽ **KHÔNG** được tải ngay lập tức. Nó chỉ được tải (âm thầm chạy thêm câu SQL) khi property đó lần đầu tiên được truy cập (access).
- **Yêu cầu kỹ thuật**:
    1. Cài gói `Microsoft.EntityFrameworkCore.Proxies`.
    2. Navigation Property phải là `virtual` (Ví dụ: `virtual Customer Customer`).
    3. Cấu hình `UseLazyLoadingProxies()` trong DbContext.

## 2. Chuẩn bị
- Visual Studio 2022+ hoặc VS Code.
- SQL Server (LocalDB mặc định).
- .NET 8 SDK.

## 3. Cách chạy dự án
Ứng dụng đã được cấu hình **Auto-Migration**, bạn không cần chạy lệnh update database thủ công.

1.  Mở terminal tại thư mục `Demo03`.
2.  Chạy lệnh:
    ```bash
    dotnet run
    ```
3.  Truy cập browser tại: `http://localhost:5xxx`.

## 4. Kịch bản Demo (Scenario) - QUAN TRỌNG

### Bước 1: Vào trang danh sách (Index)
- URL: `http://localhost:5xxx/Customer`
- Quan sát Console Log:
    - Chỉ chạy **1 câu lệnh SQL** để lấy danh sách Customer (`SELECT [c].[CustomerId], [c].[CustomerName] FROM [Customers]...`).
    - Dữ liệu Orders chưa hề được tải.

### Bước 2: Bấm nút "View Details"
- Chọn 1 khách hàng (ví dụ: Thay giao Thinh).
- URL: `http://localhost:5xxx/Customer/Details/1`.

### Bước 3: Quan sát Console Log một lần nữa
- Bạn sẽ thấy tiếp tục xuất hiện **1 câu lệnh SQL** query bảng Order (`SELECT [o].[OrderId]... FROM [Orders]...`).
- **Giải thích**: Câu lệnh này được sinh ra **TỰ ĐỘNG** ngay khi file View `.cshtml` chạy vòng lặp `foreach (var order in Model.Orders)`. Đây chính là **Lazy Loading**.

### Lưu ý
Nếu bạn quên từ khóa `virtual` hoặc quên `UseLazyLoadingProxies()`, khi truy cập `Model.Orders`, giá trị sẽ là `null` hoặc rỗng, và không có câu SQL nào chạy thêm.

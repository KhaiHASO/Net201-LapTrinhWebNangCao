# Demo02: Dependency Injection (Teaching Kit)

**Môn học:** NET201 - Lập trình Web Nâng Cao (Slide 6)  
**Mục tiêu:** Bộ công cụ  trực quan về DI và Service Lifetimes.

## 🌟 Cấu trúc Demo

Project được chia làm 2 phần demo riêng biệt, truy cập từ Dashboard chính:

### 1. Demo 1: Loose Coupling (Tính lỏng lẻo)
**Mục đích:** Chứng minh rằng khi sử dụng Interface, ta có thể thay đổi toàn bộ logic nghiệp vụ (Business Logic) mà **KHÔNG CẦN SỬA CONTROLLER**.

- **Kịch bản:** Trang web tính tiền sản phẩm.
- **Class mặc định:** `StandardCalculator` (Giá giữ nguyên).
- **Class thay thế:** `BlackFridayCalculator` (Giảm giá 50%).

### 2. Demo 2: Service Lifetimes (Vòng đời)
**Mục đích:** So sánh trực quan 3 chế độ `Transient`, `Scoped`, `Singleton`.

## 👨‍🏫 Kịch bản  (Dành cho Giảng Viên)

### Màn 1: Demo "Loose Coupling" (Tại sao cần DI?)

1.  Cho sinh viên xem `ProductController.cs`. Nhấn mạnh dòng code:
    ```csharp
    private readonly ICalculatorService _calculatorService; // Chỉ phụ thuộc Interface
    ```
2.  Chạy web, vào **Demo 1**. Chỉ vào tổng tiền (Ví dụ: 22,000,000 đ).
3.  **Đặt vấn đề:** "Sếp yêu cầu hôm nay chạy Black Friday, giảm 50% toàn bộ".
4.  Mở `Program.cs`. Comment dòng `StandardCalculator`, mở comment dòng `BlackFridayCalculator`:
    ```csharp
    // builder.Services.AddTransient<..., StandardCalculator>();
    builder.Services.AddTransient<Demo02.Services.Calculators.ICalculatorService, Demo02.Services.Calculators.BlackFridayCalculator>();
    ```
5.  Lưu file. Hot Relad (hoặc chạy lại).
6.  Refresh trang web. -> Tổng tiền giảm còn 50% (11,000,000 đ) và có huy hiệu "Black Friday".
7.  **Kết luận:** Ta đã thay đổi logic cả dự án mà không hề sửa 1 dòng nào trong Controller hay View. Đó là sức mạnh của DI.

### Màn 2: Demo "Service Lifetimes"

1.  Vào Dashboard, chọn **Demo 2**.
2.  Giải thích bảng so sánh GUID.
3.  **Thao tác 1 (Trong cùng 1 Request):**
    -   Chỉ vào cột **Transient**: 2 GUID khác nhau -> *Sinh ra mới liên tục mỗi lần gọi*.
    -   Chỉ vào cột **Scoped**: 2 GUID giống nhau -> *Trong 1 request thì dùng chung 1 thằng*.
4.  **Thao tác 2 (Refresh trang - Request mới):**
    -   Bấm F5.
    -   **Transient**: Lại ra 2 số mới toanh.
    -   **Scoped**: Ra số mới (nhưng 2 thằng vẫn giống nhau). -> *Request mới thì tạo mới*.
    -   **Singleton**: Vẫn y xì số cũ từ lúc bật server. -> *Bất tử cho đến khi tắt Server*.

## 🛠 Cài đặt & Chạy
1.  Sửa `ConnectionStrings` trong `appsettings.json` nếu cần.
2.  Chạy lệnh cập nhật database (vì đã đổi tên DB):
    ```bash
    dotnet ef database update
    ```
3.  Chạy ứng dụng:
    ```bash
    dotnet run
    ```

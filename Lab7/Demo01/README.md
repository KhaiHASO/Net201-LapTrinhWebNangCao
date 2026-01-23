# Demo01 - Native SQL Queries in Entity Framework Core

Dự án này minh họa cách sử dụng Native SQL (truy vấn SQL thuần) trong Entity Framework Core 10.0 (hoặc mới nhất). Dự án bao gồm các ví dụ về `FromSqlRaw`, `FromSqlInterpolated`, `ExecuteSqlRaw`, `ExecuteSqlInterpolated` và minh họa lỗ hổng SQL Injection.

## 1. Yêu cầu hệ thống
- .NET SDK (phiên bản mới nhất)
- SQL Server (LocalDB)

## 2. Hướng dẫn cài đặt

### Bước 1: Cấu hình Connection String
Mở file `appsettings.json` trong project `Demo01` và kiểm tra chuỗi kết nối (mặc định đã cấu hình cho LocalDB):
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=Net201Slide7Demo01;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

### Bước 2: Chạy Migration và Update Database
Mở Terminal tại thư mục của project `Demo01` (hoặc thư mục gốc Solution) và chạy các lệnh sau:

1. **Cài đặt công cụ EF Core (nếu chưa có):**
   ```bash
   dotnet tool install --global dotnet-ef
   ```

2. **Tạo Migration đầu tiên:**
   ```bash
   dotnet ef migrations add InitialCreate --project Demo01 --startup-project Demo01
   ```

3. **Cập nhật Database:**
   ```bash
   dotnet ef database update --project Demo01 --startup-project Demo01
   ```

Sau khi chạy xong, Database `Net201Slide7Demo01` sẽ được tạo và có sẵn 10 sinh viên mẫu.

### Bước 3: Chạy ứng dụng
```bash
dotnet run --project Demo01
```

---

## 3. Kịch bản Demo (Script cho )

### Truy cập trang chủ
- Truy cập `https://localhost:5001` (hoặc port tương ứng).
- Bấm vào menu **"Native SQL Demo"** trên thanh điều hướng hoặc ở trang chủ.

### Bước 1: Demo `FromSqlRaw` (SELECT)
- **Hành động:** Chọn mục **"1. FromSqlRaw (SELECT)"**.
- **Giải thích:**
  - Trang này hiển thị danh sách sinh viên có tuổi > 18.
  - Xem code `NativeSqlController.cs`, action `RawQueryBasic`.
  - Code sử dụng `_context.Students.FromSqlRaw("SELECT * FROM Students WHERE Age > {0}", 18)`.
  - Placeholder `{0}` giúp tránh SQL Injection cơ bản (parameterization).

### Bước 2: Demo `FromSqlInterpolated` (SELECT)
- **Hành động:** Chọn mục **"2. FromSqlInterpolated (SELECT)"**.
- **Thử nghiệm:**
  - Nhập tên: `Nguyen` -> Bấm Search -> Ra các sinh viên họ Nguyen.
  - Nhập tên: `B` -> Bấm Search -> Ra sinh viên tên B.
- **Giải thích:**
  - Code sử dụng `_context.Students.FromSqlInterpolated($"SELECT * FROM Students WHERE FullName LIKE {term}")`.
  - Cú pháp `$` (String Interpolation) trong EF Core sẽ tự động chuyển các biến thành tham số SQL an toàn.

### Bước 3: Demo `ExecuteSqlRaw` (UPDATE) và `ExecuteSqlInterpolated` (DELETE)
- **Demo Update:**
  - Chọn mục **"3. ExecuteSqlRaw (UPDATE)"**.
  - Bấm nút **"Execute Update"**.
  - Thông báo hiển thị số dòng bị ảnh hưởng (tăng tuổi sinh viên Id=1).
  - Có thể quay lại danh sách (`Index` hoặc `RawQueryBasic`) để kiểm tra tuổi của sinh viên Id=1 đã tăng lên.
  
- **Demo Delete:**
  - Chọn mục **"4. ExecuteSqlInterpolated (DELETE)"**.
  - Nhập Id sinh viên muốn xóa (ví dụ: `2`).
  - Bấm **Delete**.
  - Hệ thống báo thành công và chuyển về trang danh sách.

### Bước 4: SQL Injection (Quan trọng)
- **Hành động:** Chọn mục **"5. SQL Injection Test (Unsafe)"**.
- **Hack Demo:**
  - Nhập tên bình thường: `Nguyen` -> Hoạt động tốt.
  - **Hack:** Nhập chuỗi sau vào ô tìm kiếm: 
    ```sql
    ' OR 1=1 --
    ```
  - **Kết quả:** Nó sẽ liệt kê **TẤT CẢ** bản ghi trong bảng Student thay vì tìm theo tên.
  - **Giải thích:** Do code dùng cộng chuỗi `'... WHERE FullName = '` + `searchName` + `'`, nên câu lệnh trở thành `SELECT ... WHERE FullName = '' OR 1=1 --'`. Điều này luôn đúng (`1=1`).
  
- **Chứng minh an toàn (So sánh):**
  - Quay lại mục **"2. FromSqlInterpolated"**.
  - Nhập cùng chuỗi tấn công: `' OR 1=1 --`.
  - **Kết quả:** Không tìm thấy gì (hoặc chỉ tìm ai đó có tên kỳ quặc như vậy). 
  - **Lý do:** EF Core đã mã hóa chuỗi đó thành tham số, không phải là lệnh SQL thực thi.

## 4. Testing
Để chạy các Unit Test:
```bash
dotnet test Demo01.Tests
```

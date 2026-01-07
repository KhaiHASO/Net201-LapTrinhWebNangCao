# Lab 1 - Dashboard ASP.NET Core MVC (Tiếng Việt)

Dự án này là phiên bản nâng cấp của Lab 1, chuyển đổi thành một **Ứng dụng Dashboard** hiện đại sử dụng **ASP.NET Core MVC** (.NET 10) và **Bootstrap 5**.

Giao diện đã được Việt hóa hoàn toàn và cải tiến đẹp mắt.

## 🚀 Tính Năng

### 1. Giao diện Dashboard Hiện Đại
- **Thanh bên (Sidebar)**: Menu điều hướng thông minh, có hiệu ứng gradient và hover đẹp mắt.
- **Trang chủ Dashboard**: Hiển thị thẻ truy cập nhanh và thống kê cơ bản.
- **Tiếng Việt**: Toàn bộ nhãn, thông báo, nút bấm đều là Tiếng Việt.

### 2. Bài 1: Quản Lý Người Dùng
- **Chức năng**: Xem danh sách, Thêm mới, Chỉnh sửa, Xóa người dùng.
- **Dữ liệu**: Sử dụng danh sách tĩnh (List) trong bộ nhớ để demo ngay lập tức.
- **Route**: `/User`

### 3. Bài 2: Quản Lý Sản Phẩm
- **Chức năng**: Quản lý kho hàng (Tên, Giá, Số lượng).
- **Định tuyến (Attribute Routing)**:
    - `/Product/Details/{id}`: Xem chi tiết sản phẩm.
    - `/Product/Search/{name}`: Tìm kiếm sản phẩm theo tên.
    - `ProductOperation`: Xử lý cả Thêm mới và Cập nhật trong cùng một Action.
- **Route**: `/Product`

### 4. Bài 3: Quản Lý Tệp & Demo Result
- **Quản lý Tệp**: 
    - Upload file lên `wwwroot/uploads`.
    - Download file an toàn.
- **Demo Action Results** (Các loại kết quả trả về):
    - **ContentResult**: Trả về văn bản thô.
    - **JsonResult**: Trả về dữ liệu JSON (Danh sách file).
    - **FileResult**: Tải file từ MemoryStream.
    - **RedirectResult**: Chuyển hướng sang Google.
    - **RedirectToActionResult**: Chuyển hướng nội bộ về trang Index.
- **Route**: `/File`

---

## 🛠️ Cài Đặt & Chạy Demo

1. **Khôi phục thư viện**:
   ```bash
   dotnet restore
   ```

2. **Chạy ứng dụng**:
   ```bash
   dotnet run
   ```

3. **Truy cập Dashboard**:
   Mở trình duyệt và vào: `https://localhost:8001` (hoặc port hiển thị trên terminal).

---

## 🧪 Hướng Dẫn Demo

### Demo Bài 1 (Người Dùng)
1. Chọn **Bài 1: QL Người Dùng** từ menu.
2. Thử **Thêm Người Dùng**, điền form và lưu -> Kiểm tra danh sách đã cập nhật.
3. Thử **Sửa** thông tin và **Xóa** một user.

### Demo Bài 2 (Sản Phẩm)
1. Chọn **Bài 2: QL Sản Phẩm**.
2. **Tìm kiếm**: Nhập "Laptop" hoặc "Phone" vào ô tìm kiếm -> Nhấn nút Tìm Kiếm.
3. **Chi tiết**: Bấm vào biểu tượng con mắt để xem chi tiết với format tiền tệ Việt Nam.

### Demo Bài 3 (Tệp & Result)
1. Chọn **Bài 3: QL Tệp & Demo**.
2. **Upload**: Chọn 1 file ảnh hoặc text bất kỳ -> Bấm Tải Lên -> File sẽ hiện trong danh sách bên dưới.
3. **Demo Results** (Bấm các nút bên phải):
    - **ContentResult**: Tab mới hiện text "Đây là ví dụ về ContentResult..."
    - **JsonResult**: Tab mới hiện dữ liệu JSON.
    - **RedirectResult**: Chuyển hướng sang Google.com.

---

## 📂 Cấu Trúc Dự Án

- `Controllers/`: Chứa `UserController` (Bài 1), `ProductController` (Bài 2), `FileController` (Bài 3).
- `Views/`: Giao diện Razor đã Việt hóa.
- `wwwroot/`: Tài nguyên tĩnh (CSS, JS) và thư mục `uploads/`.

# Demo01: Data Validation & Data Annotations

Dự án này minh họa cách sử dụng **Data Annotations** để kiểm tra tính hợp lệ của dữ liệu (Validation) trong ASP.NET Core MVC.

## 🌟 Tính năng Demo

Dự án tập trung vào Model `Student` và chức năng `Create` để demo các trường hợp validation phổ biến:

1.  **Required**: Bắt buộc nhập (Họ tên, Email).
2.  **StringLength/MinLength**: Độ dài tối thiểu/tối đa (Họ tên).
3.  **EmailAddress**: Kiểm tra định dạng email hợp lệ.
4.  **Range**: Kiểm tra giá trị số nằm trong khoảng (Tuổi: 18-100, GPA: 0-10).

## 🚀 Cách chạy chương trình

1.  Mở terminal tại thư mục `Demo01`:
    ```bash
    cd Demo01
    dotnet run
    ```
2.  Truy cập: `http://localhost:5000` (hoặc port hiển thị trên màn hình).
3.  Chọn menu **"Demo: Validation Form"** hoặc truy cập `/Student/Create`.

## 🧪 Kịch bản Test (Scenario)

### Trường hợp 1: Form rỗng
- **Thao tác**: Không nhập gì cả, bấm nút "Lưu Hồ Sơ".
- **Kết quả mong đợi**:
    - Hệ thống báo lỗi đỏ tại các ô Họ tên, Email, Tuổi, GPA.
    - Thông báo lỗi chi tiết hiển thị (ví dụ: "The FullName field is required.").

### Trường hợp 2: Sai định dạng
- **Thao tác**:
    - Email: nhap linh tinh (không có @).
    - Tuổi: 10 (nhỏ hơn 18) hoặc 150 (lớn hơn 100).
    - GPA: 11 (lớn hơn 10).
- **Kết quả mong đợi**:
    - Báo lỗi định dạng Email không hợp lệ.
    - Báo lỗi Tuổi phải từ 18-100.
    - Báo lỗi GPA phải từ 0-10.

### Trường hợp 3: Hợp lệ
- **Thao tác**: Nhập đúng dữ liệu (Họ tên > 5 ký tự, Email đúng, Tuổi 20, GPA 8.0).
- **Kết quả mong đợi**: Form submit thành công (trong demo này sẽ chuyển hướng về Index hoặc hiển thị thành công).

## 🛠 Công nghệ sử dụng
- **ASP.NET Core MV 8.0**
- **Bootstrap 5** (Premium UI)
- **Data Annotations** (`System.ComponentModel.DataAnnotations`)

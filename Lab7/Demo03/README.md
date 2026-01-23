# Demo03 - LINQ to Entities (Advanced Querying)

Dự án này minh họa các kỹ thuật truy vấn nâng cao trong Entity Framework Core sử dụng LINQ (Language Integrated Query).
Nội dung demo bao gồm: Filtering, Sorting, Projection, Grouping, Pagination, và Relationships.

## 1. Yêu cầu hệ thống
- .NET SDK (phiên bản mới nhất)
- SQL Server (LocalDB)

## 2. Setup Database (Quan trọng)

**Lưu ý:** Dự án này KHÔNG tự động tạo database khi chạy. Bạn cần chạy thủ công các lệnh sau trong Terminal để khởi tạo:

### Bước 1: Tạo Migration ban đầu
Tại thư mục gốc của Solution (`Lab7`), chạy lệnh:
```bash
dotnet ef migrations add InitialCreate --project Demo03 --startup-project Demo03
```

### Bước 2: Cập nhật Database
```bash
dotnet ef database update --project Demo03 --startup-project Demo03
```

*Sau bước này, Database `Net201Slide7Demo03` sẽ được tạo và dữ liệu mẫu (Seeding) sẽ được thêm vào ngay khi bạn chạy ứng dụng lần đầu.*

### Bước 3: Chạy ứng dụng
```bash
dotnet run --project Demo03
```

---

## 3. Kịch bản Demo (Script cho demo)

Truy cập `https://localhost:5001/Linq` (hoặc bấm menu **"Linq Demo"**).

### 1. Basic Query
- **Chức năng:** Lọc các sản phẩm có `Price > 100` và sắp xếp theo tên.
- **Code:** `_context.Products.Where(p => p.Price > 100).OrderBy(p => p.ProductName)`

### 2. Single Result
- **Chức năng:** Tìm sản phẩm có ID = 1.
- **Code:** `_context.Products.FirstOrDefault(...)` kết hợp với toán tử `??` để xử lý null.

### 3. Relationships (Include)
- **Chức năng:** Hiển thị danh sách sản phẩm KÈM THEO tên Category.
- **Code:** `_context.Products.Include(p => p.Category)` (Eager Loading).

### 4. Grouping
- **Chức năng:** Thống kê số lượng sản phẩm và giá trung bình theo từng Category.
- **Code:** `.GroupBy(p => p.Category.CategoryName)` và Projection ra `GroupViewModel`.

### 5. Projection
- **Chức năng:** Chỉ lấy các trường cần thiết và map vào `ProductViewModel` (Ví dụ: Định dạng giá tiền).
- **Code:** `.Select(p => new ProductViewModel { ... })`.

### 6. Pagination (Phân trang)
- **Chức năng:** Hiển thị dữ liệu theo trang (Mỗi trang 3 sản phẩm).
- **Thao tác:** Bấm nút **Next** / **Previous** ở dưới bảng để chuyển trang.
- **Code:** `.Skip((pageIndex - 1) * pageSize).Take(pageSize)`.

### 7. Search
- **Chức năng:** Tìm kiếm sản phẩm theo tên gần đúng.
- **Thao tác:** Nhập "Pro" hoặc "Smart" vào ô tìm kiếm và bấm Go.
- **Code:** `.Where(p => p.ProductName.Contains(keyword))`.

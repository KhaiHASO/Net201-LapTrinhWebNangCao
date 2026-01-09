# Demo02 - [FromQuery] Model Binding trong ASP.NET Core MVC

Bài Demo này minh họa cách sử dụng attribute `[FromQuery]` để binding dữ liệu từ Query String (URL) vào Model, thường dùng cho các chức năng Tìm kiếm (Search), Lọc (Filter) và Phân trang (Paging).

## 1. Cơ Chế Hoạt Động

Khi  truy cập URL:
`/Products?SearchTerm=Laptop&PageNumber=2`

Hệ thống Model Binding sẽ tự động thực hiện:
1.  Nhận diện tham số `SearchTerm` và `PageNumber` từ Query String.
2.  Khởi tạo đối tượng `ProductQueryParameters`.
3.  Gán giá trị `Laptop` vào thuộc tính `SearchTerm`.
4.  Gán giá trị `2` vào thuộc tính `PageNumber`.

Điều này xảy ra nhờ vào khai báo trong Controller:
```csharp
public async Task<IActionResult> Index([FromQuery] ProductQueryParameters queryParams)
```

## 2. Các Thành Phần Chính

### Model: `ProductQueryParameters`
Chứa các tham số cần thiết để query dữ liệu:
```csharp
public class ProductQueryParameters
{
    public string SearchTerm { get; set; }  // Tìm kiếm theo tên
    public string Category { get; set; }    // Lọc theo danh mục
    public string SortBy { get; set; }      // Sắp xếp theo trường nào (Price, Name)
    public bool SortAscending { get; set; } // Thứ tự sắp xếp
    public int PageNumber { get; set; } = 1; // Trang hiện tại (Mặc định 1)
    public int PageSize { get; set; } = 2;   // Số item trên 1 trang (Mặc định 2)
}
```

### Service: `ProductService`
Thực hiện logic lọc dữ liệu dựa trên `ProductQueryParameters`.
- Sử dụng `.Where()` để lọc.
- Sử dụng `.OrderBy()` để sắp xếp.
- Sử dụng `.Skip()` và `.Take()` để phân trang.

### Controller: `ProductsController`
- Nhận `query` từ URL.
- Gọi Service để lấy dữ liệu.
- Truyền kết quả và các thông tin phân trang (ViewBag) sang View.

## 3. Chạy Demo

1.  Mở terminal tại thư mục `Demo02`.
2.  Build và Run:
    ```bash
    dotnet run
    ```
3.  Truy cập: `/Products`
4.  Thử nghiệm các URL sau để thấy kết quả:
    - Tìm kiếm: `/Products?SearchTerm=Laptop`
    - Lọc Category: `/Products?Category=Fashion`
    - Sắp xếp giá giảm dần: `/Products?SortBy=Price&SortAscending=false`
    - Phân trang: `/Products?PageNumber=2`
    - Kết hợp: `/Products?SearchTerm=t&PageNumber=1&SortBy=Price`

## Lưu Ý
- `[FromQuery]` rất hữu ích cho các request GET (lấy dữ liệu).
- Tên tham số trên URL không phân biệt hoa thường (Case-insensitive) nhưng phải đúng chính tả với tên Property trong Model.

# Demo04 - Custom Model Binding (Binding List<int> from Comma Separated String)

Bài Demo này minh họa cách tự tạo một **Custom Model Binder** để xử lý một kiểu dữ liệu mà mặc định ASP.NET Core không hỗ trợ tự động binding theo ý muốn.
Cụ thể: Binding một chuỗi các số ngăn cách bởi dấu phẩy (ví dụ: `1,2,3,4`) vào một `List<int>`.

## Mục Tiêu
Chuyển đổi URL:
`/Home/GetDetails?Ids=1,2,3,4,5`

Thành tham số Action:
```csharp
public IActionResult GetDetails(List<int> Ids) // Ids = [1, 2, 3, 4, 5]
```

## Giải Pháp

### 1. CommaSeparatedModelBinder
Lớp thực thi `IModelBinder`:
1.  Lấy giá trị từ `ValueProvider`.
2.  Kiểm tra nếu có giá trị, thực hiện `Split(',')`.
3.  Parse từng phần tử sang `int`.
4.  Gán kết quả `List<int>` vào `bindingContext.Result`.

**Lưu ý về Task.CompletedTask**:
Phương thức `BindModelAsync` trả về `Task`. Vì logic xử lý chuỗi của chúng ta là đồng bộ (synchronous) và rất nhanh, không cần `await` bất kỳ I/O nào, nên ta trả về `Task.CompletedTask` để báo hiệu Task đã hoàn thành ngay lập tức mà không cần tạo thêm overhead của state machine async/await.

### 2. CommaSeparatedModelBinderProvider
Lớp thực thi `IModelBinderProvider`:
- Kiểm tra nếu kiểu dữ liệu cần bind là `List<int>`.
- Nếu đúng, trả về instance của `CommaSeparatedModelBinder`.

### 3. Đăng Ký (Registration)
Trong `Program.cs`:
```csharp
builder.Services.AddControllersWithViews(options =>
{
    options.ModelBinderProviders.Insert(0, new CommaSeparatedModelBinderProvider());
});
```
Việc `Insert(0, ...)` đảm bảo Provider của chúng ta được kiểm tra đầu tiên, trước các provider mặc định.

## Hướng Dẫn Chạy

1.  Mở terminal tại thư mục `Demo04`.
2.  Chạy lệnh:
    ```bash
    dotnet run
    ```
3.  Truy cập URL test (hoặc click link trên Header):
    `http://localhost:<port>/Home/GetDetails?Ids=10,20,30`
4.  Kết quả trả về JSON: `[10, 20, 30]`.

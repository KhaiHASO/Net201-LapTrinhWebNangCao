# Demo03 - Complex Type Model Binding trong ASP.NET Core MVC

Bài Demo này minh họa khả năng **Complex Type Model Binding** của ASP.NET Core, cho phép binding dữ liệu từ form vào các cấu trúc phức tạp như: Object lồng nhau, Enum và Collections.

## Các Tính Năng Demo

### 1. Binding Đối Tượng Lồng Nhau (Nested Objects)
Model `Student` chứa một thuộc tính là object `Address`:
```csharp
public class Student {
    // ...
    public Address Address { get; set; }
}

public class Address {
    public string Street { get; set; }
    public string City { get; set; }
    // ...
}
```
**Cách hoạt động**:
Trong View, sử dụng Tag Helper `asp-for="Address.Street"` sẽ sinh ra HTML `name="Address.Street"`. Model Binder sẽ tự động khởi tạo đối tượng `Address` và gán giá trị `Street` vào nó.

### 2. Binding Enum
Model sử dụng Enum cho `Gender` và `Branch`.
- **Radio Buttons**: Duyệt qua các giá trị Enum và tạo Radio input.
- **Select List**: Tạo Dropdown list từ các giá trị Enum.

### 3. Binding Collections (List)
Sở thích (`Hobbies`) là một `List<string>`.
```html
<input type="checkbox" name="Hobbies" value="Music" />
<input type="checkbox" name="Hobbies" value="Sports" />
```
Khi người dùng chọn nhiều checkbox, Model Binder sẽ gom tất cả các giá trị `value` được chọn vào List `Hobbies`.

## Cấu Trúc Dự Án

- **Models**:
    - `Enums.cs`: Định nghĩa `Branch`, `Gender`.
    - `Address.cs`: Lớp địa chỉ.
    - `Student.cs`: Lớp chính chứa tất cả các thành phần trên.
- **Controllers/StudentsController.cs**:
    - Chuẩn bị dữ liệu Enum/List để hiển thị trên form.
    - Xử lý POST request và hiển thị kết quả.
- **Views/Students/Create.cshtml**: Form đăng ký với đầy đủ các loại input control.

## Hướng Dẫn Chạy

1.  Mở terminal tại thư mục `Demo03`.
2.  Chạy lệnh:
    ```bash
    dotnet run
    ```
3.  Truy cập: `/Students/Create` (hoặc dùng link trên Header "Demo 3").
4.  Điền form:
    - Nhập thông tin cơ bản.
    - Chọn Gender (Radio), Branch (Select).
    - Nhập địa chỉ (Street, City).
    - Chọn nhiều sở thích.
5.  Nhấn Register và xem kết quả binding thành công.

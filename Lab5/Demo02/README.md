# Demo02 - Validation & Cascade Delete

## 📚 Giới thiệu

Dự án demo minh họa **Data Annotations Validation** và **Fluent API Cascade Delete** trong Entity Framework Core cho môn C#4 tại FPT Polytechnic.

### Mục tiêu học tập

- ✅ Sử dụng **Data Annotations** để validation dữ liệu đầu vào
- ✅ Hiển thị thông báo lỗi validation trên giao diện
- ✅ Cấu hình **Cascade Delete** bằng Fluent API
- ✅ Demo hành vi Cascade Delete khi xóa dữ liệu

## 🛠️ Yêu cầu hệ thống

- **.NET 10 SDK** hoặc cao hơn
- **SQL Server LocalDB** hoặc **SQL Server Express**
- **Visual Studio 2022** hoặc **Visual Studio Code**

## 📂 Cấu trúc dự án

```
Demo02/
├── Data/
│   └── DemoContext.cs           # DbContext với Cascade Delete
├── Models/
│   ├── ClassRoom.cs             # Entity Lớp học
│   └── Student.cs               # Entity Sinh viên (với validation)
├── Controllers/
│   ├── StudentsController.cs    # CRUD Sinh viên
│   └── ClassRoomsController.cs  # CRUD Lớp học
├── Views/
│   ├── Students/                # Views cho Student
│   └── ClassRooms/              # Views cho ClassRoom
└── appsettings.json             # Connection String
```

## 🗄️ Sơ đồ Database

```
┌─────────────────┐
│   ClassRooms    │
│   (Lớp học)     │
└────────┬────────┘
         │ 1
         │
         │ N (Cascade Delete)
         │
┌────────▼────────┐
│    Students     │
│   (Sinh viên)   │
└─────────────────┘
```

**Cascade Delete:** Khi xóa ClassRoom → Tự động xóa tất cả Students thuộc lớp đó

## 🚀 Hướng dẫn chạy dự án

### Bước 1: Mở dự án

```bash
cd c:\Users\Admin\Desktop\github\Net201-LapTrinhWebNangCao\Lab5\Demo02
```

### Bước 2: Restore packages

```bash
dotnet restore
```

### Bước 3: Cấu hình Connection String

File `appsettings.json` đã được cấu hình sẵn:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=demo02;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=true"
  }
}
```

**Database:** `demo02`

### Bước 4: Chạy Migration

```bash
# Tạo migration
dotnet ef migrations add InitialCreate

# Cập nhật database
dotnet ef database update
```

### Bước 5: Chạy ứng dụng

```bash
dotnet run
```

Mở trình duyệt: `https://localhost:5xxx`

## 📖 Kiến thức chính

### 1. Data Annotations - Validation

#### Các Attribute Validation đã sử dụng

| Attribute | Mục đích | Ví dụ trong Student.cs |
|-----------|----------|------------------------|
| `[Required]` | Bắt buộc nhập | `FullName`, `Email`, `GPA` |
| `[StringLength]` | Giới hạn độ dài | `FullName` (3-50 ký tự) |
| `[Range]` | Giới hạn giá trị số | `GPA` (0-10) |
| `[EmailAddress]` | Kiểm tra định dạng email | `Email` |
| `[Phone]` | Kiểm tra số điện thoại | `PhoneNumber` |

#### Ví dụ trong code:

```csharp
public class Student
{
    [Required(ErrorMessage = "Họ tên là bắt buộc")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Họ tên phải từ 3 đến 50 ký tự")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Điểm trung bình là bắt buộc")]
    [Range(0, 10, ErrorMessage = "Điểm trung bình phải nằm trong khoảng từ 0 đến 10")]
    public decimal GPA { get; set; }

    [Required(ErrorMessage = "Email là bắt buộc")]
    [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
    public string Email { get; set; }
}
```

### 2. Hiển thị lỗi Validation trên View

Trong các View `Create.cshtml` và `Edit.cshtml`, validation errors được hiển thị bằng:

```html
<div class="form-group">
    <label asp-for="FullName" class="control-label"></label>
    <input asp-for="FullName" class="form-control" />
    <span asp-validation-for="FullName" class="text-danger"></span>
</div>
```

**Khi nhập sai:**
- Nhập họ tên < 3 ký tự → Hiện lỗi "Họ tên phải từ 3 đến 50 ký tự"
- Nhập điểm 15 → Hiện lỗi "Điểm trung bình phải nằm trong khoảng từ 0 đến 10"
- Nhập email sai định dạng → Hiện lỗi "Email không đúng định dạng"

### 3. Fluent API - Cascade Delete

#### Cấu hình trong DemoContext.cs

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Quan hệ 1-N với Cascade Delete
    modelBuilder.Entity<ClassRoom>()
        .HasMany(c => c.Students)
        .WithOne(s => s.ClassRoom)
        .HasForeignKey(s => s.ClassRoomId)
        .OnDelete(DeleteBehavior.Cascade);  // ⭐ Cascade Delete
}
```

#### Giải thích Cascade Delete

**DeleteBehavior.Cascade** nghĩa là:
- Khi xóa một **ClassRoom**, EF Core sẽ **TỰ ĐỘNG xóa** tất cả **Students** thuộc lớp đó
- Không cần phải xóa Students thủ công
- Database sẽ tự động xử lý thông qua Foreign Key Constraint

#### Các loại DeleteBehavior khác

| DeleteBehavior | Hành vi |
|----------------|---------|
| **Cascade** | Xóa parent → Xóa tất cả children |
| **SetNull** | Xóa parent → Set Foreign Key = NULL |
| **Restrict** | Không cho xóa parent nếu còn children |
| **NoAction** | Không làm gì (database tự xử lý) |

## 🧪 Test Validation

### Scenario 1: Nhập dữ liệu hợp lệ

1. Truy cập `/Students/Create`
2. Nhập:
   - Mã SV: `PH12349`
   - Họ tên: `Nguyễn Văn Test`
   - Điểm TB: `8.5`
   - Email: `test@fpt.edu.vn`
3. Kết quả: ✅ Lưu thành công

### Scenario 2: Test validation lỗi

#### Test 1: Họ tên quá ngắn
- Nhập họ tên: `AB` (< 3 ký tự)
- Kết quả: ❌ Hiện lỗi màu đỏ "Họ tên phải từ 3 đến 50 ký tự"

#### Test 2: Điểm ngoài khoảng
- Nhập điểm: `15` (> 10)
- Kết quả: ❌ Hiện lỗi "Điểm trung bình phải nằm trong khoảng từ 0 đến 10"

#### Test 3: Email sai định dạng
- Nhập email: `abc123` (không có @)
- Kết quả: ❌ Hiện lỗi "Email không đúng định dạng"

#### Test 4: Bỏ trống trường bắt buộc
- Không nhập họ tên
- Kết quả: ❌ Hiện lỗi "Họ tên là bắt buộc"

## 🧪 Test Cascade Delete

### Chuẩn bị

1. Xem danh sách lớp học: `/ClassRooms`
2. Xem danh sách sinh viên: `/Students`
3. Chú ý lớp "NET201" có 2 sinh viên

### Thực hiện test

1. Vào `/ClassRooms`
2. Click **Delete** ở lớp "NET201"
3. Confirm xóa
4. Vào `/Students` kiểm tra

**Kết quả mong đợi:**
- ✅ Lớp "NET201" đã bị xóa
- ✅ 2 sinh viên thuộc lớp "NET201" cũng bị xóa tự động
- ✅ Chỉ còn sinh viên của lớp "NET202"

**Giải thích:**
- Đây là hành vi của **Cascade Delete**
- EF Core tự động xóa các bản ghi liên quan
- Không cần code xóa thủ công

## 📝 Các lệnh Migration quan trọng

```bash
# Tạo migration mới
dotnet ef migrations add <TenMigration>

# Cập nhật database
dotnet ef database update

# Xóa database (để test lại từ đầu)
dotnet ef database drop

# Xem danh sách migrations
dotnet ef migrations list

# Xóa migration cuối cùng (chưa apply)
dotnet ef migrations remove
```

## 🎯 Điểm nhấn kỹ thuật

### Data Annotations vs Fluent API

| Tính năng | Data Annotations | Fluent API |
|-----------|------------------|------------|
| **Validation** | ✅ Tốt nhất | ❌ Không hỗ trợ |
| **Cascade Delete** | ❌ Không cấu hình được | ✅ Cấu hình đầy đủ |
| **Vị trí** | Trên Model | Trong DbContext |

### Khi nào dùng cái gì?

- **Data Annotations**: Dùng cho **validation** (Required, Range, EmailAddress...)
- **Fluent API**: Dùng cho **database configuration** (Cascade Delete, Relationships...)

## 🐛 Troubleshooting

### Lỗi: Validation không hoạt động

**Nguyên nhân:** Thiếu `asp-validation-for` trong View

**Giải pháp:**
```html
<span asp-validation-for="FullName" class="text-danger"></span>
```

### Lỗi: Cascade Delete không hoạt động

**Nguyên nhân:** Chưa cấu hình `OnDelete(DeleteBehavior.Cascade)`

**Giải pháp:** Kiểm tra DemoContext.cs, đảm bảo có:
```csharp
.OnDelete(DeleteBehavior.Cascade);
```

### Lỗi: Không xóa được ClassRoom

**Nguyên nhân:** Có thể đang dùng `DeleteBehavior.Restrict`

**Giải pháp:** Đổi thành `DeleteBehavior.Cascade` hoặc xóa Students trước

## 📚 Tài liệu tham khảo

- [Data Annotations](https://docs.microsoft.com/en-us/ef/core/modeling/entity-properties)
- [Fluent API](https://docs.microsoft.com/en-us/ef/core/modeling/)
- [Cascade Delete](https://docs.microsoft.com/en-us/ef/core/saving/cascade-delete)
- [Validation](https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation)

## 👨‍🏫 Tác giả

**Trợ giảng:** FPT Polytechnic  
**Môn học:** C#4 - Lập trình Web Nâng cao  
**Bài học:** Slide 5 - Data Annotations & Fluent API  

---

**Chúc các bạn học tập tốt! 🎓**

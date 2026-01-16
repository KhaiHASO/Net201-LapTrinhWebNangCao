# Kịch Bản Demo - Validation & Cascade Delete
## Thời lượng: 15 phút

---

## 📋 Chuẩn bị trước khi demo

### Công cụ cần mở:
- ✅ Visual Studio Code / Visual Studio 2022
- ✅ Terminal / PowerShell
- ✅ Trình duyệt (Chrome/Edge)
- ✅ SQL Server Management Studio (SSMS) - Tùy chọn

### Checklist:
- [ ] Database `demo02` đã được tạo và có dữ liệu mẫu
- [ ] Đã test chạy `dotnet run` thành công
- [ ] Đã mở sẵn các file: `Student.cs`, `DemoContext.cs`
- [ ] Đã chuẩn bị dữ liệu test sai (điểm 15, email sai...)

---

## ⏱️ Timeline Demo (15 phút)

| Thời gian | Nội dung | Hành động |
|-----------|----------|-----------|
| 0:00 - 2:00 | Giới thiệu & Mục tiêu | Slide + Giải thích |
| 2:00 - 8:00 | **PHẦN 1: Demo Validation** | Live demo trên Web + Code |
| 8:00 - 13:00 | **PHẦN 2: Demo Cascade Delete** | Live demo + SSMS |
| 13:00 - 15:00 | Q&A & Tổng kết | Tương tác |

---

## 🎬 PHẦN 0: GIỚI THIỆU (0:00 - 2:00)

### Script:

> "Chào các bạn! Hôm nay chúng ta sẽ tập trung vào hai kỹ thuật quan trọng:
> 
> 1. **Data Annotations Validation** - Kiểm tra dữ liệu đầu vào
> 2. **Fluent API Cascade Delete** - Xóa dữ liệu tự động
> 
> Đây là những kỹ thuật các bạn sẽ dùng RẤT NHIỀU trong dự án thực tế!"

### Giải thích nhanh:

**Validation là gì?**
- Kiểm tra dữ liệu người dùng nhập có hợp lệ không
- Ví dụ: Điểm phải từ 0-10, email phải đúng định dạng
- Hiển thị lỗi màu đỏ khi nhập sai

**Cascade Delete là gì?**
- Khi xóa bản ghi cha → Tự động xóa các bản ghi con
- Ví dụ: Xóa lớp học → Tự động xóa tất cả sinh viên trong lớp
- Không cần code xóa thủ công

---

## 🎬 PHẦN 1: DEMO VALIDATION (2:00 - 8:00)

### Bước 1: Chạy ứng dụng (30 giây)

**Hành động:**
```bash
cd Lab5\Demo02
dotnet run
```

**Script:**
> "Tôi sẽ chạy ứng dụng lên. Chú ý URL: https://localhost:xxxx"

### Bước 2: Truy cập trang Create Student (30 giây)

**Hành động:**
```
Mở trình duyệt: https://localhost:xxxx/Students/Create
```

**Script:**
> "Đây là form tạo sinh viên mới. Các bạn thấy có các trường: Mã SV, Họ tên, Điểm TB, Email..."

### Bước 3: Test Validation - Nhập dữ liệu SAI (3 phút)

#### Test 1: Bỏ trống trường bắt buộc

**Hành động:**
```
1. Để trống trường "Họ và tên"
2. Click nút "Create"
```

**Kết quả:**
- ❌ Hiện lỗi màu đỏ: "Họ tên là bắt buộc"

**Script:**
> "Các bạn thấy không? Khi tôi bỏ trống họ tên, hệ thống hiện lỗi màu đỏ ngay. Đây là validation từ `[Required]`."

#### Test 2: Nhập họ tên quá ngắn

**Hành động:**
```
1. Nhập họ tên: "AB" (chỉ 2 ký tự)
2. Click nút "Create"
```

**Kết quả:**
- ❌ Hiện lỗi: "Họ tên phải từ 3 đến 50 ký tự"

**Script:**
> "Tôi nhập 'AB' - chỉ 2 ký tự. Hệ thống báo lỗi vì tôi đã cấu hình `[StringLength(50, MinimumLength = 3)]`."

#### Test 3: Nhập điểm ngoài khoảng (QUAN TRỌNG!)

**Hành động:**
```
1. Nhập điểm: "15" (vượt quá 10)
2. Click nút "Create"
```

**Kết quả:**
- ❌ Hiện lỗi: "Điểm trung bình phải nằm trong khoảng từ 0 đến 10"

**Script:**
> "⭐ ĐÂY LÀ ĐIỂM QUAN TRỌNG! Tôi nhập điểm 15 - vượt quá giới hạn. Hệ thống từ chối vì có `[Range(0, 10)]`."

#### Test 4: Nhập email sai định dạng

**Hành động:**
```
1. Nhập email: "abc123" (không có @)
2. Click nút "Create"
```

**Kết quả:**
- ❌ Hiện lỗi: "Email không đúng định dạng"

**Script:**
> "Email 'abc123' không có dấu @, nên validation `[EmailAddress]` báo lỗi."

### Bước 4: Mở code Student.cs để giải thích (2 phút)

**Hành động:**
```
Mở file: Models/Student.cs
```

**Script:**
> "Bây giờ chúng ta xem CODE để hiểu tại sao có những lỗi đó."

**Chỉ vào từng Attribute:**

```csharp
[Required(ErrorMessage = "Họ tên là bắt buộc")]
```
> "**[Required]** = Bắt buộc nhập. `ErrorMessage` là thông báo lỗi hiển thị."

```csharp
[StringLength(50, MinimumLength = 3, ErrorMessage = "Họ tên phải từ 3 đến 50 ký tự")]
```
> "**[StringLength]** giới hạn độ dài. Tối thiểu 3, tối đa 50 ký tự."

```csharp
[Range(0, 10, ErrorMessage = "Điểm trung bình phải nằm trong khoảng từ 0 đến 10")]
```
> "⭐ **[Range]** - Đây là attribute QUAN TRỌNG! Giới hạn giá trị số từ 0 đến 10."

```csharp
[EmailAddress(ErrorMessage = "Email không đúng định dạng")]
```
> "**[EmailAddress]** kiểm tra định dạng email tự động."

### Bước 5: Nhập dữ liệu ĐÚNG để hoàn thành (1 phút)

**Hành động:**
```
1. Nhập:
   - Mã SV: PH99999
   - Họ tên: Nguyễn Văn Demo
   - Điểm TB: 8.5
   - Email: demo@fpt.edu.vn
   - Chọn lớp: NET201
2. Click "Create"
```

**Kết quả:**
- ✅ Lưu thành công, chuyển về trang Index

**Script:**
> "Khi nhập đúng tất cả, dữ liệu được lưu thành công. Đây là cách validation bảo vệ database khỏi dữ liệu sai!"

### Câu hỏi tương tác:
> "Các bạn thấy validation có quan trọng không? Nếu không có validation, người dùng có thể nhập điểm 999, email sai... rất nguy hiểm!"

---

## 🎬 PHẦN 2: DEMO CASCADE DELETE (8:00 - 13:00)

### Bước 1: Xem dữ liệu hiện tại (1 phút)

**Hành động:**
```
1. Truy cập: /ClassRooms
2. Truy cập: /Students
```

**Script:**
> "Trước khi demo Cascade Delete, chúng ta xem dữ liệu hiện tại:
> 
> - Lớp **NET201** có 2 sinh viên
> - Lớp **NET202** có 2 sinh viên
> 
> Bây giờ tôi sẽ XÓA lớp NET201 và xem điều gì xảy ra!"

### Bước 2: Xóa ClassRoom (1 phút)

**Hành động:**
```
1. Vào /ClassRooms
2. Click nút "Delete" ở lớp NET201
3. Confirm xóa
```

**Kết quả:**
- ✅ Lớp NET201 bị xóa
- Chuyển về trang Index, chỉ còn lớp NET202

**Script:**
> "Tôi vừa xóa lớp NET201. Bây giờ câu hỏi là: Các sinh viên thuộc lớp NET201 có bị xóa theo không?"

### Bước 3: Kiểm tra Students (1 phút)

**Hành động:**
```
Truy cập: /Students
```

**Kết quả:**
- ✅ Chỉ còn 2 sinh viên của lớp NET202
- ✅ 2 sinh viên của lớp NET201 đã BỊ XÓA TỰ ĐỘNG

**Script:**
> "⭐ CÁC BẠN THẤY KHÔNG? Khi tôi xóa lớp NET201, 2 sinh viên thuộc lớp đó cũng BỊ XÓA TỰ ĐỘNG!
> 
> Đây chính là **CASCADE DELETE**!"

### Bước 4: Mở code DemoContext.cs để giải thích (2 phút)

**Hành động:**
```
Mở file: Data/DemoContext.cs
Scroll xuống OnModelCreating
```

**Script:**
> "Bây giờ chúng ta xem CODE để hiểu tại sao có hành vi này."

**Chỉ vào:**
```csharp
modelBuilder.Entity<ClassRoom>()
    .HasMany(c => c.Students)
    .WithOne(s => s.ClassRoom)
    .HasForeignKey(s => s.ClassRoomId)
    .OnDelete(DeleteBehavior.Cascade);  // ⭐ ĐÂY!
```

**Script:**
> "⭐ Chú ý dòng này: `.OnDelete(DeleteBehavior.Cascade)`
> 
> Đây là cấu hình Fluent API. Nó nói với EF Core:
> 
> - Khi xóa **ClassRoom** (parent)
> - Thì tự động xóa tất cả **Students** (children) liên quan
> - Không cần code xóa thủ công!"

### Bước 5: Giải thích các DeleteBehavior khác (1 phút)

**Viết lên bảng hoặc slide:**

| DeleteBehavior | Hành vi |
|----------------|---------|
| **Cascade** | Xóa parent → Xóa children |
| **SetNull** | Xóa parent → Set FK = NULL |
| **Restrict** | Không cho xóa parent nếu còn children |

**Script:**
> "Ngoài Cascade, còn có:
> 
> - **SetNull**: Xóa lớp → Sinh viên vẫn còn nhưng ClassRoomId = NULL
> - **Restrict**: KHÔNG CHO xóa lớp nếu còn sinh viên
> 
> Tùy yêu cầu nghiệp vụ mà chọn DeleteBehavior phù hợp!"

### Bước 6: (Tùy chọn) Xem trong SSMS (1 phút)

**Nếu có thời gian:**

**Hành động:**
```
1. Mở SSMS
2. Connect: (localdb)\mssqllocaldb
3. Database: demo02
4. Chạy query:
   SELECT * FROM Students;
   SELECT * FROM ClassRooms;
```

**Script:**
> "Trong database, các bạn thấy Foreign Key có cấu hình ON DELETE CASCADE. Đây là cơ chế database tự động xóa."

### Câu hỏi tương tác:
> "Các bạn nghĩ trong trường hợp nào nên dùng Cascade Delete? Khi nào KHÔNG nên dùng?"
> 
> *(Gợi ý: Dùng khi dữ liệu con phụ thuộc hoàn toàn vào cha. Không dùng khi cần giữ lại lịch sử)*

---

## 🎬 PHẦN 3: Q&A & TỔNG KẾT (13:00 - 15:00)

### Tổng kết kiến thức (1 phút)

**Script:**
> "Chúng ta đã học:"
> 
> ✅ **Data Annotations Validation:**
> - `[Required]`, `[StringLength]`, `[Range]`, `[EmailAddress]`
> - Hiển thị lỗi trên View bằng `asp-validation-for`
> 
> ✅ **Fluent API Cascade Delete:**
> - Cấu hình `.OnDelete(DeleteBehavior.Cascade)`
> - Xóa parent → Tự động xóa children
> - Tiết kiệm code, an toàn hơn

### So sánh nhanh (30 giây)

**Viết lên bảng:**

| Kỹ thuật | Dùng cho | Vị trí |
|----------|----------|--------|
| Data Annotations | Validation | Trên Model |
| Fluent API | Database Config | Trong DbContext |

### Câu hỏi thường gặp (30 giây)

**Q1: "Validation có chạy ở server không?"**

**A:**
> "Có! ASP.NET Core validation chạy cả client-side (JavaScript) và server-side (C#). Nếu user tắt JavaScript, server vẫn kiểm tra."

**Q2: "Cascade Delete có nguy hiểm không?"**

**A:**
> "Có thể nguy hiểm nếu dùng sai! Ví dụ: Xóa nhầm một lớp → Mất hết sinh viên. Nên:
> - Có confirm trước khi xóa
> - Backup database thường xuyên
> - Cân nhắc dùng Soft Delete (đánh dấu xóa thay vì xóa thật)"

---

## 📌 Ghi chú quan trọng

### Các điểm cần nhấn mạnh:

1. **Validation bảo vệ database** ⭐
2. **Range validation rất quan trọng cho số** ⭐
3. **Cascade Delete tiết kiệm code nhưng cần cẩn thận** ⭐
4. **Luôn test validation trước khi deploy** ⭐

### Các lỗi thường gặp cần đề cập:

❌ Quên thêm `asp-validation-for` trong View  
❌ Quên cấu hình `OnDelete` trong Fluent API  
❌ Dùng Cascade Delete cho dữ liệu quan trọng  

### Tips khi demo:

✅ Nhập dữ liệu sai THẬT SỰ để sinh viên thấy lỗi  
✅ Giải thích TẠI SAO cần validation  
✅ Demo Cascade Delete với dữ liệu test, không dùng dữ liệu thật  
✅ Khuyến khích sinh viên hỏi  

---

## 🎯 Bài tập về nhà

**Script:**
> "Bài tập về nhà:"
> 
> 1. Thêm validation cho `PhoneNumber` (phải đúng 10 số)
> 2. Thêm validation cho `DateOfBirth` (phải >= 16 tuổi)
> 3. Thử đổi `DeleteBehavior.Cascade` thành `SetNull` và xem kết quả
> 4. Tạo thêm entity `Course` và cấu hình Cascade Delete

---

## 📋 Checklist sau khi demo

- [ ] Sinh viên hiểu được cách dùng Data Annotations validation
- [ ] Sinh viên biết cách hiển thị lỗi validation trên View
- [ ] Sinh viên hiểu được Cascade Delete hoạt động như thế nào
- [ ] Sinh viên biết khi nào nên/không nên dùng Cascade Delete

---

## 🚨 Lưu ý quan trọng

### Trước khi demo:
1. ✅ Chạy `dotnet ef database update` để đảm bảo có dữ liệu mẫu
2. ✅ Test tất cả các scenario validation
3. ✅ Chuẩn bị dữ liệu test để xóa (không dùng dữ liệu quan trọng)

### Trong khi demo:
1. ✅ Nói CHẬM, RÕ RÀNG
2. ✅ Chỉ vào từng dòng code khi giải thích
3. ✅ Để sinh viên thấy rõ thông báo lỗi màu đỏ
4. ✅ Nhấn mạnh sự khác biệt giữa Data Annotations và Fluent API

### Sau khi demo:
1. ✅ Chia sẻ source code cho sinh viên
2. ✅ Gửi bài tập về nhà
3. ✅ Trả lời câu hỏi qua email/chat

---

**Chúc bạn demo thành công! 🎓**

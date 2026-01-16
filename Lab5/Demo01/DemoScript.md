# Kịch Bản Demo - Data Annotations & Fluent API
## Thời lượng: 15 phút

---

## 📋 Chuẩn bị trước khi demo

### Công cụ cần mở:
- ✅ Visual Studio Code / Visual Studio 2022
- ✅ Terminal / PowerShell
- ✅ SQL Server Management Studio (SSMS) hoặc Azure Data Studio
- ✅ Trình chiếu slide (Slide 5.pdf)

### Checklist:
- [ ] Đã build project thành công
- [ ] Database đã được tạo và có dữ liệu mẫu
- [ ] Đã test chạy `dotnet run` thành công
- [ ] Đã mở sẵn các file quan trọng trong editor

---

## ⏱️ Timeline Demo (15 phút)

| Thời gian | Nội dung | Hành động |
|-----------|----------|-----------|
| 0:00 - 2:00 | Giới thiệu & Mục tiêu | Slide + Giải thích |
| 2:00 - 5:00 | Data Annotations | Live coding + Giải thích |
| 5:00 - 9:00 | Fluent API | Live coding + Giải thích |
| 9:00 - 12:00 | Migration & Database | Terminal + SSMS |
| 12:00 - 15:00 | Q&A & Tổng kết | Tương tác |

---

## 🎬 PHẦN 1: GIỚI THIỆU (0:00 - 2:00)

### Script:

> "Chào các bạn! Hôm nay chúng ta sẽ học về **Data Annotations** và **Fluent API** trong Entity Framework Core. Đây là hai kỹ thuật quan trọng để cấu hình database trong Code-First approach."

### Slide cần chiếu:
- Slide 1: Giới thiệu Data Annotations
- Slide 2: Giới thiệu Fluent API

### Giải thích:

**Data Annotations** là gì?
- Là các **attribute** (đặt trên class, property)
- Dùng để cấu hình database và validation
- Ví dụ: `[Required]`, `[StringLength]`, `[Key]`

**Fluent API** là gì?
- Là cách cấu hình bằng **code** trong DbContext
- Mạnh mẽ hơn Data Annotations
- Dùng cho các cấu hình phức tạp (composite key, relationships...)

### Câu hỏi tương tác:
> "Các bạn đã từng dùng `[Required]` hay `[StringLength]` chưa? Đó chính là Data Annotations đấy!"

---

## 🎬 PHẦN 2: DATA ANNOTATIONS (2:00 - 5:00)

### Bước 1: Mở file `Department.cs` (30 giây)

**Hành động:**
```
Mở: Models/Department.cs
```

**Script:**
> "Chúng ta sẽ xem entity Department. Đây là một entity đơn giản với các Data Annotations cơ bản."

### Bước 2: Giải thích từng Annotation (2 phút)

**Chỉ vào từng dòng code và giải thích:**

```csharp
[Table("Departments")]  // ← CHỈ VÀO ĐÂY
```
> "**[Table]** chỉ định tên bảng trong database. Nếu không có, EF sẽ dùng tên class."

```csharp
[Key]  // ← CHỈ VÀO ĐÂY
```
> "**[Key]** đánh dấu đây là Primary Key. Nếu property tên là Id hoặc DepartmentId thì không cần."

```csharp
[Required]
[StringLength(100)]
[Column("DepartmentName", TypeName = "nvarchar(100)")]
```
> "**[Required]** = NOT NULL trong SQL. **[StringLength]** giới hạn độ dài. **[Column]** chỉ định tên cột và kiểu dữ liệu."

### Bước 3: Mở file `Employee.cs` (1 phút)

**Hành động:**
```
Mở: Models/Employee.cs
```

**Script:**
> "Bây giờ xem entity Employee - phức tạp hơn một chút với Foreign Key."

**Chỉ vào:**
```csharp
[ForeignKey("Department")]
public int? DepartmentId { get; set; }
```
> "**[ForeignKey]** chỉ định Navigation Property tương ứng. Đây là cách tạo quan hệ 1-N."

### Bước 4: Mở file `EmployeeAddress.cs` (30 giây)

**Script:**
> "Đặc biệt, với quan hệ 1-1, chúng ta dùng **Shared Primary Key pattern**:"

```csharp
[Key]
[ForeignKey("Employee")]
public int EmployeeId { get; set; }
```
> "EmployeeId vừa là Primary Key, vừa là Foreign Key. Đây là cách tạo quan hệ 1-1."

### Câu hỏi tương tác:
> "Các bạn thấy Data Annotations có dễ đọc không? Nhưng nó có hạn chế gì không nhỉ?"  
> *(Gợi ý: Không làm được Composite Key)*

---

## 🎬 PHẦN 3: FLUENT API (5:00 - 9:00)

### Bước 1: Mở file `ApplicationDbContext.cs` (30 giây)

**Hành động:**
```
Mở: Data/ApplicationDbContext.cs
Scroll xuống method OnModelCreating
```

**Script:**
> "Bây giờ đến phần mạnh mẽ nhất - **Fluent API**. Tất cả cấu hình nằm trong method `OnModelCreating`."

### Bước 2: Giải thích Composite Key (1 phút)

**Chỉ vào:**
```csharp
modelBuilder.Entity<EmployeesInProject>()
    .HasKey(ep => new { ep.EmployeeId, ep.ProjectId });
```

**Script:**
> "Đây là **Composite Primary Key** - hai cột kết hợp làm khóa chính. Data Annotations **KHÔNG LÀM ĐƯỢC** điều này!"

### Bước 3: Giải thích quan hệ 1-N (1 phút)

**Chỉ vào:**
```csharp
modelBuilder.Entity<Department>()
    .HasMany(d => d.Employees)           // Department có nhiều Employees
    .WithOne(e => e.Department)          // Employee thuộc một Department
    .HasForeignKey(e => e.DepartmentId)  // Foreign Key
    .OnDelete(DeleteBehavior.SetNull);   // Khi xóa Department, set NULL
```

**Script:**
> "Đọc như tiếng Anh: Department **has many** Employees, **with one** Department, **has foreign key** DepartmentId."

**Vẽ trên bảng (nếu có):**
```
Department (1) ──────< Employee (N)
```

### Bước 4: Giải thích quan hệ 1-1 (1 phút)

**Chỉ vào:**
```csharp
modelBuilder.Entity<Employee>()
    .HasOne(e => e.EmployeeAddress)
    .WithOne(ea => ea.Employee)
    .HasForeignKey<EmployeeAddress>(ea => ea.EmployeeId)
    .OnDelete(DeleteBehavior.Cascade);
```

**Script:**
> "Quan hệ 1-1: Employee **has one** Address, **with one** Employee. Chú ý `HasForeignKey<EmployeeAddress>` chỉ định bên nào chứa Foreign Key."

**Vẽ trên bảng:**
```
Employee (1) ────── (1) EmployeeAddress
```

### Bước 5: Giải thích quan hệ N-N (1.5 phút)

**Chỉ vào:**
```csharp
// Composite Key
modelBuilder.Entity<EmployeesInProject>()
    .HasKey(ep => new { ep.EmployeeId, ep.ProjectId });

// Quan hệ với Employee
modelBuilder.Entity<EmployeesInProject>()
    .HasOne(ep => ep.Employee)
    .WithMany(e => e.EmployeesInProjects)
    .HasForeignKey(ep => ep.EmployeeId);

// Quan hệ với Project
modelBuilder.Entity<EmployeesInProject>()
    .HasOne(ep => ep.Project)
    .WithMany(p => p.EmployeesInProjects)
    .HasForeignKey(ep => ep.ProjectId);
```

**Script:**
> "Quan hệ N-N phức tạp hơn. Chúng ta cần bảng trung gian `EmployeesInProject` với Composite Key. Sau đó cấu hình hai quan hệ 1-N từ bảng trung gian ra Employee và Project."

**Vẽ trên bảng:**
```
Employee (N) ────< EmployeesInProject >──── (N) Project
                   (Composite Key)
```

### Câu hỏi tương tác:
> "Các bạn thấy Fluent API có phức tạp hơn không? Nhưng nó cho phép chúng ta làm những gì mà Data Annotations không làm được?"

---

## 🎬 PHẦN 4: MIGRATION & DATABASE (9:00 - 12:00)

### Bước 1: Chạy Migration (1 phút)

**Hành động:**
```bash
# Mở Terminal
cd c:\Users\Admin\Desktop\github\Net201-LapTrinhWebNangCao\Lab5\Demo01

# Chạy lệnh
dotnet ef migrations add InitialCreate
```

**Script:**
> "Lệnh `dotnet ef migrations add` sẽ phân tích DbContext và tạo file migration. File này chứa code để tạo database schema."

**Chỉ vào:**
```
Mở thư mục: Migrations/
Mở file: <timestamp>_InitialCreate.cs
```

> "Các bạn thấy đây, EF Core đã tự động tạo code SQL để tạo bảng, foreign key, index..."

### Bước 2: Update Database (30 giây)

**Hành động:**
```bash
dotnet ef database update
```

**Script:**
> "Lệnh `update` sẽ thực thi migration và tạo database thật sự."

### Bước 3: Mở SSMS và xem Database (1.5 phút)

**Hành động:**
```
1. Mở SQL Server Management Studio
2. Connect: (localdb)\mssqllocaldb
3. Expand Databases → net201slide5demo01
4. Expand Tables
```

**Script:**
> "Bây giờ chúng ta xem database đã được tạo như thế nào."

**Chỉ vào từng bảng:**
- ✅ Departments
- ✅ Employees
- ✅ EmployeeAddresses
- ✅ Projects
- ✅ EmployeesInProjects

**Mở bảng EmployeesInProjects:**
```sql
Right-click → Design
```

**Script:**
> "Các bạn thấy đây, Primary Key là composite của EmployeeId và ProjectId. Đây là kết quả của Fluent API."

### Bước 4: Xem dữ liệu mẫu (30 giây)

**Hành động:**
```sql
SELECT * FROM Departments;
SELECT * FROM Employees;
SELECT * FROM EmployeesInProjects;
```

**Script:**
> "Dữ liệu mẫu đã được insert tự động nhờ Seed Data trong DbContext."

---

## 🎬 PHẦN 5: Q&A & TỔNG KẾT (12:00 - 15:00)

### Tổng kết kiến thức (1 phút)

**Script:**
> "Chúng ta đã học:"
> 
> ✅ **Data Annotations**: Đơn giản, dễ đọc, dùng cho validation và cấu hình cơ bản  
> ✅ **Fluent API**: Mạnh mẽ, linh hoạt, dùng cho cấu hình phức tạp  
> ✅ **Quan hệ 1-N**: Department ↔ Employee  
> ✅ **Quan hệ 1-1**: Employee ↔ EmployeeAddress  
> ✅ **Quan hệ N-N**: Employee ↔ Project (qua bảng trung gian)  
> ✅ **Composite Key**: Chỉ làm được bằng Fluent API  
> ✅ **Migration**: Code-First approach  

### So sánh nhanh (30 giây)

**Viết lên bảng:**

| Data Annotations | Fluent API |
|------------------|------------|
| ✅ Đơn giản | ✅ Mạnh mẽ |
| ✅ Validation | ✅ Composite Key |
| ❌ Hạn chế | ✅ Tách biệt logic |

### Câu hỏi thường gặp (1.5 phút)

**Q1: "Khi nào dùng Data Annotations, khi nào dùng Fluent API?"**

**A:**
> "Dùng **cả hai**! Data Annotations cho validation (`[Required]`, `[EmailAddress]`...), Fluent API cho database configuration (relationships, composite key...)."

**Q2: "Có thể dùng Fluent API override Data Annotations không?"**

**A:**
> "Có! Fluent API có **độ ưu tiên cao hơn**. Nếu cả hai đều cấu hình cùng một thứ, Fluent API sẽ thắng."

**Q3: "Làm sao để xóa migration?"**

**A:**
```bash
# Xóa migration cuối cùng (chưa apply)
dotnet ef migrations remove

# Rollback database về migration trước
dotnet ef database update <TenMigrationTruoc>
```

### Bài tập về nhà (30 giây)

**Script:**
> "Bài tập về nhà:"
> 
> 1. Thêm entity **Customer** và **Order** với quan hệ 1-N
> 2. Thêm entity **Product** và tạo quan hệ N-N giữa Order và Product
> 3. Sử dụng kết hợp Data Annotations và Fluent API
> 4. Tạo migration và update database

---

## 📌 Ghi chú quan trọng

### Các điểm cần nhấn mạnh:

1. **Composite Key chỉ làm được bằng Fluent API** ⭐
2. **OnDelete Behavior** rất quan trọng (Cascade, SetNull, Restrict)
3. **Navigation Properties** giúp EF Core hiểu quan hệ
4. **Seed Data** giúp có dữ liệu mẫu ngay từ đầu

### Các lỗi thường gặp cần đề cập:

❌ Quên đăng ký DbContext trong `Program.cs`  
❌ Connection String sai  
❌ Quên cài package `Microsoft.EntityFrameworkCore.Tools`  
❌ Không có Primary Key  

### Tips khi demo:

✅ Nói chậm, rõ ràng  
✅ Chỉ vào từng dòng code khi giải thích  
✅ Vẽ sơ đồ quan hệ trên bảng  
✅ Khuyến khích sinh viên hỏi  
✅ Chia sẻ kinh nghiệm thực tế  

---

## 🎯 Checklist sau khi demo

- [ ] Sinh viên hiểu được sự khác biệt giữa Data Annotations và Fluent API
- [ ] Sinh viên biết cách tạo các quan hệ 1-1, 1-N, N-N
- [ ] Sinh viên biết cách chạy migration
- [ ] Sinh viên có thể tự làm bài tập về nhà

---

**Chúc bạn demo thành công! 🎓**

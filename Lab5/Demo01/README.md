# Demo01 - Data Annotations & Fluent API

## 📚 Giới thiệu

Dự án demo minh họa các kỹ thuật **Data Annotations** và **Fluent API** trong **Entity Framework Core** cho môn C#4 (Lập trình C# nâng cao) tại FPT Polytechnic.

### Mục tiêu học tập

- Hiểu và áp dụng **Data Annotations** để cấu hình entity
- Sử dụng **Fluent API** để cấu hình các quan hệ phức tạp
- Thực hành các loại quan hệ:
  - **One-to-Many (1-N)**: Department ↔ Employee
  - **One-to-One (1-1)**: Employee ↔ EmployeeAddress
  - **Many-to-Many (N-N)**: Employee ↔ Project
- Cấu hình **Composite Primary Key**
- Sử dụng **Code-First Migration**

## 🛠️ Yêu cầu hệ thống

- **.NET 10 SDK** hoặc cao hơn
- **SQL Server LocalDB** hoặc **SQL Server Express**
- **Visual Studio 2022** hoặc **Visual Studio Code**
- **SQL Server Management Studio (SSMS)** hoặc **Azure Data Studio** (tùy chọn, để xem database)

## 📂 Cấu trúc dự án

```
Demo01/
├── Data/
│   └── ApplicationDbContext.cs    # DbContext với Fluent API
├── Models/
│   ├── Department.cs              # Entity Phòng ban
│   ├── Employee.cs                # Entity Nhân viên
│   ├── EmployeeAddress.cs         # Entity Địa chỉ (1-1)
│   ├── Project.cs                 # Entity Dự án
│   └── EmployeesInProject.cs      # Junction table (N-N)
├── Migrations/                    # EF Core Migrations
├── Controllers/
├── Views/
└── appsettings.json              # Connection String
```

## 🗄️ Sơ đồ Database

```
┌─────────────────┐
│   Departments   │
│  (Phòng ban)    │
└────────┬────────┘
         │ 1
         │
         │ N
┌────────▼────────┐         ┌──────────────────┐
│    Employees    │ 1 ─── 1 │ EmployeeAddresses│
│   (Nhân viên)   │         │   (Địa chỉ)      │
└────────┬────────┘         └──────────────────┘
         │ N
         │
         │ N (thông qua EmployeesInProjects)
         │
┌────────▼────────┐         ┌──────────────────┐
│EmployeesInProj  │ N ─── 1 │    Projects      │
│  (Trung gian)   │         │   (Dự án)        │
└─────────────────┘         └──────────────────┘
```

## 🚀 Hướng dẫn chạy dự án

### Bước 1: Clone hoặc mở dự án

```bash
cd c:\Users\Admin\Desktop\github\Net201-LapTrinhWebNangCao\Lab5\Demo01
```

### Bước 2: Restore packages

```bash
dotnet restore
```

### Bước 3: Tạo Migration (nếu chưa có)

```bash
dotnet ef migrations add InitialCreate
```

**Giải thích:**
- Lệnh này tạo file migration trong thư mục `Migrations/`
- Migration chứa code để tạo database schema từ các entity models
- EF Core sẽ phân tích DbContext và tạo các lệnh SQL tương ứng

### Bước 4: Cập nhật Database

```bash
dotnet ef database update
```

**Giải thích:**
- Lệnh này thực thi migration và tạo database `net201slide5demo01`
- Tạo tất cả các bảng: Departments, Employees, EmployeeAddresses, Projects, EmployeesInProjects
- Tạo các Foreign Keys, Indexes, và Constraints
- Insert dữ liệu mẫu (seed data)

### Bước 5: Chạy ứng dụng

```bash
dotnet run
```

Mở trình duyệt và truy cập: `https://localhost:5001`

## 📖 Kiến thức chính

### 1. Data Annotations

Data Annotations là các **attribute** đặt trên properties của entity class để cấu hình database.

#### Các Annotations thường dùng:

| Annotation | Mục đích | Ví dụ |
|------------|----------|-------|
| `[Table]` | Chỉ định tên bảng | `[Table("Departments")]` |
| `[Key]` | Đánh dấu Primary Key | `[Key]` |
| `[Required]` | NOT NULL constraint | `[Required]` |
| `[StringLength]` | Giới hạn độ dài chuỗi | `[StringLength(100)]` |
| `[Column]` | Chỉ định tên cột và kiểu | `[Column("FullName", TypeName = "nvarchar(100)")]` |
| `[ForeignKey]` | Chỉ định Foreign Key | `[ForeignKey("Department")]` |
| `[NotMapped]` | Không map vào database | `[NotMapped]` |
| `[EmailAddress]` | Validation email | `[EmailAddress]` |
| `[Range]` | Giới hạn giá trị | `[Range(0, 100)]` |

#### Ví dụ trong code:

```csharp
[Table("Departments")]
public class Department
{
    [Key]
    public int DepartmentId { get; set; }

    [Required]
    [StringLength(100)]
    [Column("DepartmentName", TypeName = "nvarchar(100)")]
    public string Name { get; set; }
}
```

### 2. Fluent API

Fluent API là cách cấu hình **mạnh mẽ hơn** Data Annotations, được viết trong method `OnModelCreating` của DbContext.

#### Ưu điểm của Fluent API:

✅ Cấu hình được những thứ Data Annotations không làm được (ví dụ: Composite Key)  
✅ Tách biệt logic cấu hình khỏi entity class  
✅ Linh hoạt và mạnh mẽ hơn  

#### Ví dụ Composite Key:

```csharp
modelBuilder.Entity<EmployeesInProject>()
    .HasKey(ep => new { ep.EmployeeId, ep.ProjectId });
```

### 3. Các loại quan hệ

#### a) One-to-Many (1-N): Department ↔ Employee

**Ý nghĩa:** Một phòng ban có nhiều nhân viên, mỗi nhân viên thuộc một phòng ban.

**Cấu hình Fluent API:**

```csharp
modelBuilder.Entity<Department>()
    .HasMany(d => d.Employees)           // Department có nhiều Employees
    .WithOne(e => e.Department)          // Employee thuộc một Department
    .HasForeignKey(e => e.DepartmentId)  // Foreign Key
    .OnDelete(DeleteBehavior.SetNull);   // Khi xóa Department, set NULL
```

#### b) One-to-One (1-1): Employee ↔ EmployeeAddress

**Ý nghĩa:** Một nhân viên có một địa chỉ duy nhất.

**Cấu hình Fluent API:**

```csharp
modelBuilder.Entity<Employee>()
    .HasOne(e => e.EmployeeAddress)      // Employee có một Address
    .WithOne(ea => ea.Employee)          // Address thuộc một Employee
    .HasForeignKey<EmployeeAddress>(ea => ea.EmployeeId)
    .OnDelete(DeleteBehavior.Cascade);   // Xóa Employee thì xóa Address
```

#### c) Many-to-Many (N-N): Employee ↔ Project

**Ý nghĩa:** Một nhân viên tham gia nhiều dự án, một dự án có nhiều nhân viên.

**Cách thực hiện:** Sử dụng bảng trung gian `EmployeesInProject` với Composite Key.

**Cấu hình Fluent API:**

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

## 🔍 Kiểm tra Database

### Sử dụng SQL Server Management Studio (SSMS)

1. Mở SSMS
2. Connect tới: `(localdb)\mssqllocaldb`
3. Tìm database: `net201slide5demo01`
4. Xem các bảng và quan hệ

### Sử dụng Command Line

```bash
# Xem danh sách databases
dotnet ef database list

# Xóa database (nếu cần reset)
dotnet ef database drop

# Tạo lại database
dotnet ef database update
```

### Kiểm tra dữ liệu mẫu

```sql
-- Xem phòng ban
SELECT * FROM Departments;

-- Xem nhân viên và phòng ban
SELECT e.FullName, e.Email, d.DepartmentName
FROM Employees e
LEFT JOIN Departments d ON e.DepartmentId = d.DepartmentId;

-- Xem nhân viên trong dự án
SELECT e.FullName, p.ProjectName, ep.Role, ep.WorkloadPercentage
FROM EmployeesInProjects ep
JOIN Employees e ON ep.EmployeeId = e.EmployeeId
JOIN Projects p ON ep.ProjectId = p.ProjectId;
```

## 📝 Các lệnh Migration quan trọng

```bash
# Tạo migration mới
dotnet ef migrations add <TenMigration>

# Xem danh sách migrations
dotnet ef migrations list

# Cập nhật database lên migration mới nhất
dotnet ef database update

# Rollback về migration cụ thể
dotnet ef database update <TenMigration>

# Xóa migration cuối cùng (chưa apply)
dotnet ef migrations remove

# Xóa database
dotnet ef database drop

# Tạo SQL script từ migrations
dotnet ef migrations script
```

## 🎯 Điểm nhấn kỹ thuật

### So sánh Data Annotations vs Fluent API

| Tiêu chí | Data Annotations | Fluent API |
|----------|------------------|------------|
| **Vị trí** | Trên entity class | Trong DbContext |
| **Độ phức tạp** | Đơn giản, dễ đọc | Phức tạp hơn nhưng mạnh mẽ |
| **Composite Key** | ❌ Không hỗ trợ | ✅ Hỗ trợ |
| **Tách biệt logic** | ❌ Trộn lẫn | ✅ Tách biệt rõ ràng |
| **Validation** | ✅ Có | ❌ Không (dùng cho DB config) |

### Khi nào dùng cái gì?

- **Data Annotations**: Validation, cấu hình đơn giản (Required, StringLength, EmailAddress...)
- **Fluent API**: Cấu hình quan hệ, composite key, index, default values...
- **Kết hợp cả hai**: Sử dụng Data Annotations cho validation và Fluent API cho database configuration

## 🐛 Troubleshooting

### Lỗi: "Unable to create an object of type 'ApplicationDbContext'"

**Giải pháp:**
- Đảm bảo `Program.cs` đã đăng ký DbContext
- Kiểm tra Connection String trong `appsettings.json`

### Lỗi: "A network-related or instance-specific error"

**Giải pháp:**
- Kiểm tra SQL Server LocalDB đã được cài đặt
- Chạy: `sqllocaldb info` để xem danh sách instances
- Tạo instance mới: `sqllocaldb create MSSQLLocalDB`

### Lỗi Migration: "The entity type requires a primary key"

**Giải pháp:**
- Đảm bảo mọi entity đều có `[Key]` hoặc property tên `Id` hoặc `<ClassName>Id`
- Với composite key, phải dùng Fluent API

## 📚 Tài liệu tham khảo

- [Entity Framework Core Documentation](https://docs.microsoft.com/en-us/ef/core/)
- [Data Annotations](https://docs.microsoft.com/en-us/ef/core/modeling/entity-properties)
- [Fluent API](https://docs.microsoft.com/en-us/ef/core/modeling/)
- [Relationships](https://docs.microsoft.com/en-us/ef/core/modeling/relationships)

## 👨‍🏫 Tác giả

**Giảng viên:** FPT Polytechnic  
**Môn học:** C#4 - Lập trình C# nâng cao  
**Bài học:** Slide 5 - Data Annotations & Fluent API  

---

**Chúc các bạn học tập tốt! 🎓**

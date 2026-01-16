# Demo03 - InverseProperty (Multiple Relationships)

## 📚 Giới thiệu

Dự án demo minh họa **InverseProperty** - Xử lý nhiều quan hệ giữa 2 thực thể trong Entity Framework Core cho môn C#4 tại FPT Polytechnic.

### Mục tiêu học tập

- ✅ Hiểu vấn đề khi có **nhiều quan hệ** giữa 2 entity
- ✅ Sử dụng **[InverseProperty]** để chỉ định rõ ràng mapping
- ✅ So sánh InverseProperty vs Fluent API
- ✅ Thực hành với scenario thực tế: Chuyến bay & Sân bay

## 🛠️ Yêu cầu hệ thống

- **.NET 10 SDK** hoặc cao hơn
- **SQL Server LocalDB** hoặc **SQL Server Express**
- **Visual Studio 2022** hoặc **Visual Studio Code**

## 📂 Cấu trúc dự án

```
Demo03/
├── Data/
│   └── Demo03Context.cs         # DbContext với giải thích InverseProperty
├── Models/
│   ├── Airport.cs               # Entity với [InverseProperty]
│   └── Flight.cs                # Entity với 2 FK đến Airport
├── Controllers/
│   ├── FlightsController.cs     # CRUD Chuyến bay
│   └── AirportsController.cs    # CRUD Sân bay
└── appsettings.json             # Connection String
```

## 🗄️ Sơ đồ Database

```
┌─────────────────────┐
│      Airports       │
│    (Sân bay)        │
└──────┬──────┬───────┘
       │      │
       │ 1    │ 1
       │      │
       │ N    │ N
       │      │
┌──────▼──────▼───────┐
│      Flights        │
│   (Chuyến bay)      │
│                     │
│ DepartureAirportId  │ ──→ Airports (Sân bay đi)
│ ArrivalAirportId    │ ──→ Airports (Sân bay đến)
└─────────────────────┘
```

**Vấn đề:** Một Flight có 2 FK trỏ đến cùng 1 entity (Airport)  
**Giải pháp:** Dùng `[InverseProperty]` để EF Core hiểu rõ mapping

## 🚀 Hướng dẫn chạy dự án

### Bước 1: Mở dự án

```bash
cd c:\Users\Admin\Desktop\github\Net201-LapTrinhWebNangCao\Lab5\Demo03
```

### Bước 2: Restore packages

```bash
dotnet restore
```

### Bước 3: Cấu hình Connection String

File `appsettings.json` đã được cấu hình:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=net201slide5demo03;..."
  }
}
```

**Database:** `net201slide5demo03`

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

## 📖 Kiến thức chính

### 1. Vấn đề: Nhiều quan hệ giữa 2 thực thể

#### Scenario

Một **Flight** (Chuyến bay) có:
- `DepartureAirport` - Sân bay đi
- `ArrivalAirport` - Sân bay đến

Một **Airport** (Sân bay) có:
- `DepartingFlights` - Các chuyến bay đi từ đây
- `ArrivingFlights` - Các chuyến bay đến đây

#### Vấn đề nếu KHÔNG có InverseProperty

Nếu chỉ định nghĩa như sau:

```csharp
public class Airport
{
    public ICollection<Flight> DepartingFlights { get; set; }
    public ICollection<Flight> ArrivingFlights { get; set; }
}

public class Flight
{
    public Airport DepartureAirport { get; set; }
    public Airport ArrivalAirport { get; set; }
}
```

**EF Core sẽ KHÔNG BIẾT:**
- `DepartingFlights` map với `DepartureAirport` hay `ArrivalAirport`?
- `ArrivingFlights` map với `DepartureAirport` hay `ArrivalAirport`?

**Kết quả:** Lỗi hoặc tạo thêm FK không mong muốn!

### 2. Giải pháp: InverseProperty

#### Trong Airport.cs

```csharp
public class Airport
{
    [InverseProperty("DepartureAirport")]
    public virtual ICollection<Flight> DepartingFlights { get; set; }

    [InverseProperty("ArrivalAirport")]
    public virtual ICollection<Flight> ArrivingFlights { get; set; }
}
```

**Giải thích:**
- `[InverseProperty("DepartureAirport")]` → `DepartingFlights` map với `DepartureAirport`
- `[InverseProperty("ArrivalAirport")]` → `ArrivingFlights` map với `ArrivalAirport`

#### Trong Flight.cs

```csharp
public class Flight
{
    [ForeignKey("DepartureAirport")]
    public int DepartureAirportId { get; set; }
    public virtual Airport? DepartureAirport { get; set; }

    [ForeignKey("ArrivalAirport")]
    public int ArrivalAirportId { get; set; }
    public virtual Airport? ArrivalAirport { get; set; }
}
```

### 3. InverseProperty vs Fluent API

#### Cách 1: Dùng InverseProperty (Đơn giản hơn)

```csharp
// Trong Airport.cs
[InverseProperty("DepartureAirport")]
public ICollection<Flight> DepartingFlights { get; set; }

[InverseProperty("ArrivalAirport")]
public ICollection<Flight> ArrivingFlights { get; set; }
```

**Ưu điểm:**
- ✅ Code ngắn gọn, dễ đọc
- ✅ Mapping rõ ràng ngay trong Model
- ✅ Không cần cấu hình thêm trong DbContext

#### Cách 2: Dùng Fluent API

```csharp
// Trong DbContext.OnModelCreating
modelBuilder.Entity<Flight>()
    .HasOne(f => f.DepartureAirport)
    .WithMany(a => a.DepartingFlights)
    .HasForeignKey(f => f.DepartureAirportId)
    .OnDelete(DeleteBehavior.Restrict);

modelBuilder.Entity<Flight>()
    .HasOne(f => f.ArrivalAirport)
    .WithMany(a => a.ArrivingFlights)
    .HasForeignKey(f => f.ArrivalAirportId)
    .OnDelete(DeleteBehavior.Restrict);
```

**Ưu điểm:**
- ✅ Linh hoạt hơn
- ✅ Có thể cấu hình thêm DeleteBehavior
- ✅ Tách biệt logic cấu hình khỏi Model

### 4. Khi nào dùng InverseProperty?

**Nên dùng InverseProperty khi:**
- ✅ Có nhiều quan hệ giữa 2 entity
- ✅ Muốn code đơn giản, dễ đọc
- ✅ Không cần cấu hình phức tạp (DeleteBehavior, etc.)

**Nên dùng Fluent API khi:**
- ✅ Cần cấu hình chi tiết (OnDelete, Constraints...)
- ✅ Muốn tách biệt logic cấu hình
- ✅ Có nhiều cấu hình phức tạp

## 🧪 Test Demo

### Scenario 1: Tạo chuyến bay mới

1. Truy cập `/Flights/Create`
2. Chọn:
   - Số hiệu: `VN301`
   - Sân bay đi: `Tân Sơn Nhất`
   - Sân bay đến: `Nội Bài`
3. Kết quả: ✅ Lưu thành công với đúng 2 FK

### Scenario 2: Xem danh sách chuyến bay

1. Truy cập `/Flights`
2. Kết quả: Hiển thị bảng với cột "Sân bay đi" và "Sân bay đến" rõ ràng

### Scenario 3: Kiểm tra trong Database

```sql
SELECT 
    f.FlightNumber,
    dep.Name AS 'Sân bay đi',
    arr.Name AS 'Sân bay đến'
FROM Flights f
JOIN Airports dep ON f.DepartureAirportId = dep.AirportId
JOIN Airports arr ON f.ArrivalAirportId = arr.AirportId
```

**Kết quả:** Dữ liệu đúng với 2 quan hệ riêng biệt!

## 📝 Seed Data

### Airports (Sân bay)

| AirportId | Code | Name |
|-----------|------|------|
| 1 | SGN | Sân bay Quốc tế Tân Sơn Nhất |
| 2 | HAN | Sân bay Quốc tế Nội Bài |
| 3 | DAD | Sân bay Quốc tế Đà Nẵng |

### Flights (Chuyến bay)

| FlightNumber | Departure | Arrival |
|--------------|-----------|---------|
| VN101 | SGN → HAN | Tân Sơn Nhất → Nội Bài |
| VN102 | HAN → SGN | Nội Bài → Tân Sơn Nhất |
| VN201 | SGN → DAD | Tân Sơn Nhất → Đà Nẵng |

## 🎯 Điểm nhấn kỹ thuật

### InverseProperty là gì?

**InverseProperty** là một Data Annotation cho phép bạn chỉ định rõ ràng:
- Navigation property nào (bên entity A)
- Map với navigation property nào (bên entity B)

### Tại sao cần InverseProperty?

Khi có **nhiều hơn 1 quan hệ** giữa 2 entity, EF Core không thể tự động suy ra mapping. InverseProperty giúp làm rõ điều này.

### Cú pháp

```csharp
[InverseProperty("TênNavigationPropertyBênKia")]
public ICollection<Entity> CollectionProperty { get; set; }
```

## 🐛 Troubleshooting

### Lỗi: "Unable to determine the relationship"

**Nguyên nhân:** Thiếu InverseProperty hoặc Fluent API

**Giải pháp:** Thêm `[InverseProperty]` như trong Airport.cs

### Lỗi: FK constraint conflict

**Nguyên nhân:** Seed data có vấn đề

**Giải pháp:** Kiểm tra DepartureAirportId và ArrivalAirportId trong seed data

## 📚 Tài liệu tham khảo

- [InverseProperty Attribute](https://docs.microsoft.com/en-us/ef/core/modeling/relationships)
- [Fluent API Relationships](https://docs.microsoft.com/en-us/ef/core/modeling/relationships)
- [Multiple Relationships](https://docs.microsoft.com/en-us/ef/core/modeling/relationships#multiple-relationships)

## 👨‍🏫 Tác giả

**Trợ giảng:** FPT Polytechnic  
**Môn học:** C#4 - Lập trình Web Nâng cao  
**Bài học:** Slide 5 - Data Annotations & Fluent API (Slide 11: InverseProperty)  

---

**Chúc các bạn học tập tốt! 🎓**

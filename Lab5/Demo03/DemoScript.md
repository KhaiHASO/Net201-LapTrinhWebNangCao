# Kịch Bản Demo - InverseProperty (Multiple Relationships)
## Thời lượng: 15 phút

---

## 📋 Chuẩn bị trước khi demo

### Công cụ cần mở:
- ✅ Visual Studio Code / Visual Studio 2022
- ✅ Terminal / PowerShell
- ✅ Trình duyệt (Chrome/Edge)
- ✅ Slide 11 (InverseProperty)

### Checklist:
- [ ] Database `net201slide5demo03` đã được tạo
- [ ] Đã mở sẵn file `Airport.cs` và `Flight.cs`
- [ ] Đã test chạy ứng dụng thành công

---

## ⏱️ Timeline Demo (15 phút)

| Thời gian | Nội dung | Hành động |
|-----------|----------|-----------|
| 0:00 - 2:00 | Giới thiệu vấn đề | Slide + Vẽ sơ đồ |
| 2:00 - 7:00 | **Giải thích InverseProperty** | Live code + Giải thích |
| 7:00 - 12:00 | **Demo trên Web** | Tạo chuyến bay |
| 12:00 - 15:00 | So sánh & Q&A | Tổng kết |

---

## 🎬 PHẦN 0: GIỚI THIỆU VẤN ĐỀ (0:00 - 2:00)

### Script:

> "Chào các bạn! Hôm nay chúng ta sẽ học về **InverseProperty** - một kỹ thuật quan trọng khi làm việc với **nhiều quan hệ giữa 2 thực thể**.
> 
> Scenario của chúng ta hôm nay là: **Chuyến bay và Sân bay**."

### Vẽ sơ đồ trên bảng:

```
Airport (Sân bay)
    ↓ 1
    ↓ N
Flight (Chuyến bay)
    - DepartureAirport (Sân bay đi)
    - ArrivalAirport (Sân bay đến)
```

### Giải thích vấn đề:

> "Các bạn thấy không? Một chuyến bay có:
> - Sân bay ĐI (DepartureAirport)
> - Sân bay ĐẾN (ArrivalAirport)
> 
> Cả 2 đều là Airport! Đây là **2 quan hệ khác nhau** giữa Flight và Airport.
> 
> Vấn đề: **EF Core sẽ KHÔNG BIẾT** cách map nếu không có InverseProperty!"

---

## 🎬 PHẦN 1: GIẢI THÍCH INVERSEPROPERTY (2:00 - 7:00)

### Bước 1: Mở file Airport.cs (30 giây)

**Hành động:**
```
Mở: Models/Airport.cs
```

**Script:**
> "Chúng ta sẽ xem code của Airport entity."

### Bước 2: Giải thích vấn đề (1 phút)

**Chỉ vào:**
```csharp
public ICollection<Flight> DepartingFlights { get; set; }
public ICollection<Flight> ArrivingFlights { get; set; }
```

**Script:**
> "Airport có 2 collection:
> - **DepartingFlights**: Các chuyến bay ĐI TỪ sân bay này
> - **ArrivingFlights**: Các chuyến bay ĐẾN sân bay này
> 
> Câu hỏi: EF Core làm sao biết DepartingFlights map với DepartureAirport hay ArrivalAirport?
> 
> **Trả lời: KHÔNG BIẾT!** Nếu không có InverseProperty."

### Bước 3: Giải thích InverseProperty (2 phút)

**Chỉ vào:**
```csharp
[InverseProperty("DepartureAirport")]
public virtual ICollection<Flight> DepartingFlights { get; set; }
```

**Script:**
> "⭐ **[InverseProperty("DepartureAirport")]**
> 
> Dòng này nói với EF Core:
> - Collection **DepartingFlights** (bên Airport)
> - Map với property **DepartureAirport** (bên Flight)
> 
> Tương tự:"

**Chỉ vào:**
```csharp
[InverseProperty("ArrivalAirport")]
public virtual ICollection<Flight> ArrivingFlights { get; set; }
```

> "**[InverseProperty("ArrivalAirport")]**
> 
> - Collection **ArrivingFlights** (bên Airport)
> - Map với property **ArrivalAirport** (bên Flight)
> 
> Bây giờ EF Core hiểu rõ ràng 2 mối quan hệ!"

### Bước 4: Mở file Flight.cs (1 phút)

**Hành động:**
```
Mở: Models/Flight.cs
```

**Chỉ vào:**
```csharp
[ForeignKey("DepartureAirport")]
public int DepartureAirportId { get; set; }
public virtual Airport? DepartureAirport { get; set; }

[ForeignKey("ArrivalAirport")]
public int ArrivalAirportId { get; set; }
public virtual Airport? ArrivalAirport { get; set; }
```

**Script:**
> "Bên Flight, chúng ta có 2 FK:
> - **DepartureAirportId** → **DepartureAirport**
> - **ArrivalAirportId** → **ArrivalAirport**
> 
> Nhờ InverseProperty bên Airport:
> - DepartureAirport ↔ DepartingFlights
> - ArrivalAirport ↔ ArrivingFlights
> 
> Mọi thứ đã rõ ràng!"

### Bước 5: Mở DbContext (30 giây)

**Hành động:**
```
Mở: Data/Demo03Context.cs
Scroll đến comment
```

**Chỉ vào comment:**
```csharp
// Nhờ có [InverseProperty] trong Airport.cs,
// chúng ta KHÔNG CẦN cấu hình Fluent API cho quan hệ này!
```

**Script:**
> "Điểm quan trọng: Nhờ có InverseProperty, chúng ta **KHÔNG CẦN** cấu hình Fluent API!
> 
> Nếu không có InverseProperty, bạn phải viết Fluent API dài dòng như trong comment."

### Câu hỏi tương tác:
> "Các bạn thấy InverseProperty có đơn giản hơn Fluent API không?"

---

## 🎬 PHẦN 2: DEMO TRÊN WEB (7:00 - 12:00)

### Bước 1: Chạy ứng dụng (30 giây)

**Hành động:**
```bash
dotnet run
```

**Script:**
> "Tôi sẽ chạy ứng dụng lên."

### Bước 2: Xem danh sách chuyến bay (1 phút)

**Hành động:**
```
Truy cập: /Flights
```

**Script:**
> "Đây là danh sách chuyến bay. Các bạn chú ý 2 cột:
> - **Sân bay đi** (DepartureAirport)
> - **Sân bay đến** (ArrivalAirport)
> 
> Cả 2 đều là Airport nhưng **vai trò khác nhau**!"

### Bước 3: Tạo chuyến bay mới (2 phút)

**Hành động:**
```
1. Click "Create New"
2. Nhập:
   - Flight Number: VN301
   - Departure Airport: Tân Sơn Nhất
   - Arrival Airport: Nội Bài
3. Click "Create"
```

**Script:**
> "Tôi sẽ tạo chuyến bay mới:
> - Số hiệu: VN301
> - Từ: Tân Sơn Nhất
> - Đến: Nội Bài
> 
> Chú ý: Có 2 dropdown riêng biệt cho sân bay đi và sân bay đến!"

**Kết quả:**
- ✅ Chuyến bay được tạo thành công
- ✅ Hiển thị đúng sân bay đi và sân bay đến

**Script:**
> "✅ Thành công! Dữ liệu đã được lưu với đúng 2 FK:
> - DepartureAirportId = 1 (Tân Sơn Nhất)
> - ArrivalAirportId = 2 (Nội Bài)"

### Bước 4: Xem chi tiết chuyến bay (1 phút)

**Hành động:**
```
Click "Details" trên chuyến bay vừa tạo
```

**Script:**
> "Xem chi tiết chuyến bay, các bạn thấy rõ ràng:
> - Sân bay đi: Tân Sơn Nhất
> - Sân bay đến: Nội Bài
> 
> Đây là kết quả của InverseProperty!"

### Bước 5: (Tùy chọn) Xem trong Database (1 phút)

**Nếu có thời gian, mở SSMS:**

**Hành động:**
```sql
SELECT 
    f.FlightNumber,
    dep.Name AS 'Sân bay đi',
    arr.Name AS 'Sân bay đến'
FROM Flights f
JOIN Airports dep ON f.DepartureAirportId = dep.AirportId
JOIN Airports arr ON f.ArrivalAirportId = arr.AirportId
```

**Script:**
> "Trong database, chúng ta thấy:
> - 2 Foreign Keys riêng biệt
> - 2 JOIN khác nhau
> - Dữ liệu đúng với 2 quan hệ!"

---

## 🎬 PHẦN 3: SO SÁNH & Q&A (12:00 - 15:00)

### So sánh InverseProperty vs Fluent API (1 phút)

**Viết lên bảng:**

| InverseProperty | Fluent API |
|-----------------|------------|
| ✅ Đơn giản | ✅ Linh hoạt |
| ✅ Ngắn gọn | ✅ Mạnh mẽ |
| ✅ Trong Model | ✅ Trong DbContext |
| ❌ Ít tùy chỉnh | ✅ Nhiều tùy chỉnh |

**Script:**
> "Khi nào dùng InverseProperty?
> - Khi có nhiều quan hệ giữa 2 entity
> - Khi muốn code đơn giản
> 
> Khi nào dùng Fluent API?
> - Khi cần cấu hình chi tiết (DeleteBehavior...)
> - Khi muốn tách biệt logic"

### Tổng kết (1 phút)

**Script:**
> "Chúng ta đã học:
> 
> ✅ **Vấn đề:** Nhiều quan hệ giữa 2 entity  
> ✅ **Giải pháp:** InverseProperty  
> ✅ **Cú pháp:** `[InverseProperty("TênPropertyBênKia")]`  
> ✅ **Ưu điểm:** Đơn giản, rõ ràng  
> ✅ **Demo:** Chuyến bay và Sân bay  

### Câu hỏi thường gặp (1 phút)

**Q1: "Có thể dùng cả InverseProperty và Fluent API không?"**

**A:**
> "Có! Nhưng Fluent API sẽ override InverseProperty. Nên chọn một trong hai để code rõ ràng."

**Q2: "Khi nào bắt buộc phải dùng InverseProperty?"**

**A:**
> "Khi có **2 hoặc nhiều hơn** quan hệ giữa 2 entity. Ví dụ:
> - Employee - Manager (cùng là Employee)
> - Flight - Airport (như demo hôm nay)"

**Q3: "InverseProperty có thay thế được ForeignKey không?"**

**A:**
> "Không! InverseProperty chỉ định **collection nào map với property nào**. ForeignKey chỉ định **FK column**. Hai cái khác nhau!"

### Bài tập về nhà (30 giây)

**Script:**
> "Bài tập về nhà:
> 
> 1. Tạo entity **Employee** tự tham chiếu (ManagerId)
> 2. Dùng InverseProperty cho:
>    - Subordinates (Nhân viên cấp dưới)
>    - Manager (Quản lý)
> 3. Test tạo cây phân cấp nhân viên"

---

## 📌 Ghi chú quan trọng

### Các điểm cần nhấn mạnh:

1. **InverseProperty giải quyết vấn đề gì?** ⭐
2. **Cú pháp InverseProperty** ⭐
3. **So sánh với Fluent API** ⭐
4. **Khi nào dùng InverseProperty** ⭐

### Tips khi demo:

✅ Vẽ sơ đồ rõ ràng trên bảng  
✅ Chỉ vào từng dòng code khi giải thích  
✅ Nhấn mạnh "2 quan hệ khác nhau"  
✅ Demo thực tế trên Web  
✅ So sánh với Fluent API  

### Lỗi thường gặp cần đề cập:

❌ Quên InverseProperty → EF Core không hiểu mapping  
❌ Viết sai tên property trong InverseProperty  
❌ Dùng InverseProperty nhưng không có Navigation Property  

---

## 🎯 Checklist sau khi demo

- [ ] Sinh viên hiểu vấn đề nhiều quan hệ
- [ ] Sinh viên biết cách dùng InverseProperty
- [ ] Sinh viên biết so sánh với Fluent API
- [ ] Sinh viên có thể tự làm bài tập về nhà

---

**Chúc bạn demo thành công! 🎓**

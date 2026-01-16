using Microsoft.EntityFrameworkCore;
using Demo03.Models;

namespace Demo03.Data
{
    /// <summary>
    /// Demo03Context - DbContext cho Demo03
    /// Minh họa: InverseProperty không cần cấu hình thêm trong Fluent API
    /// </summary>
    public class Demo03Context : DbContext
    {
        public Demo03Context(DbContextOptions<Demo03Context> options)
            : base(options)
        {
        }

        // DbSet - Đại diện cho các bảng trong database
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Flight> Flights { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ========================================
            // LƯU Ý QUAN TRỌNG:
            // ========================================
            // Nhờ có [InverseProperty] trong Airport.cs,
            // EF Core tự động hiểu mapping.
            // 
            // NHƯNG: SQL Server không cho phép 2 FK có CASCADE DELETE
            // cùng trỏ đến 1 bảng (gây ra multiple cascade paths).
            // 
            // Giải pháp: Dùng Fluent API để set OnDelete = Restrict

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


            // ========================================
            // CẤU HÌNH BỔ SUNG
            // ========================================

            // Index cho tìm kiếm nhanh
            modelBuilder.Entity<Airport>()
                .HasIndex(a => a.Code)
                .IsUnique();

            modelBuilder.Entity<Flight>()
                .HasIndex(f => f.FlightNumber)
                .IsUnique();

            // ========================================
            // SEED DATA - DỮ LIỆU MẪU
            // ========================================

            // Thêm 2 sân bay
            modelBuilder.Entity<Airport>().HasData(
                new Airport
                {
                    AirportId = 1,
                    Code = "SGN",
                    Name = "Sân bay Quốc tế Tân Sơn Nhất",
                    City = "Hồ Chí Minh",
                    Country = "Việt Nam"
                },
                new Airport
                {
                    AirportId = 2,
                    Code = "HAN",
                    Name = "Sân bay Quốc tế Nội Bài",
                    City = "Hà Nội",
                    Country = "Việt Nam"
                },
                new Airport
                {
                    AirportId = 3,
                    Code = "DAD",
                    Name = "Sân bay Quốc tế Đà Nẵng",
                    City = "Đà Nẵng",
                    Country = "Việt Nam"
                }
            );

            // Thêm chuyến bay mẫu
            modelBuilder.Entity<Flight>().HasData(
                new Flight
                {
                    FlightId = 1,
                    FlightNumber = "VN101",
                    DepartureAirportId = 1, // Tân Sơn Nhất
                    ArrivalAirportId = 2,   // Nội Bài
                    DepartureTime = new DateTime(2024, 6, 15, 8, 0, 0),
                    ArrivalTime = new DateTime(2024, 6, 15, 10, 15, 0),
                    TotalSeats = 180,
                    Status = "Scheduled"
                },
                new Flight
                {
                    FlightId = 2,
                    FlightNumber = "VN102",
                    DepartureAirportId = 2, // Nội Bài
                    ArrivalAirportId = 1,   // Tân Sơn Nhất
                    DepartureTime = new DateTime(2024, 6, 15, 14, 0, 0),
                    ArrivalTime = new DateTime(2024, 6, 15, 16, 15, 0),
                    TotalSeats = 180,
                    Status = "Scheduled"
                },
                new Flight
                {
                    FlightId = 3,
                    FlightNumber = "VN201",
                    DepartureAirportId = 1, // Tân Sơn Nhất
                    ArrivalAirportId = 3,   // Đà Nẵng
                    DepartureTime = new DateTime(2024, 6, 15, 9, 30, 0),
                    ArrivalTime = new DateTime(2024, 6, 15, 10, 45, 0),
                    TotalSeats = 150,
                    Status = "Scheduled"
                }
            );
        }
    }
}

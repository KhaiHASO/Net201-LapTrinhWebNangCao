using Microsoft.EntityFrameworkCore;
using Demo02.Models;

namespace Demo02.Data
{
    /// <summary>
    /// DemoContext - DbContext cho Demo02
    /// Minh họa: Fluent API và Cascade Delete
    /// </summary>
    public class DemoContext : DbContext
    {
        public DemoContext(DbContextOptions<DemoContext> options)
            : base(options)
        {
        }

        // DbSet - Đại diện cho các bảng trong database
        public DbSet<ClassRoom> ClassRooms { get; set; }
        public DbSet<Student> Students { get; set; }

        /// <summary>
        /// OnModelCreating - Cấu hình Fluent API
        /// Trọng tâm: Cascade Delete
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ========================================
            // FLUENT API: CÁC CẤU HÌNH QUAN HỆ
            // ========================================

            // Quan hệ 1-N: ClassRoom (1) - Student (N)
            // Một lớp học có nhiều sinh viên
            modelBuilder.Entity<ClassRoom>()
                .HasMany(c => c.Students)              // ClassRoom có nhiều Students
                .WithOne(s => s.ClassRoom)             // Student thuộc một ClassRoom
                .HasForeignKey(s => s.ClassRoomId)     // Foreign Key
                .OnDelete(DeleteBehavior.Cascade);     // ⭐ CASCADE DELETE: Xóa ClassRoom → Xóa tất cả Students

            // Giải thích Cascade Delete:
            // - Khi xóa một ClassRoom, EF Core sẽ TỰ ĐỘNG xóa tất cả Students thuộc lớp đó
            // - Không cần phải xóa Students thủ công
            // - Đây là hành vi quan trọng cần demo cho sinh viên

            // ========================================
            // CẤU HÌNH BỔ SUNG
            // ========================================

            // Index cho tìm kiếm nhanh
            modelBuilder.Entity<Student>()
                .HasIndex(s => s.Email)
                .IsUnique(); // Email phải là duy nhất

            modelBuilder.Entity<ClassRoom>()
                .HasIndex(c => c.ClassCode)
                .IsUnique(); // Mã lớp phải là duy nhất

            // ========================================
            // SEED DATA - DỮ LIỆU MẪU
            // ========================================

            // Thêm 2 lớp học
            modelBuilder.Entity<ClassRoom>().HasData(
                new ClassRoom
                {
                    ClassRoomId = 1,
                    ClassCode = "NET201",
                    ClassName = "Lập trình Web Nâng cao - Sáng"
                },
                new ClassRoom
                {
                    ClassRoomId = 2,
                    ClassCode = "NET202",
                    ClassName = "Lập trình Web Nâng cao - Chiều"
                }
            );

            // Thêm 4 sinh viên
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    StudentId = 1,
                    StudentCode = "PH12345",
                    FullName = "Nguyễn Văn An",
                    GPA = 8.5m,
                    Email = "annv@fpt.edu.vn",
                    PhoneNumber = "0901234567",
                    DateOfBirth = new DateTime(2003, 5, 15),
                    ClassRoomId = 1
                },
                new Student
                {
                    StudentId = 2,
                    StudentCode = "PH12346",
                    FullName = "Trần Thị Bình",
                    GPA = 9.2m,
                    Email = "binhtt@fpt.edu.vn",
                    PhoneNumber = "0907654321",
                    DateOfBirth = new DateTime(2003, 8, 20),
                    ClassRoomId = 1
                },
                new Student
                {
                    StudentId = 3,
                    StudentCode = "PH12347",
                    FullName = "Lê Văn Cường",
                    GPA = 7.8m,
                    Email = "cuonglv@fpt.edu.vn",
                    PhoneNumber = "0912345678",
                    DateOfBirth = new DateTime(2003, 3, 10),
                    ClassRoomId = 2
                },
                new Student
                {
                    StudentId = 4,
                    StudentCode = "PH12348",
                    FullName = "Phạm Thị Dung",
                    GPA = 8.9m,
                    Email = "dungpt@fpt.edu.vn",
                    PhoneNumber = "0923456789",
                    DateOfBirth = new DateTime(2003, 11, 25),
                    ClassRoomId = 2
                }
            );
        }
    }
}

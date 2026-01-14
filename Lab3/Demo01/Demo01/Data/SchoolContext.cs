using Demo01.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo01.Data
{
    // SchoolContext kế thừa từ DbContext đóng vai trò cầu nối
    public class SchoolContext : DbContext
    {
        // DbSet đại diện cho các bảng dữ liệu
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        // Ghi đè phương thức OnConfiguring để cấu hình connection string
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Sử dụng SQL Server với LocalDB cho mục đích demo nhanh
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Demo01EFCoreSlide3;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }
    }
}

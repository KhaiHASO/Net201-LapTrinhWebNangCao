using Microsoft.EntityFrameworkCore;
using NET201Slide8Demo02.Models;

namespace NET201Slide8Demo02.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Data (Dữ liệu mẫu) để minh họa

            // 1. Tạo một Branch
            modelBuilder.Entity<Branch>().HasData(
                new Branch { BranchId = 1, BranchName = "CNTT" }
            );

            // 2. Tạo một Address
            modelBuilder.Entity<Address>().HasData(
                new Address { AddressId = 1, Street = "Hanoi" }
            );

            // 3. Tạo các Student
            modelBuilder.Entity<Student>().HasData(
                // Student 1: Có cả Branch và Address
                // Điều này minh họa một bản ghi tiêu chuẩn.
                new Student 
                { 
                    StudentId = 1, 
                    Name = "Nguyen Van A", 
                    BranchId = 1, // Phải có Branch
                    AddressId = 1 // Có Address
                },

                // Student 2: Có Branch nhưng KHÔNG CÓ Address
                // Điều này minh họa LEFT JOIN. Mặc dù Address được join, 
                // bản ghi này xuất hiện với Address là null.
                new Student 
                { 
                    StudentId = 2, 
                    Name = "Tran Van B", 
                    BranchId = 1, // Phải có Branch
                    AddressId = null // KHÔNG CÓ Address -> Kết quả Left Join
                }
            );
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Demo01.Models;

namespace Demo01.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, FullName = "Nguyen Van A", Age = 20, Major = "IT" },
                new Student { Id = 2, FullName = "Tran Thi B", Age = 19, Major = "Marketing" },
                new Student { Id = 3, FullName = "Le Van C", Age = 21, Major = "IT" },
                new Student { Id = 4, FullName = "Pham Thi D", Age = 22, Major = "Graphic Design" },
                new Student { Id = 5, FullName = "Hoang Van E", Age = 18, Major = "Business" },
                new Student { Id = 6, FullName = "Do Thi F", Age = 20, Major = "IT" },
                new Student { Id = 7, FullName = "Ngo Van G", Age = 23, Major = "Marketing" },
                new Student { Id = 8, FullName = "Bui Thi H", Age = 19, Major = "Graphic Design" },
                new Student { Id = 9, FullName = "Vu Van I", Age = 21, Major = "Business" },
                new Student { Id = 10, FullName = "Dinh Thi J", Age = 22, Major = "IT" }
            );
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Demo02.Models;

namespace Demo02.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Name = "Nguyen Van A", Status = "Active", IsActive = true },
                new Customer { Id = 2, Name = "Tran Thi B", Status = "Inactive", IsActive = false },
                new Customer { Id = 3, Name = "Le Van C", Status = "Active", IsActive = true },
                new Customer { Id = 4, Name = "Pham Thi D", Status = "New", IsActive = true }, // New but considered active for demo? Or IsActive determines it.
                new Customer { Id = 5, Name = "Hoang Van E", Status = "Inactive", IsActive = false },
                new Customer { Id = 6, Name = "Do Thi F", Status = "Active", IsActive = true },
                new Customer { Id = 7, Name = "Ngo Van G", Status = "Active", IsActive = true }
            );
        }
    }
}

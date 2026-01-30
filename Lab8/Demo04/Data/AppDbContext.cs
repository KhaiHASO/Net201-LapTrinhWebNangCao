using Microsoft.EntityFrameworkCore;
using Demo04.Models;

namespace Demo04.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Tạo dữ liệu mẫu
            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, FullName = "Khach Hang Demo Explicit" }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order { OrderId = 1, CustomerId = 1, OrderDate = DateTime.Now.AddDays(-10), TotalAmount = 150.00m },
                new Order { OrderId = 2, CustomerId = 1, OrderDate = DateTime.Now.AddDays(-5), TotalAmount = 200.50m },
                new Order { OrderId = 3, CustomerId = 1, OrderDate = DateTime.Now, TotalAmount = 99.99m }
            );
        }
    }
}

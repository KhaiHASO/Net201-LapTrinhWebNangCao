using Demo01.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo01.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed Data (Dữ liệu mẫu)
        modelBuilder.Entity<Customer>().HasData(
            new Customer { CustomerId = 1, CustomerName = "FPT Polytechnic" },
            new Customer { CustomerId = 2, CustomerName = "Nguyen Van A" }
        );

        modelBuilder.Entity<Address>().HasData(
            new Address { AddressId = 1, Street = "13 Trinh Van Bo", City = "Ha Noi", CustomerId = 1 },
            new Address { AddressId = 2, Street = "Cong vien phan mem Quang Trung", City = "Ho Chi Minh", CustomerId = 1 },
            new Address { AddressId = 3, Street = "Nha Rieng", City = "Da Nang", CustomerId = 2 }
        );

        modelBuilder.Entity<Order>().HasData(
            new Order { OrderId = 1, OrderName = "Order #001 - Laptop", CustomerId = 1 },
            new Order { OrderId = 2, OrderName = "Order #002 - Mouse", CustomerId = 1 },
            new Order { OrderId = 3, OrderName = "Order #003 - Keyboard", CustomerId = 2 },
            new Order { OrderId = 4, OrderName = "Order #004 - Monitor", CustomerId = null } // Đơn hàng mồ côi để kiểm tra giá trị null
        );
    }
}

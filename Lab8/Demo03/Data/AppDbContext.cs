using Demo03.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo03.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // 1. Enable Lazy Loading (Slide 20)
        optionsBuilder.UseLazyLoadingProxies(); 
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 2. Seeding Data (Slide 21)
        modelBuilder.Entity<Customer>().HasData(
            new Customer { CustomerId = 1, CustomerName = "Thay giao Thinh" }, // Thay giao Thinh
            new Customer { CustomerId = 2, CustomerName = "Sinh vien FPT" } // Sinh vien FPT
        );

        modelBuilder.Entity<Order>().HasData(
            // Orders for Customer 1
            new Order { OrderId = 1, OrderName = "Order #101 - Laptop Dell", CustomerId = 1 },
            new Order { OrderId = 2, OrderName = "Order #102 - IPhone 15", CustomerId = 1 },
            new Order { OrderId = 3, OrderName = "Order #103 - iPad Pro", CustomerId = 1 },

            // Orders for Customer 2
            new Order { OrderId = 4, OrderName = "Order #201 - Book C#", CustomerId = 2 },
            new Order { OrderId = 5, OrderName = "Order #202 - Pen", CustomerId = 2 }
        );
    }
}

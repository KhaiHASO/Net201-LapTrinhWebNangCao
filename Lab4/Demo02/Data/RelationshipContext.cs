using Demo02.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo02.Data
{
    public class RelationshipContext : DbContext
    {
        public RelationshipContext(DbContextOptions<RelationshipContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed Data (Dữ liệu mẫu)
            
            // 1. Seed Departments
            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentID = 1, DepartmentName = "IT Software" },
                new Department { DepartmentID = 2, DepartmentName = "HR Admin" }
            );

            // 2. Seed Employees
            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeID = 1, EmployeeName = "Nguyen Van A", DepartmentID = 1 },
                new Employee { EmployeeID = 2, EmployeeName = "Tran Thi B", DepartmentID = 1 },
                new Employee { EmployeeID = 3, EmployeeName = "Le Van C", DepartmentID = 1 },
                new Employee { EmployeeID = 4, EmployeeName = "Pham Thi D", DepartmentID = 2 },
                new Employee { EmployeeID = 5, EmployeeName = "Hoang Van E", DepartmentID = 2 }
            );

            // 3. Seed Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerID = 1, CustomerName = "Cong ty Alpha", PhoneNo = "0901234567" },
                new Customer { CustomerID = 2, CustomerName = "Nha hang Beta", PhoneNo = "0987654321" },
                new Customer { CustomerID = 3, CustomerName = "Khach san Gamma", PhoneNo = "0912345678" }
            );
        }
    }
}

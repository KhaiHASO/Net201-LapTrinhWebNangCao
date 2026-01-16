using Microsoft.EntityFrameworkCore;
using Demo01.Models;

namespace Demo01.Data
{
    /// <summary>
    /// ApplicationDbContext - Lớp quản lý kết nối và cấu hình Database
    /// Minh họa: Fluent API trong OnModelCreating
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet - Đại diện cho các bảng trong database
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeAddress> EmployeeAddresses { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<EmployeesInProject> EmployeesInProjects { get; set; }

        /// <summary>
        /// OnModelCreating - Cấu hình các quan hệ bằng Fluent API
        /// Đây là nơi thể hiện sức mạnh của Fluent API so với Data Annotations
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ========================================
            // 1. CÁC CẤU HÌNH COMPOSITE KEY
            // ========================================

            // Cấu hình Composite Primary Key cho EmployeesInProject
            // Không thể làm được bằng Data Annotations!
            modelBuilder.Entity<EmployeesInProject>()
                .HasKey(ep => new { ep.EmployeeId, ep.ProjectId });

            // ========================================
            // 2. QUAN HỆ ONE-TO-MANY (1-N)
            // ========================================

            // Department (1) - Employee (N)
            // Một phòng ban có nhiều nhân viên
            modelBuilder.Entity<Department>()
                .HasMany(d => d.Employees)           // Department có nhiều Employees
                .WithOne(e => e.Department)          // Mỗi Employee thuộc một Department
                .HasForeignKey(e => e.DepartmentId)  // Foreign Key
                .OnDelete(DeleteBehavior.SetNull);   // Khi xóa Department, set DepartmentId = NULL

            // ========================================
            // 3. QUAN HỆ ONE-TO-ONE (1-1)
            // ========================================

            // Employee (1) - EmployeeAddress (1)
            // Một nhân viên có một địa chỉ duy nhất
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.EmployeeAddress)      // Employee có một EmployeeAddress
                .WithOne(ea => ea.Employee)          // EmployeeAddress thuộc về một Employee
                .HasForeignKey<EmployeeAddress>(ea => ea.EmployeeId) // Foreign Key
                .OnDelete(DeleteBehavior.Cascade);   // Khi xóa Employee, xóa luôn Address

            // ========================================
            // 4. QUAN HỆ MANY-TO-MANY (N-N)
            // ========================================

            // Employee (N) - Project (N) thông qua EmployeesInProject
            
            // Cấu hình phía Employee
            modelBuilder.Entity<EmployeesInProject>()
                .HasOne(ep => ep.Employee)           // EmployeesInProject có một Employee
                .WithMany(e => e.EmployeesInProjects) // Employee có nhiều EmployeesInProjects
                .HasForeignKey(ep => ep.EmployeeId)  // Foreign Key
                .OnDelete(DeleteBehavior.Cascade);   // Xóa Employee thì xóa luôn các bản ghi liên quan

            // Cấu hình phía Project
            modelBuilder.Entity<EmployeesInProject>()
                .HasOne(ep => ep.Project)            // EmployeesInProject có một Project
                .WithMany(p => p.EmployeesInProjects) // Project có nhiều EmployeesInProjects
                .HasForeignKey(ep => ep.ProjectId)   // Foreign Key
                .OnDelete(DeleteBehavior.Cascade);   // Xóa Project thì xóa luôn các bản ghi liên quan

            // ========================================
            // 5. CẤU HÌNH BỔ SUNG (OPTIONAL)
            // ========================================

            // Cấu hình Index cho tìm kiếm nhanh
            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.Email)
                .IsUnique(); // Email phải là duy nhất

            // Cấu hình giá trị mặc định
            modelBuilder.Entity<Department>()
                .Property(d => d.EstablishedDate)
                .HasDefaultValueSql("GETDATE()");

            // ========================================
            // 6. SEED DATA (DỮ LIỆU MẪU)
            // ========================================

            // Thêm dữ liệu mẫu cho Department
            modelBuilder.Entity<Department>().HasData(
                new Department
                {
                    DepartmentId = 1,
                    Name = "Phòng Công Nghệ",
                    Description = "Phòng phát triển phần mềm và công nghệ thông tin",
                    EstablishedDate = new DateTime(2020, 1, 1)
                },
                new Department
                {
                    DepartmentId = 2,
                    Name = "Phòng Nhân Sự",
                    Description = "Phòng quản lý nhân sự và tuyển dụng",
                    EstablishedDate = new DateTime(2020, 1, 1)
                },
                new Department
                {
                    DepartmentId = 3,
                    Name = "Phòng Kinh Doanh",
                    Description = "Phòng bán hàng và chăm sóc khách hàng",
                    EstablishedDate = new DateTime(2020, 6, 1)
                }
            );

            // Thêm dữ liệu mẫu cho Employee
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    EmployeeId = 1,
                    Name = "Nguyễn Văn An",
                    Email = "an.nguyen@company.com",
                    PhoneNumber = "0901234567",
                    DateOfBirth = new DateTime(1990, 5, 15),
                    Salary = 25000000,
                    DepartmentId = 1
                },
                new Employee
                {
                    EmployeeId = 2,
                    Name = "Trần Thị Bình",
                    Email = "binh.tran@company.com",
                    PhoneNumber = "0907654321",
                    DateOfBirth = new DateTime(1992, 8, 20),
                    Salary = 22000000,
                    DepartmentId = 1
                },
                new Employee
                {
                    EmployeeId = 3,
                    Name = "Lê Văn Cường",
                    Email = "cuong.le@company.com",
                    PhoneNumber = "0912345678",
                    DateOfBirth = new DateTime(1988, 3, 10),
                    Salary = 18000000,
                    DepartmentId = 2
                }
            );

            // Thêm dữ liệu mẫu cho Project
            modelBuilder.Entity<Project>().HasData(
                new Project
                {
                    ProjectId = 1,
                    Name = "Website Thương Mại Điện Tử",
                    Description = "Xây dựng website bán hàng trực tuyến",
                    StartDate = new DateTime(2024, 1, 1),
                    EndDate = new DateTime(2024, 6, 30),
                    Budget = 500000000,
                    Status = "In Progress"
                },
                new Project
                {
                    ProjectId = 2,
                    Name = "Ứng Dụng Quản Lý Nhân Sự",
                    Description = "Phát triển hệ thống quản lý nhân sự nội bộ",
                    StartDate = new DateTime(2024, 3, 1),
                    EndDate = new DateTime(2024, 9, 30),
                    Budget = 300000000,
                    Status = "Planning"
                }
            );

            // Thêm dữ liệu mẫu cho EmployeeAddress
            modelBuilder.Entity<EmployeeAddress>().HasData(
                new EmployeeAddress
                {
                    EmployeeId = 1,
                    Street = "123 Đường Lê Lợi",
                    City = "Hồ Chí Minh",
                    Province = "Hồ Chí Minh",
                    PostalCode = "700000",
                    Country = "Việt Nam"
                },
                new EmployeeAddress
                {
                    EmployeeId = 2,
                    Street = "456 Đường Nguyễn Huệ",
                    City = "Hà Nội",
                    Province = "Hà Nội",
                    PostalCode = "100000",
                    Country = "Việt Nam"
                }
            );

            // Thêm dữ liệu mẫu cho EmployeesInProject
            modelBuilder.Entity<EmployeesInProject>().HasData(
                new EmployeesInProject
                {
                    EmployeeId = 1,
                    ProjectId = 1,
                    JoinedDate = new DateTime(2024, 1, 1),
                    Role = "Team Lead",
                    WorkloadPercentage = 80
                },
                new EmployeesInProject
                {
                    EmployeeId = 2,
                    ProjectId = 1,
                    JoinedDate = new DateTime(2024, 1, 15),
                    Role = "Developer",
                    WorkloadPercentage = 100
                },
                new EmployeesInProject
                {
                    EmployeeId = 1,
                    ProjectId = 2,
                    JoinedDate = new DateTime(2024, 3, 1),
                    Role = "Consultant",
                    WorkloadPercentage = 20
                }
            );
        }
    }
}

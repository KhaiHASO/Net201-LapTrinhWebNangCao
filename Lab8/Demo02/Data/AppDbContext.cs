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

            // Seed Data for Demonstration

            // 1. Create a Branch
            modelBuilder.Entity<Branch>().HasData(
                new Branch { BranchId = 1, BranchName = "CNTT" }
            );

            // 2. Create an Address
            modelBuilder.Entity<Address>().HasData(
                new Address { AddressId = 1, Street = "Hanoi" }
            );

            // 3. Create Students
            modelBuilder.Entity<Student>().HasData(
                // Student 1: Has both Branch and Address
                // This will demonstrate a standard record.
                new Student 
                { 
                    StudentId = 1, 
                    Name = "Nguyen Van A", 
                    BranchId = 1, // Must have Branch
                    AddressId = 1 // Has Address
                },

                // Student 2: Has Branch but NO Address
                // This demonstrates LEFT JOIN. Even though Address is joined, 
                // this record appears with Address as null.
                new Student 
                { 
                    StudentId = 2, 
                    Name = "Tran Van B", 
                    BranchId = 1, // Must have Branch
                    AddressId = null // NO Address -> Left Join Result
                }
            );
        }
    }
}

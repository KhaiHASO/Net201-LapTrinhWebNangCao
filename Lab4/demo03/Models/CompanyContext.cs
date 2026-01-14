using Microsoft.EntityFrameworkCore;

namespace demo03.Models;

public class CompanyContext : DbContext
{
    public CompanyContext(DbContextOptions<CompanyContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; } = null!;
}


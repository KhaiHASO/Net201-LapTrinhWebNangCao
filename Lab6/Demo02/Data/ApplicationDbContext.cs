using Microsoft.EntityFrameworkCore;

namespace Demo02.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Add DbSets here if needed, for now it's empty just to satisfy the "Database" requirement
        // public DbSet<Something> Somethings { get; set; }
    }
}

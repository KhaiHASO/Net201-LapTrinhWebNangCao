using Microsoft.EntityFrameworkCore;

namespace demo01.Models
{
    public class CompanyContext : DbContext
    {
        public CompanyContext(DbContextOptions<CompanyContext> options) : base(options)
        {
        }

        public DbSet<Information> Informations { get; set; }
    }
}

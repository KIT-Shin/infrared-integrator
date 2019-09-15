using Microsoft.EntityFrameworkCore;

namespace irid.Models
{
    public class IridDbContext : DbContext
    {
        public IridDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Device> Devices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlite("Data Source=database.db");
    }
}
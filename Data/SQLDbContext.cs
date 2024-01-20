using Guest.Entities;
using Microsoft.EntityFrameworkCore;

namespace Guest.Data
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Guests>()
                .Property(c => c.Title)
                .HasConversion<string>();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Guests> Guests { get; set; }
    }
}
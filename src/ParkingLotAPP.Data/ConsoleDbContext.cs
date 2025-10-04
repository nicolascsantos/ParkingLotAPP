using ParkingLotAPP.Data.Configurations;
using ParkingLotAPP.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ParkingLotAPP.Data
{
    public class ConsoleDbContext : DbContext
    {
        public ConsoleDbContext(DbContextOptions options) : base(options) { }  
        

        public DbSet<Car> Cars { get; set; }

        public DbSet<CarBrand> CarBrands { get; set; }

        public DbSet<CarColor> CarColors { get; set; }

        public DbSet<Driver> Drivers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CarConfiguration());
            modelBuilder.ApplyConfiguration(new CarBrandConfiguration());
            modelBuilder.ApplyConfiguration(new CarColorConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Server=nicolaspc;Database=Console;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True");
        }
    }
}

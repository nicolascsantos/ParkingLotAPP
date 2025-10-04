using ParkingLotAPP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ParkingLotAPP.Data.Configurations
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.CarBrand)
                .WithMany(x => x.Cars)
                .HasForeignKey(x => x.CarBrandId)
                .IsRequired();

            builder.HasOne(x => x.CarColor)
                .WithMany(x => x.Cars)
                .HasForeignKey(x => x.CarColorId)
                .IsRequired();

            builder.HasOne(x => x.Driver)
                .WithMany(d => d.Cars)
                .HasForeignKey(j => j.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .OwnsOne(x => x.Plate)
                .Property(x => x.Number)
                .HasColumnName("Plate")
                .IsRequired(true);
        }
    }
}

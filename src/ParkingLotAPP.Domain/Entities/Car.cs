using ParkingLotAPP.Domain.SeedWork;
using ParkingLotAPP.Domain.Validation;
using ParkingLotAPP.Domain.ValueObjects;
using System.Text.Json.Serialization;

namespace ParkingLotAPP.Domain.Entities
{
    public class Car : Entity
    {
        public string Name { get; set; }

        public Plate Plate { get; set; }

        public int Year { get; set; }

        public int ModelYear { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid CarBrandId { get; set; }

        public Guid CarColorId { get; set; }

        public Guid DriverId { get; set; }

        [JsonIgnore]
        public CarBrand CarBrand { get; set; } = null!;

        [JsonIgnore]
        public CarColor CarColor { get; set; } = null!;

        [JsonIgnore]
        public Driver Driver { get; set; }

        public Car(
            string name,
            int year,
            int modelYear,
            Guid carBrandId,
            Guid carColorId,
            string plate
        )
        {
            Name = name;
            Year = year;
            ModelYear = modelYear;
            CreatedAt = DateTime.Now;
            CarBrandId = carBrandId;
            CarColorId = carColorId;
            Plate = Plate.Create(plate);
            Validate();
        }

        public Car()
        {
            
        }
        public void Validate()
        {
            DomainValidation.NotNull(Name, nameof(Name));
            DomainValidation.NotNullOrEmpty(Name, nameof(Name));
            DomainValidation.MinLength(Name, nameof(Name), 3);
            DomainValidation.NotNull(Year, nameof(Year));
            DomainValidation.NotNull(ModelYear, nameof(ModelYear));
            DomainValidation.NotNull(CarBrandId, nameof(CarBrandId));
            DomainValidation.NotNull(CarColorId, nameof(CarColorId));
        }

        public void Update(
            string name, 
            int year,
            int modelYear,
            Guid? carBrandId,
            Guid? carColorId,
            string plate
        )
        {
            Name = name;
            Year = year;
            ModelYear = modelYear;
            CarBrandId = carBrandId ?? CarBrandId;
            CarColorId = carColorId ?? CarColorId;
            Plate = Plate.Create(plate);
        }
    }
}

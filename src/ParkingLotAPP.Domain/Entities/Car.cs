using ParkingLotAPP.Domain.SeedWork;
using ParkingLotAPP.Domain.Validation;
using ParkingLotAPP.Domain.ValueObjects;
using System.Text.Json.Serialization;

namespace ParkingLotAPP.Domain.Entities
{
    public class Car : Entity
    {
        public string Name { get; private set; }

        public Plate Plate { get; private set; }

        public int Year { get; private set; }

        public int ModelYear { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public Guid CarBrandId { get; private set; }

        public Guid CarColorId { get; private set; }

        public Guid DriverId { get; private set; }

        [JsonIgnore]
        public CarBrand CarBrand { get; private set; } = null!;

        [JsonIgnore]
        public CarColor CarColor { get; private set; } = null!;

        [JsonIgnore]
        public Driver Driver { get; private set; } = null!;

        public Car(
            string name,
            int year,
            int modelYear,
            Guid carBrandId,
            Guid carColorId,
            string plate
        ) : base()
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

        private Car() { }

        public override void Validate()
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
            Validate();
        }

        public void AssignDriver(Driver driver)
        {
            Driver = driver;
            DriverId = driver.Id;
        }
    }
}

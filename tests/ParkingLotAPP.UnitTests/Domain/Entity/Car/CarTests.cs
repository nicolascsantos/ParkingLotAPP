using ParkingLotAPP.Domain.Exceptions;
using FluentAssertions;
using DomainEntity = ParkingLotAPP.Domain.Entities;

namespace ParkingLotAPP.UnitTests.Domain.Entity.Car
{
    [Collection(nameof(CarTestFixture))]
    public class CarTests
    {
        private readonly CarTestFixture _fixture;

        public CarTests(CarTestFixture fixture)
            => _fixture = fixture;

        [Fact(DisplayName = nameof(Instantiate))]
        [Trait("Domain", "Car - Entities")]
        public void Instantiate()
        {
            var name = _fixture.GetValidCarName();
            var year = _fixture.GetValidYear();
            var modelYear = _fixture.GetValidModelYear();
            var carBrandId = Guid.NewGuid();
            var carColorId = Guid.NewGuid();
            var driverId = Guid.NewGuid();
            var plate = _fixture.GetValidPlate();
            var dateTimeBefore = DateTime.Now;

            var car = new DomainEntity.Car(name, year, modelYear, carBrandId, carColorId, plate, driverId);

            var dateTimeAfter = DateTime.Now.AddSeconds(1);

            car.Should().NotBeNull();
            car.Name.Should().Be(name);
            car.Year.Should().Be(year);
            car.ModelYear.Should().Be(modelYear);
            car.CarBrandId.Should().Be(carBrandId);
            car.CarColorId.Should().Be(carColorId);
            car.Plate.Should().NotBeNull();
            car.Plate.Number.Should().Be(plate);
            car.Id.Should().NotBeEmpty();
            (car.CreatedAt >= dateTimeBefore).Should().BeTrue();
            (car.CreatedAt <= dateTimeAfter).Should().BeTrue();
        }

        [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsNullOrEmpty))]
        [Trait("Domain", "Car - Entities")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void InstantiateErrorWhenNameIsNullOrEmpty(string? name)
        {
            Action action = () => new DomainEntity.Car(
                name!, _fixture.GetValidYear(), _fixture.GetValidModelYear(),
                Guid.NewGuid(), Guid.NewGuid(), _fixture.GetValidPlate(), Guid.NewGuid()
            );

            action.Should().Throw<EntityValidationException>();
        }

        [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsTooShort))]
        [Trait("Domain", "Car - Entities")]
        [InlineData("AB")]
        [InlineData("X")]
        public void InstantiateErrorWhenNameIsTooShort(string name)
        {
            Action action = () => new DomainEntity.Car(
                name, _fixture.GetValidYear(), _fixture.GetValidModelYear(),
                Guid.NewGuid(), Guid.NewGuid(), _fixture.GetValidPlate(), Guid.NewGuid()
            );

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Name should be at least 3 characters long.");
        }

        [Fact(DisplayName = nameof(InstantiateErrorWhenPlateIsInvalid))]
        [Trait("Domain", "Car - Entities")]
        public void InstantiateErrorWhenPlateIsInvalid()
        {
            Action action = () => new DomainEntity.Car(
                _fixture.GetValidCarName(), _fixture.GetValidYear(), _fixture.GetValidModelYear(),
                Guid.NewGuid(), Guid.NewGuid(), "INVALID", Guid.NewGuid()
            );

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact(DisplayName = nameof(Update))]
        [Trait("Domain", "Car - Entities")]
        public void Update()
        {
            var car = _fixture.GetValidCar();
            var newName = _fixture.GetValidCarName();
            var newYear = 2025;
            var newModelYear = 2026;
            var newPlate = "ABC1234";
            var newBrandId = Guid.NewGuid();
            var newColorId = Guid.NewGuid();

            car.Update(newName, newYear, newModelYear, newBrandId, newColorId, newPlate);

            car.Name.Should().Be(newName);
            car.Year.Should().Be(newYear);
            car.ModelYear.Should().Be(newModelYear);
            car.CarBrandId.Should().Be(newBrandId);
            car.CarColorId.Should().Be(newColorId);
            car.Plate.Number.Should().Be(newPlate);
        }

        [Fact(DisplayName = nameof(UpdateKeepsOriginalIdsWhenNull))]
        [Trait("Domain", "Car - Entities")]
        public void UpdateKeepsOriginalIdsWhenNull()
        {
            var car = _fixture.GetValidCar();
            var originalBrandId = car.CarBrandId;
            var originalColorId = car.CarColorId;

            car.Update(_fixture.GetValidCarName(), 2025, 2026, null, null, "ABC1234");

            car.CarBrandId.Should().Be(originalBrandId);
            car.CarColorId.Should().Be(originalColorId);
        }

        [Fact(DisplayName = nameof(AssignDriver))]
        [Trait("Domain", "Car - Entities")]
        public void AssignDriver()
        {
            var car = _fixture.GetValidCar();
            var driver = new DomainEntity.Driver("John Doe", "12345678900", "C001", "john@test.com", "11999999999");

            car.AssignDriver(driver);

            car.Driver.Should().Be(driver);
            car.DriverId.Should().Be(driver.Id);
        }
    }
}

using ParkingLotAPP.Domain.Exceptions;
using FluentAssertions;
using DomainEntity = ParkingLotAPP.Domain.Entities;

namespace ParkingLotAPP.UnitTests.Domain.Entity.Driver
{
    [Collection(nameof(DriverTestFixture))]
    public class DriverTests
    {
        private readonly DriverTestFixture _fixture;

        public DriverTests(DriverTestFixture fixture)
            => _fixture = fixture;

        [Fact(DisplayName = nameof(Instantiate))]
        [Trait("Domain", "Driver - Entities")]
        public void Instantiate()
        {
            var name = _fixture.GetValidDriverName();
            var document = _fixture.GetValidDocument();
            var contractNumber = _fixture.GetValidContractNumber();
            var email = _fixture.GetValidEmail();
            var phoneNumber = _fixture.GetValidPhoneNumber();

            var driver = new DomainEntity.Driver(name, document, contractNumber, email, phoneNumber);

            driver.Should().NotBeNull();
            driver.Name.Should().Be(name);
            driver.Document.Should().Be(document);
            driver.ContractNumber.Should().Be(contractNumber);
            driver.Email.Should().Be(email);
            driver.PhoneNumber.Should().Be(phoneNumber);
            driver.IsActive.Should().BeTrue();
            driver.Id.Should().NotBeEmpty();
            driver.Cars.Should().NotBeNull();
            driver.Cars.Should().BeEmpty();
        }

        [Fact(DisplayName = nameof(InstantiateWithIsActiveFalse))]
        [Trait("Domain", "Driver - Entities")]
        public void InstantiateWithIsActiveFalse()
        {
            var driver = new DomainEntity.Driver(
                _fixture.GetValidDriverName(),
                _fixture.GetValidDocument(),
                _fixture.GetValidContractNumber(),
                _fixture.GetValidEmail(),
                _fixture.GetValidPhoneNumber(),
                isActive: false
            );

            driver.IsActive.Should().BeFalse();
        }

        [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsNull))]
        [Trait("Domain", "Driver - Entities")]
        [InlineData(null)]
        public void InstantiateErrorWhenNameIsNull(string? name)
        {
            Action action = () => new DomainEntity.Driver(
                name!, _fixture.GetValidDocument(), _fixture.GetValidContractNumber(),
                _fixture.GetValidEmail(), _fixture.GetValidPhoneNumber()
            );

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Name should not be null.");
        }

        [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsTooShort))]
        [Trait("Domain", "Driver - Entities")]
        [InlineData("AB")]
        [InlineData("X")]
        public void InstantiateErrorWhenNameIsTooShort(string name)
        {
            Action action = () => new DomainEntity.Driver(
                name, _fixture.GetValidDocument(), _fixture.GetValidContractNumber(),
                _fixture.GetValidEmail(), _fixture.GetValidPhoneNumber()
            );

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Name should be at least 3 characters long.");
        }

        [Fact(DisplayName = nameof(InstantiateErrorWhenNameIsTooLong))]
        [Trait("Domain", "Driver - Entities")]
        public void InstantiateErrorWhenNameIsTooLong()
        {
            var longName = new string('A', 101);

            Action action = () => new DomainEntity.Driver(
                longName, _fixture.GetValidDocument(), _fixture.GetValidContractNumber(),
                _fixture.GetValidEmail(), _fixture.GetValidPhoneNumber()
            );

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Name should be less or equal 100 characters long.");
        }

        [Fact(DisplayName = nameof(InstantiateErrorWhenDocumentIsNull))]
        [Trait("Domain", "Driver - Entities")]
        public void InstantiateErrorWhenDocumentIsNull()
        {
            Action action = () => new DomainEntity.Driver(
                _fixture.GetValidDriverName(), null!, _fixture.GetValidContractNumber(),
                _fixture.GetValidEmail(), _fixture.GetValidPhoneNumber()
            );

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Document should not be null.");
        }

        [Fact(DisplayName = nameof(InstantiateErrorWhenContractNumberIsNull))]
        [Trait("Domain", "Driver - Entities")]
        public void InstantiateErrorWhenContractNumberIsNull()
        {
            Action action = () => new DomainEntity.Driver(
                _fixture.GetValidDriverName(), _fixture.GetValidDocument(), null!,
                _fixture.GetValidEmail(), _fixture.GetValidPhoneNumber()
            );

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("ContractNumber should not be null.");
        }

        [Fact(DisplayName = nameof(Activate))]
        [Trait("Domain", "Driver - Entities")]
        public void Activate()
        {
            var driver = new DomainEntity.Driver(
                _fixture.GetValidDriverName(), _fixture.GetValidDocument(),
                _fixture.GetValidContractNumber(), _fixture.GetValidEmail(),
                _fixture.GetValidPhoneNumber(), isActive: false
            );

            driver.Activate();

            driver.IsActive.Should().BeTrue();
        }

        [Fact(DisplayName = nameof(Deactivate))]
        [Trait("Domain", "Driver - Entities")]
        public void Deactivate()
        {
            var driver = _fixture.GetValidDriver();

            driver.Deactivate();

            driver.IsActive.Should().BeFalse();
        }

        [Fact(DisplayName = nameof(AddCar))]
        [Trait("Domain", "Driver - Entities")]
        public void AddCar()
        {
            var driver = _fixture.GetValidDriver();
            var car = new DomainEntity.Car("Civic", 2024, 2025, Guid.NewGuid(), Guid.NewGuid(), "ABC1234");

            driver.AddCar(car);

            driver.Cars.Should().HaveCount(1);
            driver.Cars.Should().Contain(car);
            car.DriverId.Should().Be(driver.Id);
        }

        [Fact(DisplayName = nameof(AddMultipleCars))]
        [Trait("Domain", "Driver - Entities")]
        public void AddMultipleCars()
        {
            var driver = _fixture.GetValidDriver();
            var car1 = new DomainEntity.Car("Civic", 2024, 2025, Guid.NewGuid(), Guid.NewGuid(), "ABC1234");
            var car2 = new DomainEntity.Car("Corolla", 2023, 2024, Guid.NewGuid(), Guid.NewGuid(), "XYZ5678");

            driver.AddCar(car1);
            driver.AddCar(car2);

            driver.Cars.Should().HaveCount(2);
        }
    }
}

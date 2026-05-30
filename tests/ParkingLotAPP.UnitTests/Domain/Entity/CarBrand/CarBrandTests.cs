using ParkingLotAPP.Domain.Exceptions;
using FluentAssertions;
using DomainEntity = ParkingLotAPP.Domain.Entities;

namespace ParkingLotAPP.UnitTests.Domain.Entity.CarBrand
{
    [Collection(nameof(CarBrandTestFixture))]
    public class CarBrandTests
    {
        private readonly CarBrandTestFixture _fixture;

        public CarBrandTests(CarBrandTestFixture fixture)
            => _fixture = fixture;

        [Fact(DisplayName = nameof(Instantiate))]
        [Trait("Domain", "CarBrand - Entities")]
        public void Instantiate()
        {
            var validName = _fixture.GetValidCarBrandName();
            var dateTimeBefore = DateTime.Now;

            var carBrand = new DomainEntity.CarBrand(validName);

            var dateTimeAfter = DateTime.Now.AddSeconds(1);

            carBrand.Should().NotBeNull();
            carBrand.Name.Should().Be(validName);
            carBrand.Id.Should().NotBeEmpty();
            (carBrand.CreatedAt >= dateTimeBefore).Should().BeTrue();
            (carBrand.CreatedAt <= dateTimeAfter).Should().BeTrue();
        }

        [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsNull))]
        [Trait("Domain", "CarBrand - Entities")]
        [InlineData(null)]
        public void InstantiateErrorWhenNameIsNull(string? name)
        {
            Action action = () => new DomainEntity.CarBrand(name!);

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Name should not be null.");
        }

        [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsEmpty))]
        [Trait("Domain", "CarBrand - Entities")]
        [InlineData("")]
        [InlineData("  ")]
        public void InstantiateErrorWhenNameIsEmpty(string? name)
        {
            Action action = () => new DomainEntity.CarBrand(name!);

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Name should not be empty or null.");
        }

        [Fact(DisplayName = nameof(Update))]
        [Trait("Domain", "CarBrand - Entities")]
        public void Update()
        {
            var carBrand = _fixture.GetValidCarBrand();
            var newName = _fixture.GetValidCarBrandName();

            carBrand.Update(newName);

            carBrand.Name.Should().Be(newName);
        }

        [Fact(DisplayName = nameof(UpdateErrorWhenNameIsNull))]
        [Trait("Domain", "CarBrand - Entities")]
        public void UpdateErrorWhenNameIsNull()
        {
            var carBrand = _fixture.GetValidCarBrand();

            Action action = () => carBrand.Update(null!);

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Name should not be null.");
        }

        [Fact(DisplayName = nameof(CarsCollectionStartsEmpty))]
        [Trait("Domain", "CarBrand - Entities")]
        public void CarsCollectionStartsEmpty()
        {
            var carBrand = _fixture.GetValidCarBrand();

            carBrand.Cars.Should().NotBeNull();
            carBrand.Cars.Should().BeEmpty();
        }
    }
}

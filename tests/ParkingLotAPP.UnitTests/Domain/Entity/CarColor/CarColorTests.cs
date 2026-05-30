using ParkingLotAPP.Domain.Exceptions;
using FluentAssertions;
using DomainEntity = ParkingLotAPP.Domain.Entities;


namespace ParkingLotAPP.UnitTests.Domain.Entity.CarColor
{
    [Collection(nameof(CarColorTestFixture))]
    public class CarColorTests
    {
        private readonly CarColorTestFixture _fixture;

        public CarColorTests(CarColorTestFixture fixture)
            => _fixture = fixture;


        [Fact(DisplayName = nameof(Instantiate))]
        [Trait("Domain", "CarColor - Entities")]
        public void Instantiate()
        {
            var validCarColor = _fixture.GetValidCarColor();

            var dateTimeBefore = DateTime.Now;

            var carColor = new DomainEntity.CarColor(
                validCarColor.Name,
                validCarColor.Hex
            );

            var dateTimeAfter = DateTime.Now.AddSeconds(1);

            carColor.Should().NotBeNull();
            carColor.Name.Should().Be(validCarColor.Name);
            carColor.Hex.Should().Be(validCarColor.Hex);
            (carColor.CreatedAt >= dateTimeBefore).Should().BeTrue();
            (carColor.CreatedAt <= dateTimeAfter).Should().BeTrue();
        }

        [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsEmpty))]
        [Trait("Domain", "CarColor - Entities")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void InstantiateErrorWhenNameIsEmpty(string? name)
        {
            var carColor = _fixture.GetValidCarColor();

            Action action = () => new DomainEntity.CarColor(name!, carColor.Hex);

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Name should not be empty or null.");
        }

        [Fact(DisplayName = nameof(InstantiateErrorWhenHexIsNull))]
        [Trait("Domain", "CarColor - Entities")]
        public void InstantiateErrorWhenHexIsNull()
        {
            Action action = () => new DomainEntity.CarColor("Red", null!);

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Hex should not be null.");
        }

        [Fact(DisplayName = nameof(Update))]
        [Trait("Domain", "CarColor - Entities")]
        public void Update()
        {
            var carColor = _fixture.GetValidCarColor();

            carColor.Update("Blue", "#0000FF");

            carColor.Name.Should().Be("Blue");
            carColor.Hex.Should().Be("#0000FF");
        }

        [Fact(DisplayName = nameof(UpdateErrorWhenNameIsEmpty))]
        [Trait("Domain", "CarColor - Entities")]
        public void UpdateErrorWhenNameIsEmpty()
        {
            var carColor = _fixture.GetValidCarColor();

            Action action = () => carColor.Update("", "#0000FF");

            action.Should().Throw<EntityValidationException>();
        }

        [Fact(DisplayName = nameof(CarsCollectionStartsEmpty))]
        [Trait("Domain", "CarColor - Entities")]
        public void CarsCollectionStartsEmpty()
        {
            var carColor = _fixture.GetValidCarColor();

            carColor.Cars.Should().NotBeNull();
            carColor.Cars.Should().BeEmpty();
        }
    }
}

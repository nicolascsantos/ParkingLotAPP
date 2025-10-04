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


    }
}

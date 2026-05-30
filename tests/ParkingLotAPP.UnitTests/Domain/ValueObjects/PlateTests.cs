using FluentAssertions;
using ParkingLotAPP.Domain.ValueObjects;

namespace ParkingLotAPP.UnitTests.Domain.ValueObjects
{
    public class PlateTests
    {
        [Theory(DisplayName = nameof(CreateWithValidOldFormat))]
        [Trait("Domain", "Plate - ValueObjects")]
        [InlineData("ABC1234")]
        [InlineData("abc1234")]
        [InlineData("ABC-1234")]
        public void CreateWithValidOldFormat(string number)
        {
            var plate = Plate.Create(number);

            plate.Should().NotBeNull();
            plate.Number.Should().Be(number.Trim().ToUpper());
        }

        [Theory(DisplayName = nameof(CreateWithValidMercosulFormat))]
        [Trait("Domain", "Plate - ValueObjects")]
        [InlineData("ABC1D23")]
        [InlineData("abc1d23")]
        [InlineData("ABC-1D23")]
        public void CreateWithValidMercosulFormat(string number)
        {
            var plate = Plate.Create(number);

            plate.Should().NotBeNull();
            plate.Number.Should().Be(number.Trim().ToUpper());
        }

        [Theory(DisplayName = nameof(CreateErrorWhenNullOrEmpty))]
        [Trait("Domain", "Plate - ValueObjects")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void CreateErrorWhenNullOrEmpty(string? number)
        {
            Action action = () => Plate.Create(number!);

            action.Should().Throw<ArgumentNullException>();
        }

        [Theory(DisplayName = nameof(CreateErrorWhenInvalidFormat))]
        [Trait("Domain", "Plate - ValueObjects")]
        [InlineData("12345")]
        [InlineData("ABCDEFG")]
        [InlineData("123ABC")]
        [InlineData("A1B2C3D")]
        public void CreateErrorWhenInvalidFormat(string number)
        {
            Action action = () => Plate.Create(number);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact(DisplayName = nameof(ConvertsToUpperCase))]
        [Trait("Domain", "Plate - ValueObjects")]
        public void ConvertsToUpperCase()
        {
            var plate = Plate.Create("abc1234");

            plate.Number.Should().Be("ABC1234");
        }

        [Fact(DisplayName = nameof(EqualityWhenSameNumber))]
        [Trait("Domain", "Plate - ValueObjects")]
        public void EqualityWhenSameNumber()
        {
            var plate1 = Plate.Create("ABC1234");
            var plate2 = Plate.Create("ABC1234");

            plate1.Equals(plate2).Should().BeTrue();
        }

        [Fact(DisplayName = nameof(InequalityWhenDifferentNumber))]
        [Trait("Domain", "Plate - ValueObjects")]
        public void InequalityWhenDifferentNumber()
        {
            var plate1 = Plate.Create("ABC1234");
            var plate2 = Plate.Create("XYZ5678");

            plate1.Equals(plate2).Should().BeFalse();
        }

        [Fact(DisplayName = nameof(EqualsWithNullReturnsFalse))]
        [Trait("Domain", "Plate - ValueObjects")]
        public void EqualsWithNullReturnsFalse()
        {
            var plate = Plate.Create("ABC1234");

            plate.Equals(null).Should().BeFalse();
        }

        [Fact(DisplayName = nameof(EqualsWithObjectOverride))]
        [Trait("Domain", "Plate - ValueObjects")]
        public void EqualsWithObjectOverride()
        {
            var plate1 = Plate.Create("ABC1234");
            object plate2 = Plate.Create("ABC1234");

            plate1.Equals(plate2).Should().BeTrue();
        }

        [Fact(DisplayName = nameof(GetHashCodeConsistency))]
        [Trait("Domain", "Plate - ValueObjects")]
        public void GetHashCodeConsistency()
        {
            var plate1 = Plate.Create("ABC1234");
            var plate2 = Plate.Create("ABC1234");

            plate1.GetHashCode().Should().Be(plate2.GetHashCode());
        }

        [Fact(DisplayName = nameof(ConstructorSetsUpperCase))]
        [Trait("Domain", "Plate - ValueObjects")]
        public void ConstructorSetsUpperCase()
        {
            var plate = new Plate("abc1234");

            plate.Number.Should().Be("ABC1234");
        }
    }
}

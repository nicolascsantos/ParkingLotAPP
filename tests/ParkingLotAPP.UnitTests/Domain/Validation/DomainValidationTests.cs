using FluentAssertions;
using ParkingLotAPP.Domain.Exceptions;
using ParkingLotAPP.Domain.Validation;

namespace ParkingLotAPP.UnitTests.Domain.Validation
{
    public class DomainValidationTests
    {
        [Fact(DisplayName = nameof(NotNullOk))]
        [Trait("Domain", "DomainValidation")]
        public void NotNullOk()
        {
            var action = () => DomainValidation.NotNull("value", "Field");

            action.Should().NotThrow();
        }

        [Fact(DisplayName = nameof(NotNullThrowsWhenNull))]
        [Trait("Domain", "DomainValidation")]
        public void NotNullThrowsWhenNull()
        {
            Action action = () => DomainValidation.NotNull(null, "Field");

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Field should not be null.");
        }

        [Fact(DisplayName = nameof(NotNullOrEmptyOk))]
        [Trait("Domain", "DomainValidation")]
        public void NotNullOrEmptyOk()
        {
            var action = () => DomainValidation.NotNullOrEmpty("value", "Field");

            action.Should().NotThrow();
        }

        [Theory(DisplayName = nameof(NotNullOrEmptyThrowsWhenNullOrEmpty))]
        [Trait("Domain", "DomainValidation")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void NotNullOrEmptyThrowsWhenNullOrEmpty(string? value)
        {
            Action action = () => DomainValidation.NotNullOrEmpty(value, "Field");

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Field should not be empty or null.");
        }

        [Fact(DisplayName = nameof(MinLengthOk))]
        [Trait("Domain", "DomainValidation")]
        public void MinLengthOk()
        {
            var action = () => DomainValidation.MinLength("abcde", "Field", 3);

            action.Should().NotThrow();
        }

        [Fact(DisplayName = nameof(MinLengthThrowsWhenTooShort))]
        [Trait("Domain", "DomainValidation")]
        public void MinLengthThrowsWhenTooShort()
        {
            Action action = () => DomainValidation.MinLength("ab", "Field", 3);

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Field should be at least 3 characters long.");
        }

        [Fact(DisplayName = nameof(MaxLengthOk))]
        [Trait("Domain", "DomainValidation")]
        public void MaxLengthOk()
        {
            var action = () => DomainValidation.MaxLength("abc", "Field", 5);

            action.Should().NotThrow();
        }

        [Fact(DisplayName = nameof(MaxLengthThrowsWhenTooLong))]
        [Trait("Domain", "DomainValidation")]
        public void MaxLengthThrowsWhenTooLong()
        {
            Action action = () => DomainValidation.MaxLength("abcdef", "Field", 5);

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Field should be less or equal 5 characters long.");
        }
    }
}

using FluentAssertions;
using ParkingLotAPP.Domain.Entities;
using ParkingLotAPP.Domain.Services;

namespace ParkingLotAPP.UnitTests.Domain.Services
{
    public class DriverCarAssignmentServiceTests
    {
        private readonly DriverCarAssignmentService _service;

        public DriverCarAssignmentServiceTests()
        {
            _service = new DriverCarAssignmentService();
        }

        [Fact(DisplayName = nameof(AssignSingleCarToDriver))]
        [Trait("Domain", "DriverCarAssignmentService")]
        public void AssignSingleCarToDriver()
        {
            var driver = new Driver("John Doe", "12345678900", "C-0001", "john@test.com", "11999999999");

            var carsData = new[]
            {
                ("Civic", 2024, 2025, Guid.NewGuid(), Guid.NewGuid(), "ABC1234")
            };

            _service.AssignCarsToDriver(driver, carsData);

            driver.Cars.Should().HaveCount(1);
            driver.Cars.First().Name.Should().Be("Civic");
            driver.Cars.First().DriverId.Should().Be(driver.Id);
        }

        [Fact(DisplayName = nameof(AssignMultipleCarsToDriver))]
        [Trait("Domain", "DriverCarAssignmentService")]
        public void AssignMultipleCarsToDriver()
        {
            var driver = new Driver("Jane Doe", "98765432100", "C-0002", "jane@test.com", "11888888888");

            var carsData = new[]
            {
                ("Civic", 2024, 2025, Guid.NewGuid(), Guid.NewGuid(), "ABC1234"),
                ("Corolla", 2023, 2024, Guid.NewGuid(), Guid.NewGuid(), "XYZ5678"),
                ("Onix", 2022, 2023, Guid.NewGuid(), Guid.NewGuid(), "DEF3456")
            };

            _service.AssignCarsToDriver(driver, carsData);

            driver.Cars.Should().HaveCount(3);
            driver.Cars.Select(c => c.Name).Should().Contain(new[] { "Civic", "Corolla", "Onix" });
            driver.Cars.All(c => c.DriverId == driver.Id).Should().BeTrue();
        }

        [Fact(DisplayName = nameof(AssignNoCarsToDriver))]
        [Trait("Domain", "DriverCarAssignmentService")]
        public void AssignNoCarsToDriver()
        {
            var driver = new Driver("Bob Smith", "11122233344", "C-0003", "bob@test.com", "11777777777");

            _service.AssignCarsToDriver(driver, Enumerable.Empty<(string, int, int, Guid, Guid, string)>());

            driver.Cars.Should().BeEmpty();
        }

        [Fact(DisplayName = nameof(AssignCarWithInvalidPlateThrows))]
        [Trait("Domain", "DriverCarAssignmentService")]
        public void AssignCarWithInvalidPlateThrows()
        {
            var driver = new Driver("John Doe", "12345678900", "C-0001", "john@test.com", "11999999999");

            var carsData = new[]
            {
                ("Civic", 2024, 2025, Guid.NewGuid(), Guid.NewGuid(), "INVALID")
            };

            Action action = () => _service.AssignCarsToDriver(driver, carsData);

            action.Should().Throw<ArgumentNullException>();
        }
    }
}

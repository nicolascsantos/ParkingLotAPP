using FluentAssertions;
using NSubstitute;
using ParkingLotAPP.Application.DTO;
using ParkingLotAPP.Application.Interfaces;
using ParkingLotAPP.Application.UseCases.Driver.CreateDriver;
using ParkingLotAPP.Domain.Entities;
using ParkingLotAPP.Domain.Interfaces;
using ParkingLotAPP.Domain.Services;

namespace ParkingLotAPP.UnitTests.Application.UseCases.CreateDriver
{
    public class CreateDriverUseCaseTests
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IDriverCarAssignmentService _driverCarAssignmentService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ParkingLotAPP.Application.UseCases.Driver.CreateDriver.CreateDriver _useCase;

        public CreateDriverUseCaseTests()
        {
            _driverRepository = Substitute.For<IDriverRepository>();
            _driverCarAssignmentService = Substitute.For<IDriverCarAssignmentService>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _useCase = new ParkingLotAPP.Application.UseCases.Driver.CreateDriver.CreateDriver(
                _driverRepository, _driverCarAssignmentService, _unitOfWork);
        }

        [Fact(DisplayName = nameof(Handle_ShouldCreateDriver))]
        [Trait("Application", "CreateDriver - UseCases")]
        public async Task Handle_ShouldCreateDriver()
        {
            var input = new CreateDriverInput(
                "John Doe", "12345678900", "C-0001",
                "john@test.com", "11999999999", true, DateTime.Now
            );

            var output = await _useCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Name.Should().Be("John Doe");
            output.Document.Should().Be("12345678900");
            output.IsActive.Should().BeTrue();

            await _driverRepository.Received(1).Add(Arg.Any<Driver>(), Arg.Any<CancellationToken>());
            await _unitOfWork.Received(1).Commit(Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = nameof(Handle_ShouldCreateDriverWithCars))]
        [Trait("Application", "CreateDriver - UseCases")]
        public async Task Handle_ShouldCreateDriverWithCars()
        {
            var cars = new List<CarDTO>
            {
                new CarDTO("Civic", "ABC1234", 2024, 2025, Guid.NewGuid(), Guid.NewGuid()),
                new CarDTO("Corolla", "XYZ5678", 2023, 2024, Guid.NewGuid(), Guid.NewGuid())
            };

            var input = new CreateDriverInput(
                "Jane Doe", "98765432100", "C-0002",
                "jane@test.com", "11888888888", true, DateTime.Now, cars
            );

            var output = await _useCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Name.Should().Be("Jane Doe");

            _driverCarAssignmentService.Received(1).AssignCarsToDriver(
                Arg.Any<Driver>(),
                Arg.Any<IEnumerable<(string, int, int, Guid, Guid, string)>>()
            );

            await _driverRepository.Received(1).Add(Arg.Any<Driver>(), Arg.Any<CancellationToken>());
            await _unitOfWork.Received(1).Commit(Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = nameof(Handle_ShouldNotCallAssignmentServiceWhenNoCars))]
        [Trait("Application", "CreateDriver - UseCases")]
        public async Task Handle_ShouldNotCallAssignmentServiceWhenNoCars()
        {
            var input = new CreateDriverInput(
                "Bob Smith", "11122233344", "C-0003",
                "bob@test.com", "11777777777", true, DateTime.Now
            );

            await _useCase.Handle(input, CancellationToken.None);

            _driverCarAssignmentService.DidNotReceive().AssignCarsToDriver(
                Arg.Any<Driver>(),
                Arg.Any<IEnumerable<(string, int, int, Guid, Guid, string)>>()
            );
        }

        [Fact(DisplayName = nameof(Handle_ShouldThrowWhenDriverNameIsInvalid))]
        [Trait("Application", "CreateDriver - UseCases")]
        public async Task Handle_ShouldThrowWhenDriverNameIsInvalid()
        {
            var input = new CreateDriverInput(
                null!, "12345678900", "C-0001",
                "john@test.com", "11999999999", true, DateTime.Now
            );

            Func<Task> action = () => _useCase.Handle(input, CancellationToken.None);

            await action.Should().ThrowAsync<Exception>();
        }
    }
}

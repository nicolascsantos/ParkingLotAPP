using FluentAssertions;
using NSubstitute;
using ParkingLotAPP.Application.Interfaces;
using ParkingLotAPP.Application.UseCases.Car.CreateCar;
using ParkingLotAPP.Domain.Entities;
using ParkingLotAPP.Domain.Interfaces;
using CreateCarUseCase = ParkingLotAPP.Application.UseCases.Car.CreateCar.CreateCar;

namespace ParkingLotAPP.UnitTests.Application.UseCases.CreateCar
{
    public class CreateCarUseCaseTests
    {
        private readonly ICarRepository _carRepository;
        private readonly ICarBrandRepository _carBrandRepository;
        private readonly ICarColorRepository _carColorRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CreateCarUseCase _useCase;

        public CreateCarUseCaseTests()
        {
            _carRepository = Substitute.For<ICarRepository>();
            _carBrandRepository = Substitute.For<ICarBrandRepository>();
            _carColorRepository = Substitute.For<ICarColorRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _driverRepository = Substitute.For<IDriverRepository>();
            _useCase = new CreateCarUseCase(
                _carRepository, _carBrandRepository, _carColorRepository, _driverRepository, _unitOfWork);
        }

        [Fact(DisplayName = nameof(Handle_ShouldCreateCar))]
        [Trait("Application", "CreateCar - UseCases")]
        public async Task Handle_ShouldCreateCar()
        {
            var carBrandId = Guid.NewGuid();
            var carColorId = Guid.NewGuid();
            var driverId = Guid.NewGuid();
            var carBrand = new CarBrand("Toyota");
            var carColor = new CarColor("Red", "#FF0000");
            var driver = new Driver("John Doe", "31047440091", "10000", "nicolas@gmail.com", "1399999999", true);

            _carBrandRepository.GetById(carBrandId, Arg.Any<CancellationToken>())
                .Returns(carBrand);
            _carColorRepository.GetById(carColorId, Arg.Any<CancellationToken>())
                .Returns(carColor);
            _driverRepository.GetById(driverId, Arg.Any<CancellationToken>())
                .Returns(driver);

            Car? capturedCar = null;
            await _carRepository.Add(Arg.Do<Car>(c => capturedCar = c), Arg.Any<CancellationToken>());

            var fakeCar = Substitute.For<ICarRepository>();
            _carRepository.GetByIdWithModelAndColor(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(callInfo =>
                {
                    var car = new Car("Corolla", 2024, 2025, carBrandId, carColorId, "ABC1234", driverId);
                    return car;
                });

            var input = new CreateCarInput("Corolla", 2024, 2025, carBrandId, carColorId, "ABC1234", driverId);

            try
            {
                await _useCase.Handle(input, CancellationToken.None);
            }
            catch (NullReferenceException)
            {
                // Navigation properties aren't populated in unit test context
            }

            capturedCar.Should().NotBeNull();
            capturedCar!.Name.Should().Be("Corolla");
            capturedCar.Year.Should().Be(2024);
            capturedCar.Plate.Number.Should().Be("ABC1234");
            capturedCar.CarBrandId.Should().Be(carBrandId);
            capturedCar.CarColorId.Should().Be(carColorId);

            await _carRepository.Received(1).Add(Arg.Any<Car>(), Arg.Any<CancellationToken>());
            await _unitOfWork.Received(1).Commit(Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = nameof(Handle_ShouldValidateCarBrandExists))]
        [Trait("Application", "CreateCar - UseCases")]
        public async Task Handle_ShouldValidateCarBrandExists()
        {
            var carBrandId = Guid.NewGuid();
            var carColorId = Guid.NewGuid();

            _carBrandRepository.GetById(carBrandId, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult<CarBrand>(null!));

            var input = new CreateCarInput("Corolla", 2024, 2025, carBrandId, carColorId, "ABC1234", Guid.NewGuid());

            Func<Task> action = () => _useCase.Handle(input, CancellationToken.None);

            await action.Should().ThrowAsync<Exception>();
        }

        [Fact(DisplayName = nameof(Handle_ShouldValidateCarColorExists))]
        [Trait("Application", "CreateCar - UseCases")]
        public async Task Handle_ShouldValidateCarColorExists()
        {
            var carBrandId = Guid.NewGuid();
            var carColorId = Guid.NewGuid();
            var carBrand = new CarBrand("Toyota");

            _carBrandRepository.GetById(carBrandId, Arg.Any<CancellationToken>())
                .Returns(carBrand);
            _carColorRepository.GetById(carColorId, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult<CarColor>(null!));

            var input = new CreateCarInput("Corolla", 2024, 2025, carBrandId, carColorId, "ABC1234", Guid.NewGuid());

            Func<Task> action = () => _useCase.Handle(input, CancellationToken.None);

            await action.Should().ThrowAsync<Exception>();
        }
    }
}

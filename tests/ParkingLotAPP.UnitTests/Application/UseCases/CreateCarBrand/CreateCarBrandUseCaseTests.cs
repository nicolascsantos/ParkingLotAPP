using FluentAssertions;
using NSubstitute;
using ParkingLotAPP.Application.Interfaces;
using ParkingLotAPP.Application.UseCases.CarBrand.CreateCarBrand;
using ParkingLotAPP.Domain.Entities;
using ParkingLotAPP.Domain.Interfaces;
using CreateCarBrandUseCase = ParkingLotAPP.Application.UseCases.CarBrand.CreateCarBrand.CreateCarBrand;

namespace ParkingLotAPP.UnitTests.Application.UseCases.CreateCarBrand
{
    public class CreateCarBrandUseCaseTests
    {
        private readonly ICarBrandRepository _carBrandRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CreateCarBrandUseCase _useCase;

        public CreateCarBrandUseCaseTests()
        {
            _carBrandRepository = Substitute.For<ICarBrandRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _useCase = new CreateCarBrandUseCase(_carBrandRepository, _unitOfWork);
        }

        [Fact(DisplayName = nameof(Handle_ShouldCreateCarBrand))]
        [Trait("Application", "CreateCarBrand - UseCases")]
        public async Task Handle_ShouldCreateCarBrand()
        {
            var input = new CreateCarBrandInput("Toyota");

            _carBrandRepository.GetById(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(callInfo =>
                {
                    var id = callInfo.ArgAt<Guid>(0);
                    return new CarBrand("Toyota");
                });

            var output = await _useCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Name.Should().Be("Toyota");

            await _carBrandRepository.Received(1).Add(Arg.Any<CarBrand>(), Arg.Any<CancellationToken>());
            await _unitOfWork.Received(1).Commit(Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = nameof(Handle_ShouldCallRepositoryAdd))]
        [Trait("Application", "CreateCarBrand - UseCases")]
        public async Task Handle_ShouldCallRepositoryAdd()
        {
            var input = new CreateCarBrandInput("Honda");
            CarBrand? capturedBrand = null;

            await _carBrandRepository.Add(Arg.Do<CarBrand>(x => capturedBrand = x), Arg.Any<CancellationToken>());
            _carBrandRepository.GetById(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(callInfo => new CarBrand("Honda"));

            await _useCase.Handle(input, CancellationToken.None);

            capturedBrand.Should().NotBeNull();
            capturedBrand!.Name.Should().Be("Honda");
        }

        [Fact(DisplayName = nameof(Handle_ShouldThrowWhenNameIsInvalid))]
        [Trait("Application", "CreateCarBrand - UseCases")]
        public async Task Handle_ShouldThrowWhenNameIsInvalid()
        {
            var input = new CreateCarBrandInput(null!);

            Func<Task> action = () => _useCase.Handle(input, CancellationToken.None);

            await action.Should().ThrowAsync<Exception>();
        }
    }
}

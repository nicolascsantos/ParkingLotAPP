using ParkingLotAPP.Application.DTO;
using ParkingLotAPP.Application.Exceptions;
using ParkingLotAPP.Application.Interfaces;
using ParkingLotAPP.Application.UseCases.Car.Common;
using ParkingLotAPP.Domain.Factories;
using ParkingLotAPP.Domain.Interfaces;

namespace ParkingLotAPP.Application.UseCases.Car.CreateCar
{
    public class CreateCar : ICreateCar
    {
        private readonly ICarRepository _carRepository;
        private readonly ICarBrandRepository _carBrandRepository;
        private readonly ICarColorRepository _carColorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCar(
            ICarRepository carRepository,
            ICarBrandRepository carBrandRepository,
            ICarColorRepository carColorRepository,
            IUnitOfWork unitOfWork
        )
        {
            _carRepository = carRepository;
            _carBrandRepository = carBrandRepository;
            _carColorRepository = carColorRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<CarModelOutput> Handle(CreateCarInput request, CancellationToken cancellationToken)
        {
            var carBrand = await _carBrandRepository
                .GetById(request.CarBrandId, cancellationToken) ?? throw new NotFoundException($"CarBrand '{request.CarBrandId}' does not exist.");

            var carColor = await _carColorRepository
                .GetById(request.CarColorId, cancellationToken) ?? throw new NotFoundException($"CarColor '{request.CarColorId}' does not exist.");

            var car = CarFactory.Create(
                request.Name,
                request.Year,
                request.ModelYear,
                request.CarBrandId,
                request.CarColorId,
                request.Plate
            );

            await _carRepository.Add(car, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);

            return new CarModelOutput(
                car.Id,
                car.Name,
                car.Year,
                car.ModelYear,
                new CarBrandDTO(
                    carBrand.Id,
                    carBrand.Name
                ),
                new CarColorDTO(carColor.Id, carColor.Name, carColor.Hex),
                car.Plate.Number
            );
        }
    }
}

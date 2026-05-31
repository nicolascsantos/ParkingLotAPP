using ParkingLotAPP.Application.DTO;
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
        private readonly IDriverRepository _driverRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCar(
            ICarRepository carRepository,
            ICarBrandRepository carBrandRepository,
            ICarColorRepository carColorRepository,
            IDriverRepository driverRepository,
            IUnitOfWork unitOfWork
        )
        {
            _carRepository = carRepository;
            _carBrandRepository = carBrandRepository;
            _carColorRepository = carColorRepository;
            _driverRepository = driverRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<CarModelOutput> Handle(CreateCarInput request, CancellationToken cancellationToken)
        {
            var carBrand = await _carBrandRepository
                .GetById(request.CarBrandId, cancellationToken);

            var carColor = await _carColorRepository
                .GetById(request.CarColorId, cancellationToken);

            var driver = await _driverRepository
                .GetById(request.DriverId, cancellationToken);

            var car = CarFactory.Create(
                request.Name,
                request.Year,
                request.ModelYear,
                request.CarBrandId,
                request.CarColorId,
                request.Plate,
                driver.Id
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
                car.Plate.Number,
                car.Driver != null ? new DriverDTO(
                    car.Driver.Id,
                    car.Driver.Name,
                    car.Driver.Document,
                    car.Driver.ContractNumber,
                    car.Driver.Email,
                    car.Driver.PhoneNumber,
                    car.Driver.IsActive,
                    car.Driver.CreatedAt
                ) : null
            );
        }
    }
}

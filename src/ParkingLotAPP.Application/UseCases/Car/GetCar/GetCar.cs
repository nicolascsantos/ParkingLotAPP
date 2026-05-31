using ParkingLotAPP.Application.Interfaces;
using ParkingLotAPP.Application.UseCases.Car.Common;
using ParkingLotAPP.Domain.Interfaces;
using ParkingLotAPP.Application.DTO;

namespace ParkingLotAPP.Application.UseCases.Car.GetCar
{
    public class GetCar : IGetCar
    {
        private ICarRepository _carRepository;
        public IUnitOfWork _unitOfWork;

        public GetCar(ICarRepository carRepository, IUnitOfWork unitOfWork)
        {
            _carRepository = carRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CarModelOutput> Handle(GetCarInput request, CancellationToken cancellationToken)
        {
            var car = await _carRepository.GetByIdWithModelAndColorAndDriver(request.Id, cancellationToken);

            return new CarModelOutput(
                car.Id,
                car.Name,
                car.Year,
                car.ModelYear,
                new CarBrandDTO(car.CarBrand.Id, car.CarBrand.Name),
                new CarColorDTO(car.CarColor.Id, car.CarColor.Name, car.CarColor.Hex),
                car.Plate.Number
            );
        }
    }
}

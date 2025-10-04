using ParkingLotAPP.Application.Interfaces;
using ParkingLotAPP.Application.UseCases.Car.Common;
using ParkingLotAPP.Domain.Interfaces;

namespace ParkingLotAPP.Application.UseCases.Car.UpdateCar
{
    public class UpdateCar : IUpdateCar
    {
        private readonly ICarRepository _carRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCar(ICarRepository carRepository, IUnitOfWork unitOfWork)
        {
            _carRepository = carRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CarModelOutput> Handle(UpdateCarInput request, CancellationToken cancellationToken)
        {
            var car = await _carRepository.GetByIdWithModelAndColor(request.Id, cancellationToken);

            car.Update(request.Name,
                request.Year,
                request.ModelYear,
                request.CarBrandId,
                request.CarColorId,
                request.Plate
            );
            await _carRepository.Update(car);
            await _unitOfWork.Commit(cancellationToken);
            return CarModelOutput.FromCar(car);
        }
    }
}

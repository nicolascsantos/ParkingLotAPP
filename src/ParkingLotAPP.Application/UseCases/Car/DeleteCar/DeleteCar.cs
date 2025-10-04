using ParkingLotAPP.Application.Exceptions;
using ParkingLotAPP.Application.Interfaces;
using ParkingLotAPP.Domain.Interfaces;
using MediatR;

namespace ParkingLotAPP.Application.UseCases.Car.DeleteCar
{
    public class DeleteCar : IDeleteCar
    {
        private readonly ICarRepository _carRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCar(ICarRepository carRepository, IUnitOfWork unitOfWork)
        {
            _carRepository = carRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteCarInput request, CancellationToken cancellationToken)
        {
            var car = await _carRepository.GetById(request.Id, cancellationToken);
            NotFoundException.ThrowIfNull(car, $"Genre '{request.Id}' not found.");

            await _carRepository.Delete(car);
            await _unitOfWork.Commit(cancellationToken);
            return await Task.FromResult(Unit.Value);
        }
    }
}

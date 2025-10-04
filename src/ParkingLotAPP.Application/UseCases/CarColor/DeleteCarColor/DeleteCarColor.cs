using ParkingLotAPP.Application.Exceptions;
using ParkingLotAPP.Application.Interfaces;
using ParkingLotAPP.Domain.Interfaces;
using MediatR;

namespace ParkingLotAPP.Application.UseCases.CarColor.DeleteCarColor
{
    public class DeleteCarColor : IDeleteCarColor
    {
        private readonly ICarColorRepository _carColorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCarColor(ICarColorRepository carColorRepository, IUnitOfWork unitOfWork)
        {
            _carColorRepository = carColorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteCarColorInput request, CancellationToken cancellationToken)
        {
            var carColor = await _carColorRepository.GetById(request.Id, cancellationToken);
            NotFoundException.ThrowIfNull(carColor, $"CarColor '{request.Id}' not found.");

            await _carColorRepository.Delete(carColor);
            await _unitOfWork.Commit(cancellationToken);
            return await Task.FromResult(Unit.Value);
        }
    }
}

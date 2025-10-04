using ParkingLotAPP.Application.Interfaces;
using ParkingLotAPP.Application.UseCases.CarColor.Common;
using ParkingLotAPP.Domain.Interfaces;

namespace ParkingLotAPP.Application.UseCases.CarColor.UpdateCarColor
{
    internal class UpdateCarColor : IUpdateCarColor
    {
        private readonly ICarColorRepository _carColorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCarColor(ICarColorRepository carColorRepository, IUnitOfWork unitOfWork)
        {
            _carColorRepository = carColorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CarColorModelOutput> Handle(UpdateCarColorInput request, CancellationToken cancellationToken)
        {
            var carColor = await _carColorRepository
                .GetById(request.Id, cancellationToken);

            carColor.Update(request.Name, request.Hex);

            await _carColorRepository.Update(carColor);
            await _unitOfWork.Commit(cancellationToken);
            return CarColorModelOutput.FromCarColor(carColor);
        }
    }
}

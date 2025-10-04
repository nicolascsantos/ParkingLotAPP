using ParkingLotAPP.Application.Interfaces;
using ParkingLotAPP.Application.UseCases.CarColor.Common;
using ParkingLotAPP.Domain.Interfaces;
using DomainEntity = ParkingLotAPP.Domain.Entities;

namespace ParkingLotAPP.Application.UseCases.CarColor.CreateCarColor
{
    public class CreateCarColor : ICreateCarColor
    {
        private readonly ICarColorRepository _carColorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCarColor(ICarColorRepository carColorRepository, IUnitOfWork unitOfWork)
        {
            _carColorRepository = carColorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CarColorModelOutput> Handle(CreateCarColorInput request, CancellationToken cancellationToken)
        {
            var carColor = new DomainEntity.CarColor(request.Name, request.Hex);

            await _carColorRepository.Add(carColor, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);

            var newCarColor = await _carColorRepository.GetById(carColor.Id, cancellationToken);

            return new CarColorModelOutput(
                newCarColor.Id,
                newCarColor.Name, 
                newCarColor.Hex
            );
        }
    }
}

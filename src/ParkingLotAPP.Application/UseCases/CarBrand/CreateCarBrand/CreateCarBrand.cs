using ParkingLotAPP.Application.Interfaces;
using ParkingLotAPP.Application.UseCases.CarBrand.Common;
using ParkingLotAPP.Domain.Interfaces;
using DomainEntity = ParkingLotAPP.Domain.Entities;

namespace ParkingLotAPP.Application.UseCases.CarBrand.CreateCarBrand
{
    public class CreateCarBrand : ICreateCarBrand
    {
        private readonly ICarBrandRepository _carBrandRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCarBrand(ICarBrandRepository carBrandRepository, IUnitOfWork unitOfWork)
        {
            _carBrandRepository = carBrandRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CarBrandModelOutput> Handle(CreateCarBrandInput request, CancellationToken cancellationToken)
        {
            var carBrand = new DomainEntity.CarBrand(request.Name);

            await _carBrandRepository.Add(carBrand, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);

            var newCarBrand = await _carBrandRepository.GetById(carBrand.Id, cancellationToken);

            return new CarBrandModelOutput(newCarBrand.Id, newCarBrand.Name);
        }
    }
}

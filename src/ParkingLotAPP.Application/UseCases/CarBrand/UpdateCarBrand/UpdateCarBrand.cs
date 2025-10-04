using ParkingLotAPP.Application.Interfaces;
using ParkingLotAPP.Application.UseCases.CarBrand.Common;
using ParkingLotAPP.Domain.Interfaces;

namespace ParkingLotAPP.Application.UseCases.CarBrand.UpdateCarBrand
{
    public class UpdateCarBrand : IUpdateCarBrand
    {
        private readonly ICarBrandRepository _carBrandRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCarBrand(ICarBrandRepository carBrandRepository, IUnitOfWork unitOfWork)
        {
            _carBrandRepository = carBrandRepository;
            _unitOfWork = unitOfWork;
        } 

        public async Task<CarBrandModelOutput> Handle(UpdateCarBrandInput request, CancellationToken cancellationToken)
        {
            var carBrand = await _carBrandRepository.GetById(request.Id, cancellationToken);
            carBrand.Update(request.Name);

            await _carBrandRepository.Update(carBrand);
            await _unitOfWork.Commit(cancellationToken);
            return CarBrandModelOutput.FromCarBrand(carBrand);
        }
    }
}

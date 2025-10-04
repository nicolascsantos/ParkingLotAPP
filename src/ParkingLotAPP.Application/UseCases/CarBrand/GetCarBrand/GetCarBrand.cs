using ParkingLotAPP.Application.Interfaces;
using ParkingLotAPP.Application.UseCases.CarBrand.Common;
using ParkingLotAPP.Domain.Interfaces;

namespace ParkingLotAPP.Application.UseCases.CarBrand.GetCarBrand
{
    public class GetCarBrand : IGetCarBrand
    {
        private ICarBrandRepository _carBrandRepository;
        private IUnitOfWork _unitOfWork;

        public GetCarBrand(ICarBrandRepository carBrandRepository, IUnitOfWork unitOfWork)
        {
            _carBrandRepository = carBrandRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<CarBrandModelOutput> Handle(GetCarBrandInput request, CancellationToken cancellationToken)
        {
            var carBrand = await _carBrandRepository.GetById(request.Id, cancellationToken);

            return CarBrandModelOutput.FromCarBrand(carBrand);
        }
    }
}

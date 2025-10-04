using ParkingLotAPP.Application.Interfaces;
using ParkingLotAPP.Domain.Interfaces;
using MediatR;

namespace ParkingLotAPP.Application.UseCases.CarBrand.DeleteCarBrand
{
    public class DeleteCarBrand : IDeleteCarBrand
    {
        private readonly ICarBrandRepository _carBrandRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCarBrand(ICarBrandRepository carBrandRepository, IUnitOfWork unitOfWork)
        {
            _carBrandRepository = carBrandRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteCarBrandInput request, CancellationToken cancellationToken)
        {
            var carBrand = await _carBrandRepository.GetById(request.Id, cancellationToken);

            await _carBrandRepository.Delete(carBrand);
            await _unitOfWork.Commit(cancellationToken);
            return await Task.FromResult(Unit.Value);
        }
    }
}

using ParkingLotAPP.Application.UseCases.CarBrand.Common;
using MediatR;

namespace ParkingLotAPP.Application.UseCases.CarBrand.DeleteCarBrand
{
    public interface IDeleteCarBrand : IRequestHandler<DeleteCarBrandInput, Unit>
    {
    }
}

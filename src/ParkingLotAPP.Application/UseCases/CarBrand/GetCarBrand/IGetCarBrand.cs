using ParkingLotAPP.Application.UseCases.CarBrand.Common;
using MediatR;

namespace ParkingLotAPP.Application.UseCases.CarBrand.GetCarBrand
{
    public interface IGetCarBrand : IRequestHandler<GetCarBrandInput, CarBrandModelOutput>
    {
    }
}

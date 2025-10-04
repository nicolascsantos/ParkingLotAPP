using ParkingLotAPP.Application.UseCases.CarBrand.Common;
using MediatR;

namespace ParkingLotAPP.Application.UseCases.CarBrand.UpdateCarBrand
{
    public interface IUpdateCarBrand : IRequestHandler<UpdateCarBrandInput, CarBrandModelOutput>
    {

    }
}

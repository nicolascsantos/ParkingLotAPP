using ParkingLotAPP.Application.UseCases.CarBrand.Common;
using MediatR;

namespace ParkingLotAPP.Application.UseCases.CarBrand.CreateCarBrand
{
    public interface ICreateCarBrand : IRequestHandler<CreateCarBrandInput, CarBrandModelOutput>
    {
    }
}

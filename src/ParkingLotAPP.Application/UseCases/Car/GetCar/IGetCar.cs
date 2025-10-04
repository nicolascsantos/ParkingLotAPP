using ParkingLotAPP.Application.UseCases.Car.Common;
using MediatR;

namespace ParkingLotAPP.Application.UseCases.Car.GetCar
{
    public interface IGetCar : IRequestHandler<GetCarInput, CarModelOutput>
    {
    }
}

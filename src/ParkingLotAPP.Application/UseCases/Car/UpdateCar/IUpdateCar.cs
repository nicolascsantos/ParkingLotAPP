using ParkingLotAPP.Application.UseCases.Car.Common;
using MediatR;

namespace ParkingLotAPP.Application.UseCases.Car.UpdateCar
{
    public interface IUpdateCar : IRequestHandler<UpdateCarInput, CarModelOutput>
    {
    }
}

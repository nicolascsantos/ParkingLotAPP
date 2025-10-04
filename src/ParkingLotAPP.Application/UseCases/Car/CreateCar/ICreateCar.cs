using ParkingLotAPP.Application.UseCases.Car.Common;
using MediatR;

namespace ParkingLotAPP.Application.UseCases.Car.CreateCar
{
    public interface ICreateCar : IRequestHandler<CreateCarInput, CarModelOutput>
    {
    }
}

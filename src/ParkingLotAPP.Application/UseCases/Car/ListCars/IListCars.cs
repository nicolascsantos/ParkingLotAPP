using ParkingLotAPP.Application.UseCases.CarColor.ListCarColors;
using MediatR;

namespace ParkingLotAPP.Application.UseCases.Car.ListCars
{
    public interface IListCars : IRequestHandler<ListCarsInput, ListCarsOutput>
    {
    }
}

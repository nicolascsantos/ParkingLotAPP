using MediatR;

namespace ParkingLotAPP.Application.UseCases.Car.DeleteCar
{
    public interface IDeleteCar : IRequestHandler<DeleteCarInput, Unit>
    {
    }
}

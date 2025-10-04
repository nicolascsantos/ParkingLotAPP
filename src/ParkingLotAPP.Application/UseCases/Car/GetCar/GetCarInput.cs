using ParkingLotAPP.Application.UseCases.Car.Common;
using MediatR;

namespace ParkingLotAPP.Application.UseCases.Car.GetCar
{
    public class GetCarInput : IRequest<CarModelOutput>
    {
        public Guid Id { get; set; }

        public GetCarInput(Guid id)
            => Id = id;
    }
}

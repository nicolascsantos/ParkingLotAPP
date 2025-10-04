using MediatR;

namespace ParkingLotAPP.Application.UseCases.Car.DeleteCar
{
    public class DeleteCarInput : IRequest<Unit>
    {
        public DeleteCarInput(Guid id)
            => Id = id;
        

        public Guid Id { get; set; }
    }
}

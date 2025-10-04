using MediatR;

namespace ParkingLotAPP.Application.UseCases.CarColor.DeleteCarColor
{
    public class DeleteCarColorInput : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public DeleteCarColorInput(Guid id)
        {
            Id = id;
        }
    }
}

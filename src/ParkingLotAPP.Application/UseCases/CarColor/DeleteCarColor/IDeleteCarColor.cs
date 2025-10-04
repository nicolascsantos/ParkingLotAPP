using MediatR;

namespace ParkingLotAPP.Application.UseCases.CarColor.DeleteCarColor
{
    public interface IDeleteCarColor : IRequestHandler<DeleteCarColorInput, Unit>
    {
    }
}

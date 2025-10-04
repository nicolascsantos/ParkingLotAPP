using MediatR;

namespace ParkingLotAPP.Application.UseCases.CarColor.ListCarColors
{
    public interface IListCarColors : IRequestHandler<ListCarColorsInput, ListCarColorsOutput>
    {
    }
}

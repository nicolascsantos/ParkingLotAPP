using ParkingLotAPP.Application.UseCases.CarColor.Common;
using MediatR;

namespace ParkingLotAPP.Application.UseCases.CarColor.UpdateCarColor
{
    public interface IUpdateCarColor : IRequestHandler<UpdateCarColorInput, CarColorModelOutput>
    {
    }
}

using ParkingLotAPP.Application.UseCases.CarColor.Common;
using MediatR;

namespace ParkingLotAPP.Application.UseCases.CarColor.CreateCarColor
{
    public interface ICreateCarColor : IRequestHandler<CreateCarColorInput, CarColorModelOutput>
    {
    }
}

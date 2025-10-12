using MediatR;
using ParkingLotAPP.Application.UseCases.Driver.Common;

namespace ParkingLotAPP.Application.UseCases.Driver.CreateDriver
{
    public interface ICreateDriver : IRequestHandler<CreateDriverInput, DriverModelOutput>
    {
    }
}

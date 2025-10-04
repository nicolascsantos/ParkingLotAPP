using ParkingLotAPP.Application.UseCases.CarBrand.Common;
using MediatR;

namespace ParkingLotAPP.Application.UseCases.CarBrand.DeleteCarBrand
{
    public class DeleteCarBrandInput : IRequest<Unit>
    {
        public DeleteCarBrandInput(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}

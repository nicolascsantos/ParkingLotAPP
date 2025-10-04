using ParkingLotAPP.Application.UseCases.CarBrand.Common;
using MediatR;

namespace ParkingLotAPP.Application.UseCases.CarBrand.GetCarBrand
{
    public class GetCarBrandInput : IRequest<CarBrandModelOutput>
    {
        public Guid Id { get; set; }

        public GetCarBrandInput(Guid ìd)
            => Id = ìd;
    }
}

using ParkingLotAPP.Application.UseCases.CarBrand.Common;
using MediatR;

namespace ParkingLotAPP.Application.UseCases.CarBrand.UpdateCarBrand
{
    public class UpdateCarBrandInput : IRequest<CarBrandModelOutput>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public UpdateCarBrandInput(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}

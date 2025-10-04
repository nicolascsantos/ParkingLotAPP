using ParkingLotAPP.Application.UseCases.CarBrand.Common;
using MediatR;

namespace ParkingLotAPP.Application.UseCases.CarBrand.CreateCarBrand
{
    public class CreateCarBrandInput : IRequest<CarBrandModelOutput>
    {
        public CreateCarBrandInput(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}

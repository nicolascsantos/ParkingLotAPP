using ParkingLotAPP.Application.UseCases.CarColor.Common;
using MediatR;

namespace ParkingLotAPP.Application.UseCases.CarColor.CreateCarColor
{
    public class CreateCarColorInput : IRequest<CarColorModelOutput>
    {
        public CreateCarColorInput(string name, string hex)
        {
            Name = name;
            Hex = hex;
        }

        public string Name { get; set; }
        public string Hex { get; set; }
    }
}

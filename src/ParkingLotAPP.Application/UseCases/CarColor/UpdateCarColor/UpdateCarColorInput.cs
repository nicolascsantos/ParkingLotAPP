using ParkingLotAPP.Application.UseCases.CarColor.Common;
using MediatR;

namespace ParkingLotAPP.Application.UseCases.CarColor.UpdateCarColor
{
    public class UpdateCarColorInput : IRequest<CarColorModelOutput>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Hex { get; set; }

        public UpdateCarColorInput(Guid id, string name, string hex)
        {
            Id = id;
            Name = name;
            Hex = hex;
        }
    }
}

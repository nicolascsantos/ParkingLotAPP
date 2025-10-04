using DomainEntity = ParkingLotAPP.Domain.Entities;

namespace ParkingLotAPP.Application.UseCases.CarColor.Common
{
    public class CarColorModelOutput
    {
        public CarColorModelOutput(Guid id, string name, string hex)
        {
            Id = id;
            Name = name;
            Hex = hex;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Hex { get; set; }

        public static CarColorModelOutput FromCarColor(DomainEntity.CarColor carColor)
        => new(
            carColor.Id,
            carColor.Name,
            carColor.Hex
        );

    }
}

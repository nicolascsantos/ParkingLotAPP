using DomainEntity = ParkingLotAPP.Domain.Entities;

namespace ParkingLotAPP.Application.UseCases.CarBrand.Common
{
    public class CarBrandModelOutput
    {
        public CarBrandModelOutput(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public static CarBrandModelOutput FromCarBrand(DomainEntity.CarBrand carBrand)
            => new CarBrandModelOutput(carBrand.Id, carBrand.Name);
    }
}

using ParkingLotAPP.Domain.SeedWork;
using ParkingLotAPP.Domain.Validation;

namespace ParkingLotAPP.Domain.Entities
{
    public class CarBrand : Entity
    {
        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<Car> Cars {  get; set; } = new List<Car>();
        public CarBrand(string name)
        {
            Name = name;
            CreatedAt = DateTime.Now;
        }

        public CarBrand()
        {
            
        }

        public void Update(string name)
        {
            Name = name;
        }

        public void Validate()
        {
            DomainValidation.NotNull(Name, nameof(Name));
        }
    }
}

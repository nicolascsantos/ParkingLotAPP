using ParkingLotAPP.Domain.SeedWork;
using ParkingLotAPP.Domain.Validation;

namespace ParkingLotAPP.Domain.Entities
{
    public class CarColor : Entity
    {
        public CarColor(string name, string hex)
        {
            Name = name;
            Hex = hex;
            CreatedAt = DateTime.Now;
            Validate();
        }

        public CarColor()
        {
            
        }

        public string Name { get; set; }

        public string Hex { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<Car> Cars { get; set; } = new List<Car>();

        public void Update(string name, string hex)
        {
            Name = name;
            Hex = hex;
        }

        public void Validate()
        {
            DomainValidation.NotNullOrEmpty(Name, nameof(Name));
            DomainValidation.NotNull(Hex, nameof(Hex));
        }
    }
}

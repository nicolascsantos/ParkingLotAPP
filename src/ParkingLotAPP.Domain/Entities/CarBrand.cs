using ParkingLotAPP.Domain.SeedWork;
using ParkingLotAPP.Domain.Validation;

namespace ParkingLotAPP.Domain.Entities
{
    public class CarBrand : AggregateRoot
    {
        public string Name { get; private set; }

        public DateTime CreatedAt { get; private set; }

        private readonly List<Car> _cars = new();
        public IReadOnlyCollection<Car> Cars => _cars.AsReadOnly();

        public CarBrand(string name) : base()
        {
            Name = name;
            CreatedAt = DateTime.Now;
            Validate();
        }

        private CarBrand() { }

        public void Update(string name)
        {
            Name = name;
            Validate();
        }

        public override void Validate()
        {
            DomainValidation.NotNull(Name, nameof(Name));
            DomainValidation.NotNullOrEmpty(Name, nameof(Name));
        }
    }
}

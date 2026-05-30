using ParkingLotAPP.Domain.SeedWork;
using ParkingLotAPP.Domain.Validation;

namespace ParkingLotAPP.Domain.Entities
{
    public class CarColor : AggregateRoot
    {
        public string Name { get; private set; }

        public string Hex { get; private set; }

        public DateTime CreatedAt { get; private set; }

        private readonly List<Car> _cars = new();
        public IReadOnlyCollection<Car> Cars => _cars.AsReadOnly();

        public CarColor(string name, string hex) : base()
        {
            Name = name;
            Hex = hex;
            CreatedAt = DateTime.Now;
            Validate();
        }

        private CarColor() { }

        public void Update(string name, string hex)
        {
            Name = name;
            Hex = hex;
            Validate();
        }

        public override void Validate()
        {
            DomainValidation.NotNullOrEmpty(Name, nameof(Name));
            DomainValidation.NotNull(Hex, nameof(Hex));
        }
    }
}

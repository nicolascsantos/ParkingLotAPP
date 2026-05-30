using ParkingLotAPP.Domain.SeedWork;
using ParkingLotAPP.Domain.Validation;

namespace ParkingLotAPP.Domain.Entities
{
    public class Driver : AggregateRoot
    {
        public string Name { get; private set; }

        public string Document { get; private set; }

        public string ContractNumber { get; private set; }

        public string Email { get; private set; }

        public string PhoneNumber { get; private set; }

        public bool IsActive { get; private set; }

        public DateTime CreatedAt { get; private set; }

        private readonly List<Car> _cars = new();
        public IReadOnlyCollection<Car> Cars => _cars.AsReadOnly();

        public Driver(
            string name,
            string document,
            string contractNumber,
            string email,
            string phoneNumber,
            bool isActive = true
        ) : base()
        {
            Name = name;
            Document = document;
            ContractNumber = contractNumber;
            Email = email;
            PhoneNumber = phoneNumber;
            CreatedAt = DateTime.UtcNow;
            IsActive = isActive;
            Validate();
        }

        private Driver() { }

        public void Activate()
        {
            IsActive = true;
            Validate();
        }

        public void Deactivate()
        {
            IsActive = false;
            Validate();
        }

        public void AddCar(Car car)
        {
            car.AssignDriver(this);
            _cars.Add(car);
        }

        public override void Validate()
        {
            DomainValidation.NotNull(Name, nameof(Name));
            DomainValidation.MinLength(Name, nameof(Name), 3);
            DomainValidation.MaxLength(Name, nameof(Name), 100);
            DomainValidation.NotNull(Document, nameof(Document));
            DomainValidation.NotNull(ContractNumber, nameof(ContractNumber));
        }
    }
}

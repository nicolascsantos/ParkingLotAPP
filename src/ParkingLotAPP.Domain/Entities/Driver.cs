using ParkingLotAPP.Domain.SeedWork;
using ParkingLotAPP.Domain.Validation;

namespace ParkingLotAPP.Domain.Entities
{
    public class Driver : Entity
    {
        public string Name { get; set; }

        public string Document { get; set; }

        public string ContractNumber { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<Car> Cars { get; set; } = new List<Car>();

        public Driver(
            string name,
            string document,
            string contractNumber,
            string email,
            string phoneNumber,
            bool isActive = true
        )
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

        public Driver() {}

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

        public void Validate()
        {
            DomainValidation.NotNull(Name, nameof(Name));
            DomainValidation.MinLength(Name, nameof(Name), 3);
            DomainValidation.MaxLength(Name, nameof(Name), 100);
            DomainValidation.NotNull(Document, nameof(Document));
            DomainValidation.NotNull(ContractNumber, nameof(ContractNumber));
        }
    }
}

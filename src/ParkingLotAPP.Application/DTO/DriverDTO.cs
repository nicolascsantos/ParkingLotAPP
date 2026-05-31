namespace ParkingLotAPP.Application.DTO
{
    public class DriverDTO
    {
        public DriverDTO(
            Guid id,
            string name,
            string document,
            string contractNumber,
            string email,
            string phoneNumber,
            bool isActive,
            DateTime createdAt
        )
        {
            Id = id;
            Name = name;
            Document = document;
            ContractNumber = contractNumber;
            Email = email;
            PhoneNumber = phoneNumber;
            IsActive = isActive;
            CreatedAt = createdAt;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public string Document { get; private set; }

        public string ContractNumber { get; private set; }

        public string Email { get; private set; }

        public string PhoneNumber { get; private set; }

        public bool IsActive { get; private set; }

        public DateTime CreatedAt { get; private set; }
    }
}

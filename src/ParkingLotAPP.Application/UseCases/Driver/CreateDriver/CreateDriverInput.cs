using MediatR;
using ParkingLotAPP.Application.DTO;
using ParkingLotAPP.Application.UseCases.Driver.Common;

namespace ParkingLotAPP.Application.UseCases.Driver.CreateDriver
{
    public class CreateDriverInput : IRequest<DriverModelOutput>
    {

        public string Name { get; set; }

        public string Document { get; set; }

        public string ContractNumber { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<CarDTO>? Cars { get; set; }
        public CreateDriverInput(
            string name,
            string document,
            string contractNumber,
            string email,
            string phoneNumber,
            bool isActive,
            DateTime createdAt,
            List<CarDTO>? cars = null
        )
        {
            Name = name;
            Document = document;
            ContractNumber = contractNumber;
            Email = email;
            PhoneNumber = phoneNumber;
            IsActive = isActive;
            CreatedAt = createdAt;
            Cars = cars;
        }
    }
}

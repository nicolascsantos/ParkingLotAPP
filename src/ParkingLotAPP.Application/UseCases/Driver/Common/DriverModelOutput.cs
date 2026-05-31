using ParkingLotAPP.Application.UseCases.Car.Common;
using DomainEntity = ParkingLotAPP.Domain.Entities;

namespace ParkingLotAPP.Application.UseCases.Driver.Common
{
    public class DriverModelOutput
    {
        public DriverModelOutput(
            Guid id,
            string name,
            string document,
            string contractNumber,
            string email,
            string phoneNumber,
            bool isActive,
            DateTime createdAt,
            List<CarModelOutput> cars
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
            Cars = cars;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Document { get; set; }

        public string ContractNumber { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<CarModelOutput> Cars { get; set; }

        public static DriverModelOutput FromDriver(DomainEntity.Driver driver)
        {
            List<CarModelOutput> carModelOutputList = new List<CarModelOutput>();
            foreach (var car in driver.Cars)
            {
                carModelOutputList.Add(CarModelOutput.FromCar(car));
            }

            return new DriverModelOutput(
                        driver.Id,
                        driver.Name,
                        driver.Document,
                        driver.ContractNumber,
                        driver.Email,
                        driver.PhoneNumber,
                        driver.IsActive,
                        driver.CreatedAt,
                        carModelOutputList
            );
        }
    }
}

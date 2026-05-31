using ParkingLotAPP.Application.DTO;
using DomainEntity = ParkingLotAPP.Domain.Entities;


namespace ParkingLotAPP.Application.UseCases.Car.Common
{
    public class CarModelOutput
    {
        public CarModelOutput(
            Guid id,
            string name,
            int year,
            int modelYear,
            CarBrandDTO carBrand,
            CarColorDTO carColor,
            string plate,
            DriverDTO? driver = null)
        {
            Id = id;
            Name = name;
            Year = year;
            ModelYear = modelYear;
            CarBrand = carBrand;
            CarColor = carColor;
            Plate = plate;
            Driver = driver;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public int Year { get; private set; }
        public int ModelYear { get; private set; }

        public CarBrandDTO CarBrand { get; private set; }

        public CarColorDTO CarColor { get; private set; }

        public string Plate { get; private set; }

        public DriverDTO? Driver { get; private set; }

        public static CarModelOutput FromCar(DomainEntity.Car car)
        => new CarModelOutput(
                car.Id,
                car.Name,
                car.Year,
                car.ModelYear,
                new CarBrandDTO(car.CarBrand.Id, car.CarBrand.Name),
                new CarColorDTO(car.CarColor.Id, car.CarColor.Name, car.CarColor.Hex),
                car.Plate.Number,
                new DriverDTO(
                    car.Driver.Id,
                    car.Driver.Name,
                    car.Driver.Document,
                    car.Driver.ContractNumber,
                    car.Driver.Email,
                    car.Driver.PhoneNumber,
                    car.Driver.IsActive,
                    car.Driver.CreatedAt
                )
            );
    }
}


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
            string plate)
        {
            Id = id;
            Name = name;
            Year = year;
            ModelYear = modelYear;
            CarBrand = carBrand;
            CarColor = carColor;
            Plate = plate;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Year { get; set; }
        public int ModelYear { get; set; }

        public CarBrandDTO CarBrand { get; set; }

        public CarColorDTO CarColor { get; set; }

        public string Plate { get; set; }

        public static CarModelOutput FromCar(DomainEntity.Car car)
        => new CarModelOutput(
                car.Id,
                car.Name,
                car.Year,
                car.ModelYear,
                new CarBrandDTO(car.CarBrand.Id, car.CarBrand.Name),
                new CarColorDTO(car.CarColor.Id, car.CarColor.Name, car.CarColor.Hex),
                car.Plate.Number
            );
    }
}


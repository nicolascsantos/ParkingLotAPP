using ParkingLotAPP.Application.UseCases.Car.Common;
using MediatR;

namespace ParkingLotAPP.Application.UseCases.Car.CreateCar
{
    public class CreateCarInput : IRequest<CarModelOutput>
    {
        public CreateCarInput(string name, int year, int modelYear, Guid carBrandId, Guid carColorId, string plate)
        {
            Name = name;
            Year = year;
            ModelYear = modelYear;
            CarBrandId = carBrandId;
            CarColorId = carColorId;
        }

        public CreateCarInput()
        {
            
        }

        public string Name { get; set; }

        public int Year { get; set; }

        public int ModelYear { get; set; }

        public Guid CarBrandId { get; set; }

        public Guid CarColorId { get; set; }

        public string Plate { get; set; }
    }
}

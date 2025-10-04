using ParkingLotAPP.Application.UseCases.Car.Common;
using MediatR;

namespace ParkingLotAPP.Application.UseCases.Car.UpdateCar
{
    public class UpdateCarInput : IRequest<CarModelOutput>
    {
        public UpdateCarInput(Guid id, string name, int year, int modelYear, Guid carBrandId, Guid carColorId, string plate)
        {
            Id = id;
            Name = name;
            Year = year;
            ModelYear = modelYear;
            CarBrandId = carBrandId;
            CarColorId = carColorId;
            Plate = plate;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Year { get; set; }

        public int ModelYear { get; set; }

        public Guid CarBrandId { get; set; }

        public Guid CarColorId { get; set; }

        public string Plate { get; set; }
    }
}

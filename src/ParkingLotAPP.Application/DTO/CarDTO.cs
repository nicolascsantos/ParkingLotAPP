namespace ParkingLotAPP.Application.DTO
{
    public class CarDTO
    {
        public string Name { get; set; }

        public string Plate { get; set; }

        public int Year { get; set; }

        public int ModelYear { get; set; }

        public Guid CarBrandId { get; set; }

        public Guid CarColorId { get; set; }

        public CarDTO(
            string name,
            string plate,
            int year,
            int modelYear,
            Guid carBrandId,
            Guid carColorId
        )
        {
            Name = name;
            Plate = plate;
            Year = year;
            ModelYear = modelYear;
            CarBrandId = carBrandId;
            CarColorId = carColorId;
        }
    }
}

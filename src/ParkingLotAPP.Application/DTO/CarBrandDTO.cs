namespace ParkingLotAPP.Application.DTO
{
    public class CarBrandDTO
    {
        public CarBrandDTO(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}

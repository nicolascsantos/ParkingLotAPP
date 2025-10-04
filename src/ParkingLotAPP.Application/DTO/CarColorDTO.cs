namespace ParkingLotAPP.Application.DTO
{
    public class CarColorDTO
    {
        public CarColorDTO(Guid id, string name, string hex)
        {
            Id = id;
            Name = name;
            Hex = hex;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Hex { get; set; }
    }
}

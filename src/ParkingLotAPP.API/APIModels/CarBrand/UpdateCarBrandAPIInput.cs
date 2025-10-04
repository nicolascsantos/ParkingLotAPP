namespace ParkingLotAPP.API.APIModels.CarBrand
{
    public class UpdateCarBrandAPIInput
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public UpdateCarBrandAPIInput(Guid id, string name)
        {
            Id = id; 
            Name = name;
        }
    }
}

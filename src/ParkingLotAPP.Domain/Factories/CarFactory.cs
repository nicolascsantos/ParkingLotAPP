using ParkingLotAPP.Domain.Entities;

namespace ParkingLotAPP.Domain.Factories
{
    public static class CarFactory
    {
        public static Car Create(
            string name,
            int year,
            int modelYear,
            Guid carBrandId,
            Guid carColorId,
            string plate,
            Guid driverId
        )
        {
            return new Car(name, year, modelYear, carBrandId, carColorId, plate, driverId);
        }
    }
}

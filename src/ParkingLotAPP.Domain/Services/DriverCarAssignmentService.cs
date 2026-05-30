using ParkingLotAPP.Domain.Entities;
using ParkingLotAPP.Domain.Factories;

namespace ParkingLotAPP.Domain.Services
{
    public class DriverCarAssignmentService : IDriverCarAssignmentService
    {
        public void AssignCarsToDriver(
            Driver driver,
            IEnumerable<(string Name, int Year, int ModelYear, Guid CarBrandId, Guid CarColorId, string Plate)> carsData
        )
        {
            foreach (var carData in carsData)
            {
                var car = CarFactory.Create(
                    carData.Name,
                    carData.Year,
                    carData.ModelYear,
                    carData.CarBrandId,
                    carData.CarColorId,
                    carData.Plate
                );

                driver.AddCar(car);
            }
        }
    }
}

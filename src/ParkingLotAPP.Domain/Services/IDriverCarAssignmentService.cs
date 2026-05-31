using ParkingLotAPP.Domain.Entities;

namespace ParkingLotAPP.Domain.Services
{
    public interface IDriverCarAssignmentService
    {
        void AssignCarsToDriver(
            Driver driver,
            IEnumerable<(string Name, int Year, int ModelYear, Guid CarBrandId, Guid CarColorId, string Plate)> carsData
        );
    }
}

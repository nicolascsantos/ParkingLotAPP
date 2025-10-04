using ParkingLotAPP.Domain.Entities;
using ParkingLotAPP.Domain.SeedWork;
using ParkingLotAPP.Domain.SeedWork.SearchableRepository;

namespace ParkingLotAPP.Domain.Interfaces
{
    public interface ICarColorRepository : IGenericRepository<CarColor>, ISearchableRepository<CarColor>
    {
    }
}

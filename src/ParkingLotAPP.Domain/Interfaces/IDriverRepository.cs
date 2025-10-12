using ParkingLotAPP.Domain.Entities;
using ParkingLotAPP.Domain.SeedWork;
using ParkingLotAPP.Domain.SeedWork.SearchableRepository;

namespace ParkingLotAPP.Domain.Interfaces
{
    public interface IDriverRepository : IGenericRepository<Driver>, ISearchableRepository<Driver>
    {
    }
}

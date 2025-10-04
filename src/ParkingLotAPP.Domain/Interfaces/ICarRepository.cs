using ParkingLotAPP.Domain.Entities;
using ParkingLotAPP.Domain.SeedWork;
using ParkingLotAPP.Domain.SeedWork.SearchableRepository;

namespace ParkingLotAPP.Domain.Interfaces
{
    public interface ICarRepository : IGenericRepository<Car>, ISearchableRepository<Car>
    {
        Task<Car> GetByIdWithModelAndColor(Guid id, CancellationToken cancellationToken);
    }
}

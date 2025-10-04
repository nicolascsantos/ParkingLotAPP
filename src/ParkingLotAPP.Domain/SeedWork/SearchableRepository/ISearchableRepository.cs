namespace ParkingLotAPP.Domain.SeedWork.SearchableRepository
{
    public interface ISearchableRepository<TEntity> where TEntity : Entity
    {
        Task<SearchOutput<TEntity>> Search(SearchInput input, CancellationToken cancellationToken);
    }
}

namespace ParkingLotAPP.Domain.SeedWork
{
    public interface IGenericRepository<TEntity> where TEntity : Entity
    {
        Task<List<TEntity>> GetAll(CancellationToken cancellationToken);

        Task<TEntity> GetById(Guid id, CancellationToken cancellationToken);

        Task Add(TEntity entity, CancellationToken cancellationToken);

        Task Update(TEntity entity);

        Task Delete(TEntity entity);
    }
}

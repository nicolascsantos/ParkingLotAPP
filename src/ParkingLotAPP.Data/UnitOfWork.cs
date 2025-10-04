using ParkingLotAPP.Application.Interfaces;

namespace ParkingLotAPP.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ConsoleDbContext _context;

        public UnitOfWork(ConsoleDbContext context)
        {
            _context = context;
        }

        public async Task Commit(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync();
        }

        public async Task Rollback(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

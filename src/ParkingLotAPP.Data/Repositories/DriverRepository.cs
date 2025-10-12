using Microsoft.EntityFrameworkCore;
using ParkingLotAPP.Application.Exceptions;
using ParkingLotAPP.Domain.Entities;
using ParkingLotAPP.Domain.Interfaces;
using ParkingLotAPP.Domain.SeedWork.SearchableRepository;

namespace ParkingLotAPP.Data.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        private readonly ConsoleDbContext _context;

        public DriverRepository(ConsoleDbContext context)
            => _context = context;

        public async Task Add(Driver entity, CancellationToken cancellationToken)
            => await _context.Drivers.AddAsync(entity, cancellationToken);

        public Task Delete(Driver entity)
            => Task.FromResult(_context.Drivers.Remove(entity));


        public async Task<List<Driver>> GetAll(CancellationToken cancellationToken)
            => await _context.Drivers
                .Include(x => x.Cars)
                .ToListAsync();

        public async Task<Driver> GetById(Guid id, CancellationToken cancellationToken)
        {
            var driver = await _context.Drivers.FindAsync(id);
            if (driver is null)
                throw new NotFoundException($"Driver '{id}' not found.");
            return driver;
        }

        public async Task<SearchOutput<Driver>> Search(SearchInput input, CancellationToken cancellationToken)
        {
            var toSkip = (input.Page - 1) * input.PerPage;
            var query = _context.Drivers
                .AsNoTracking()
                .AsQueryable();
            query = AddOrderToQuery(query, input.OrderBy, input.Order);

            if (!string.IsNullOrWhiteSpace(input.Search))
                query = query.Where(x => x.Name.Contains(input.Search));

            var total = await query.CountAsync();

            var drivers = await query
                .Include(x => x.Cars)
                .Skip(toSkip)
                .Take(input.PerPage)
                .ToListAsync(cancellationToken);
            return new SearchOutput<Driver>(input.Page, input.PerPage, total, drivers);
        }

        public Task Update(Driver entity)
            => Task.FromResult(_context.Drivers.Update(entity));
        

        private IQueryable<Driver> AddOrderToQuery(IQueryable<Driver> query, string orderProperty, SearchOrder order)
        => (orderProperty.ToLower(), order) switch
        {
            ("name", SearchOrder.ASC) => query.OrderBy(x => x.Name),
            ("name", SearchOrder.DESC) => query.OrderByDescending(x => x.Name),
            ("id", SearchOrder.ASC) => query.OrderBy(x => x.Id),
            ("id", SearchOrder.DESC) => query.OrderByDescending(x => x.Id),
            ("createdat", SearchOrder.ASC) => query.OrderBy(x => x.CreatedAt),
            ("createdat", SearchOrder.DESC) => query.OrderByDescending(x => x.CreatedAt),
            _ => query.OrderBy(x => x.Name)
        };
    }
}

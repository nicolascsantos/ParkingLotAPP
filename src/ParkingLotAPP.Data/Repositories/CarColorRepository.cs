using ParkingLotAPP.Application.Exceptions;
using ParkingLotAPP.Domain.Entities;
using ParkingLotAPP.Domain.Interfaces;
using ParkingLotAPP.Domain.SeedWork.SearchableRepository;
using Microsoft.EntityFrameworkCore;

namespace ParkingLotAPP.Data.Repositories
{
    public class CarColorRepository : ICarColorRepository
    {
        private readonly ConsoleDbContext _context;

        public CarColorRepository(ConsoleDbContext context)
        {
            _context = context;
        }

        public async Task Add(CarColor entity, CancellationToken cancellationToken)
            => await _context.CarColors.AddAsync(entity, cancellationToken);

        public Task Delete(CarColor entity)
            => Task.FromResult(_context.CarColors.Remove(entity));

        public async Task<List<CarColor>> GetAll(CancellationToken cancellationToken)
            => await _context.CarColors.ToListAsync(cancellationToken);

        public async Task<CarColor> GetById(Guid id, CancellationToken cancellationToken)
        {
            var carColor = await _context.CarColors
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return carColor ?? throw new NotFoundException($"Car color {id} not found.");
        }

        public async Task<SearchOutput<CarColor>> Search(SearchInput input, CancellationToken cancellationToken)
        {
            var toSkip = (input.Page - 1) * input.PerPage;
            var query = _context.CarColors
                .AsNoTracking()
                .AsQueryable();
            query = AddOrderToQuery(query, input.OrderBy, input.Order);

            if (!string.IsNullOrWhiteSpace(input.Search))
                query = query.Where(x => x.Name.Contains(input.Search));

            var total = await query.CountAsync();
            var carColors = await query
                .Skip(toSkip)
                .Take(input.PerPage)
                .ToListAsync(cancellationToken);
            return new SearchOutput<CarColor>(input.Page, input.PerPage, total, carColors);
        }

        public Task Update(CarColor entity)
            => Task.FromResult(_context.CarColors.Update(entity));

        public IQueryable<CarColor> AddOrderToQuery(IQueryable<CarColor> query, string orderProperty, SearchOrder order)
        => (orderProperty.ToLower(), order) switch
        {
            ("name", SearchOrder.ASC) => query.OrderBy(x => x.Name),
            ("name", SearchOrder.DESC) => query.OrderByDescending(x => x.Name),
            ("hex", SearchOrder.ASC) => query.OrderBy(x => x.Hex),
            ("hex", SearchOrder.DESC) => query.OrderByDescending(x => x.Hex),
            _ => query.OrderBy(x => x.Name)
        };
    }
}

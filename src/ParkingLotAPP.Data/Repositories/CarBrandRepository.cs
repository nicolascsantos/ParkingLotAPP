using ParkingLotAPP.Application.Exceptions;
using ParkingLotAPP.Domain.Entities;
using ParkingLotAPP.Domain.Interfaces;
using ParkingLotAPP.Domain.SeedWork.SearchableRepository;
using Microsoft.EntityFrameworkCore;

namespace ParkingLotAPP.Data.Repositories
{
    public class CarBrandRepository : ICarBrandRepository
    {
        private readonly ConsoleDbContext _context;

        public CarBrandRepository(ConsoleDbContext context)
        {
            _context = context;
        }

        public async Task Add(CarBrand entity, CancellationToken cancellationToken)
            => await _context.CarBrands.AddAsync(entity);


        public Task Delete(CarBrand entity)
            => Task.FromResult(_context.CarBrands.Remove(entity));

        public async Task<List<CarBrand>> GetAll(CancellationToken cancellationToken)
            => await _context.CarBrands.ToListAsync(cancellationToken);

        public async Task<CarBrand> GetById(Guid id, CancellationToken cancellationToken)
        {
            var carBrand = await _context.CarBrands
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return carBrand ?? throw new NotFoundException($"Car brand {id} not found.");
        }

        public async Task<SearchOutput<CarBrand>> Search(SearchInput input, CancellationToken cancellationToken)
        {
            var toSkip = (input.Page - 1) * input.PerPage;
            var query = _context.CarBrands
                .AsNoTracking()
                .AsQueryable();
            query = AddOrderToQuery(query, input.OrderBy, input.Order);

            if (!string.IsNullOrWhiteSpace(input.Search))
                query = query.Where(x => x.Name.Contains(input.Search));

            var total = await query.CountAsync();
            var carBrands = await query
                .Skip(toSkip)
                .Take(input.PerPage)
                .ToListAsync(cancellationToken);
            return new SearchOutput<CarBrand>(input.Page, input.PerPage, total, carBrands);
        }

        public Task Update(CarBrand entity)
            => Task.FromResult(_context.CarBrands.Update(entity));

        public IQueryable<CarBrand> AddOrderToQuery(IQueryable<CarBrand> query, string orderProperty, SearchOrder order)
       => (orderProperty.ToLower(), order) switch
       {
           ("name", SearchOrder.ASC) => query.OrderBy(x => x.Name),
           ("name", SearchOrder.DESC) => query.OrderByDescending(x => x.Name),
           _ => query.OrderBy(x => x.Name)
       };
    }
}

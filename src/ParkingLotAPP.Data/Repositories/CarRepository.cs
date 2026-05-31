using ParkingLotAPP.Application.Exceptions;
using ParkingLotAPP.Domain.Entities;
using ParkingLotAPP.Domain.Interfaces;
using ParkingLotAPP.Domain.SeedWork.SearchableRepository;
using Microsoft.EntityFrameworkCore;

namespace ParkingLotAPP.Data.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly ConsoleDbContext _dbContext;

        public CarRepository(ConsoleDbContext dbContext)
            => _dbContext = dbContext;


        public async Task Add(Car entity, CancellationToken cancellationToken)
            => await _dbContext.Cars.AddAsync(entity, cancellationToken);  
        

        public Task Delete(Car entity)
            => Task.FromResult(_dbContext.Cars.Remove(entity));
        

        public async Task<List<Car>> GetAll(CancellationToken cancellationToken)
            => await _dbContext.Cars.ToListAsync(cancellationToken);
        

        public async Task<Car> GetById(Guid id, CancellationToken cancellationToken)
        {
            var car = await _dbContext.Cars
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            return car ?? throw new NotFoundException($"Car '{id}' not found.");
        }

        public async Task<Car> GetByIdWithModelAndColor(Guid id, CancellationToken cancellationToken)
        {
            var car = await _dbContext.Cars
                .AsNoTracking()
                .Include(x => x.CarBrand)
                .Include(x => x.CarColor)
                .FirstOrDefaultAsync();

            return car ?? throw new NotFoundException($"Car '{id}' not found.");
        }

        public async Task<SearchOutput<Car>> Search(SearchInput input, CancellationToken cancellationToken)
        {
            var toSkip = (input.Page - 1) * input.PerPage;
            var query = _dbContext.Cars
                .AsNoTracking()
                .AsQueryable();    
            query = AddOrderToQuery(query, input.OrderBy, input.Order);

            if (!string.IsNullOrWhiteSpace(input.Search))
                query = query.Where(x => x.Name.Contains(input.Search));

            var total = await query.CountAsync();
            var cars = await query
                .Include(x => x.CarBrand)
                .Include(x => x.CarColor)
                .Include(x => x.Driver)
                .Skip(toSkip)
                .Take(input.PerPage)
                .ToListAsync(cancellationToken);
            return new SearchOutput<Car>(input.Page, input.PerPage, total, cars);
        }

        public Task Update(Car entity)
            => Task.FromResult(_dbContext.Cars.Update(entity));

        public IQueryable<Car> AddOrderToQuery(IQueryable<Car> query, string orderProperty, SearchOrder order)
        => (orderProperty.ToLower(), order) switch
        {
            ("name", SearchOrder.ASC) => query.OrderBy(x => x.Name),
            ("name", SearchOrder.DESC) => query.OrderByDescending(x => x.Name),
            ("year", SearchOrder.ASC) => query.OrderBy(x => x.Year),
            ("year", SearchOrder.DESC) => query.OrderByDescending(x => x.Year),
            ("modelyear", SearchOrder.ASC) => query.OrderBy(x => x.ModelYear),
            ("modelyear", SearchOrder.DESC) => query.OrderByDescending(x => x.ModelYear),
            ("carcolorid", SearchOrder.ASC) => query.OrderBy(x => x.CarColorId),
            ("carcolorid", SearchOrder.DESC) => query.OrderByDescending(x => x.CarColorId),
            ("carbrandid", SearchOrder.ASC) => query.OrderBy(x => x.CarBrandId),
            ("carbrandid", SearchOrder.DESC) => query.OrderByDescending(x => x.CarBrandId),
            _ => query.OrderBy(x => x.Name)
        };

        public async Task<IReadOnlyList<Guid>> GetIdsListByIds(List<Guid> list, CancellationToken cancellationToken)
            => await _dbContext.Cars.AsNoTracking().Where(car => list.Contains(car.Id)).Select(car => car.Id).ToListAsync(cancellationToken);

        public async Task<IReadOnlyList<Car>> GetListByIds(List<Guid> list, CancellationToken cancellationToken)
            => await _dbContext.Cars
                        .AsNoTracking()
                        .Where(car => list.Contains(car.Id))
                        .ToListAsync(cancellationToken);

        public async Task<Car> GetByIdWithModelAndColorAndDriver(Guid id, CancellationToken cancellationToken)
        {
            var car = await _dbContext.Cars
               .AsNoTracking()
               .Include(x => x.CarBrand)
               .Include(x => x.CarColor)
               .FirstOrDefaultAsync(cancellationToken);

            return car ?? throw new NotFoundException($"Car '{id}' not found.");
        }
    }
}

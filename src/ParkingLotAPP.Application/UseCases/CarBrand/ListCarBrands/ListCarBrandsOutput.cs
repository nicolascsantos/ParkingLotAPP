using ParkingLotAPP.Application.Common;
using ParkingLotAPP.Application.UseCases.CarBrand.Common;
using ParkingLotAPP.Domain.SeedWork.SearchableRepository;
using DomainEntity = ParkingLotAPP.Domain.Entities;

namespace ParkingLotAPP.Application.UseCases.CarBrand.ListCarBrands
{
    public class ListCarBrandsOutput : PaginatedListOutput<CarBrandModelOutput>
    {
        public ListCarBrandsOutput(
            int page,
            int perPage,
            int total,
            IReadOnlyList<CarBrandModelOutput> items
        ) : base(page, perPage, total, items)
        { }

        public static ListCarBrandsOutput FromSearchOutput(SearchOutput<DomainEntity.CarBrand> searchOutput)
            => new ListCarBrandsOutput(
                 page: searchOutput.CurrentPage,
                perPage: searchOutput.PerPage,
                total: searchOutput.Total,
                items: searchOutput.Items.Select(CarBrandModelOutput.FromCarBrand).ToList()
            );
    }
}

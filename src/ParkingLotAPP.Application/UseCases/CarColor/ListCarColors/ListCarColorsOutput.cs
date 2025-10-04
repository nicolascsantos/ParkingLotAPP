using ParkingLotAPP.Application.Common;
using ParkingLotAPP.Application.UseCases.CarColor.Common;
using ParkingLotAPP.Domain.SeedWork.SearchableRepository;
using DomainEntity = ParkingLotAPP.Domain.Entities;

namespace ParkingLotAPP.Application.UseCases.CarColor.ListCarColors
{
    public class ListCarColorsOutput : PaginatedListOutput<CarColorModelOutput>
    {
        public ListCarColorsOutput(int page,
            int perPage,
            int total,
            IReadOnlyList<CarColorModelOutput> items) : base(page, perPage, total, items)
        { }

        public static ListCarColorsOutput FromSearchOutput(SearchOutput<DomainEntity.CarColor> searchOutput)
            => new ListCarColorsOutput(
                page: searchOutput.CurrentPage,
                perPage: searchOutput.PerPage,
                total: searchOutput.Total,
                items: searchOutput.Items.Select(CarColorModelOutput.FromCarColor).ToList()
            );
    }
}

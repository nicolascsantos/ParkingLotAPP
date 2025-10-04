using ParkingLotAPP.Application.Common;
using ParkingLotAPP.Domain.SeedWork.SearchableRepository;
using MediatR;

namespace ParkingLotAPP.Application.UseCases.CarColor.ListCarColors
{
    public class ListCarColorsInput : PaginatedListInput, IRequest<ListCarColorsOutput>
    {
        public ListCarColorsInput(
            int page = 1,
            int perPage = 15,
            string search = "",
            string sort = "",
            SearchOrder dir = SearchOrder.ASC)
            : base(page, perPage, search, sort, dir)
        {}

        public ListCarColorsInput() : base(1, 15, "", "", SearchOrder.ASC)
        {}
    }
}

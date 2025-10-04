using ParkingLotAPP.Application.Common;
using ParkingLotAPP.Domain.SeedWork.SearchableRepository;
using MediatR;

namespace ParkingLotAPP.Application.UseCases.Car.ListCars
{
    public class ListCarsInput : PaginatedListInput, IRequest<ListCarsOutput>
    {
        public ListCarsInput(
            int page = 1,
            int perPage = 15,
            string search = "",
            string sort = "",
            SearchOrder dir = SearchOrder.ASC
        ) : base(page, perPage, search, sort, dir)
        {}

        public ListCarsInput() : base(1, 15, "", "", SearchOrder.ASC)
        {}
    }
}

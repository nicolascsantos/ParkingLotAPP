using ParkingLotAPP.Application.Common;
using ParkingLotAPP.Application.UseCases.Car.ListCars;
using ParkingLotAPP.Domain.SeedWork.SearchableRepository;
using MediatR;

namespace ParkingLotAPP.Application.UseCases.CarBrand.ListCarBrands
{
    public class ListCarBrandsInput : PaginatedListInput, IRequest<ListCarBrandsOutput>
    {
        public ListCarBrandsInput(
            int page = 1,
            int perPage = 15,
            string search = "",
            string sort = "",
            SearchOrder dir = SearchOrder.ASC
        ) : base(page, perPage, search, sort, dir) { }

        public ListCarBrandsInput() : base(1, 15, "", "", SearchOrder.ASC) { }
    }
}

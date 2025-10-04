using ParkingLotAPP.Application.Common;
using ParkingLotAPP.Application.UseCases.Car.Common;
using ParkingLotAPP.Domain.SeedWork.SearchableRepository;
using DomainEntity = ParkingLotAPP.Domain.Entities;

namespace ParkingLotAPP.Application.UseCases.Car.ListCars
{
    public class ListCarsOutput : PaginatedListOutput<CarModelOutput>
    {
        public ListCarsOutput(
            int page,
            int perPage,
            int total,
            IReadOnlyList<CarModelOutput> items
        ) : base(page, perPage, total, items)
        { }

        public static ListCarsOutput FromSearchOutput(SearchOutput<DomainEntity.Car> searchOutput)
            =>  new ListCarsOutput(
                page: searchOutput.CurrentPage,
                perPage: searchOutput.PerPage,
                total: searchOutput.Total,
                items: searchOutput.Items.Select(CarModelOutput.FromCar).ToList()
            );
    }
}

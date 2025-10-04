using MediatR;

namespace ParkingLotAPP.Application.UseCases.CarBrand.ListCarBrands
{
    public interface IListCarBrands : IRequestHandler<ListCarBrandsInput, ListCarBrandsOutput>
    {
    }
}

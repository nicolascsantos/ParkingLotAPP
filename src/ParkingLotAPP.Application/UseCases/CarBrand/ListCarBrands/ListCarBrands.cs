using ParkingLotAPP.Domain.Interfaces;

namespace ParkingLotAPP.Application.UseCases.CarBrand.ListCarBrands
{
    public class ListCarBrands : IListCarBrands
    {
        private readonly ICarBrandRepository _carBrandRepository;

        public ListCarBrands(ICarBrandRepository carBrandRepository)
            => _carBrandRepository = carBrandRepository;

        public async Task<ListCarBrandsOutput> Handle(ListCarBrandsInput request, CancellationToken cancellationToken)
        {
            var searchOutput = await _carBrandRepository
                .Search(request.ToSearchInput(), cancellationToken);

            ListCarBrandsOutput output = ListCarBrandsOutput.FromSearchOutput(searchOutput);

            return output;
        }
    }
}

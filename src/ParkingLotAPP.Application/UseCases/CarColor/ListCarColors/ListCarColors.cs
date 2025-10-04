using ParkingLotAPP.Domain.Interfaces;

namespace ParkingLotAPP.Application.UseCases.CarColor.ListCarColors
{
    public class ListCarColors : IListCarColors
    {
        private readonly ICarColorRepository _carColorRepository;

        public ListCarColors(ICarColorRepository carColorRepository)
            => _carColorRepository = carColorRepository;
        

        public async Task<ListCarColorsOutput> Handle(ListCarColorsInput request, CancellationToken cancellationToken)
        {
            var searchOutput = await _carColorRepository.Search(request.ToSearchInput(), cancellationToken);

            ListCarColorsOutput output = ListCarColorsOutput.FromSearchOutput(searchOutput);

            return output;
        }
    }
}

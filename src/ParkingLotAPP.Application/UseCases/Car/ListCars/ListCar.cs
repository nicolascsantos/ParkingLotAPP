using ParkingLotAPP.Domain.Interfaces;

namespace ParkingLotAPP.Application.UseCases.Car.ListCars
{
    public class ListCar : IListCars
    {
        private readonly ICarRepository _carRepository;

        public ListCar(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<ListCarsOutput> Handle(ListCarsInput request, CancellationToken cancellationToken)
        {
            var searchOutput = await _carRepository
                .Search(request.ToSearchInput(), cancellationToken);

            ListCarsOutput output = ListCarsOutput.FromSearchOutput(searchOutput);

            return output;
        }
    }
}

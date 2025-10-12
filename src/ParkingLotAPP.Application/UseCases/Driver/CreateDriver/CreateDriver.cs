using ParkingLotAPP.Application.Interfaces;
using ParkingLotAPP.Application.UseCases.Driver.Common;
using ParkingLotAPP.Domain.Interfaces;
using DomainEntity = ParkingLotAPP.Domain.Entities;

namespace ParkingLotAPP.Application.UseCases.Driver.CreateDriver
{
    public class CreateDriver : ICreateDriver
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateDriver(IDriverRepository driverRepository, IUnitOfWork unitOfWork)
        {
            _driverRepository = driverRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DriverModelOutput> Handle(CreateDriverInput request, CancellationToken ct)
        {
            var driver = new DomainEntity.Driver(
                request.Name,
                request.Document,
                request.ContractNumber,
                request.Email,
                request.PhoneNumber,
                request.IsActive
            );

            // Se vieram carros no input, crie entidades e associe ao driver
            if (request.Cars?.Any() == true)
            {
                foreach (var newCar in request.Cars)
                {
                    var car = new DomainEntity.Car(
                        newCar.Name,
                        newCar.Year,
                        newCar.ModelYear,
                        newCar.CarBrandId,
                        newCar.CarColorId,
                        newCar.Plate
                    );
                    car.Driver = driver;
                    driver.Cars.Add(car);
                }
            }

            await _driverRepository.Add(driver, ct);
            await _unitOfWork.Commit(ct);

            return DriverModelOutput.FromDriver(driver);
        }
    }
}

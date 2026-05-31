using ParkingLotAPP.Application.Interfaces;
using ParkingLotAPP.Application.UseCases.Driver.Common;
using ParkingLotAPP.Domain.Interfaces;
using ParkingLotAPP.Domain.Services;
using DomainEntity = ParkingLotAPP.Domain.Entities;

namespace ParkingLotAPP.Application.UseCases.Driver.CreateDriver
{
    public class CreateDriver : ICreateDriver
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IDriverCarAssignmentService _driverCarAssignmentService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateDriver(
            IDriverRepository driverRepository,
            IDriverCarAssignmentService driverCarAssignmentService,
            IUnitOfWork unitOfWork
        )
        {
            _driverRepository = driverRepository;
            _driverCarAssignmentService = driverCarAssignmentService;
            _unitOfWork = unitOfWork;
        }

        public async Task<DriverModelOutput> Handle(CreateDriverInput request, CancellationToken cancellationToken)
        {
            var driver = new DomainEntity.Driver(
                request.Name,
                request.Document,
                request.ContractNumber,
                request.Email,
                request.PhoneNumber,
                request.IsActive
            );

            if (request.Cars?.Any() == true)
            {
                var carsData = request.Cars.Select(c =>
                    (c.Name, c.Year, c.ModelYear, c.CarBrandId, c.CarColorId, c.Plate)
                );

                _driverCarAssignmentService.AssignCarsToDriver(driver, carsData);
            }

            await _driverRepository.Add(driver, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);

            return DriverModelOutput.FromDriver(driver);
        }
    }
}

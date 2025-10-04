using ParkingLotAPP.Application.UseCases.Car.CreateCar;
using ParkingLotAPP.Application.UseCases.CarColor.CreateCarColor;
using ParkingLotAPP.Application.UseCases.CarBrand.CreateCarBrand;

namespace ParkingLotAPP.API.Configurations
{
    public static class UseCasesConfiguration
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(CreateCar).Assembly));
            services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(CreateCarColor).Assembly));
            services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(CreateCarBrand).Assembly));
            return services;
        }
    }
}

using ParkingLotAPP.UnitTests.Base;
using DomainEntity = ParkingLotAPP.Domain.Entities;

namespace ParkingLotAPP.UnitTests.Domain.Entity.CarBrand
{
    [CollectionDefinition(nameof(CarBrandTestFixture))]
    public class CarBrandTestFixtureCollection : ICollectionFixture<CarBrandTestFixture> { }

    public class CarBrandTestFixture : BaseFixture
    {
        public DomainEntity.CarBrand GetValidCarBrand()
            => new DomainEntity.CarBrand(GetValidCarBrandName());

        public string GetValidCarBrandName()
            => Faker.Vehicle.Manufacturer();
    }
}

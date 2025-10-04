using ParkingLotAPP.UnitTests.Base;
using DomainEntity = ParkingLotAPP.Domain.Entities;

namespace ParkingLotAPP.UnitTests.Domain.Entity.CarColor
{
    [CollectionDefinition(nameof(CarColorTestFixture))]
    public class CarColorTestFixtureCollection : ICollectionFixture<CarColorTestFixture> { }

    public class CarColorTestFixture : BaseFixture
    {
        public DomainEntity.CarColor GetValidCarColor()
            => new DomainEntity.CarColor(
                GetValidCarColorName(),
                GetValidCarColorHex()
            );

        public string GetValidCarColorName()
            => Faker.Company.CompanyName();

        public string GetValidCarColorHex()
            => "#FF0000";

    }
}

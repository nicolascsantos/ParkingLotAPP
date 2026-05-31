using ParkingLotAPP.UnitTests.Base;
using DomainEntity = ParkingLotAPP.Domain.Entities;

namespace ParkingLotAPP.UnitTests.Domain.Entity.Car
{
    [CollectionDefinition(nameof(CarTestFixture))]
    public class CarTestFixtureCollection : ICollectionFixture<CarTestFixture> { }

    public class CarTestFixture : BaseFixture
    {
        public DomainEntity.Car GetValidCar()
            => new DomainEntity.Car(
                GetValidCarName(),
                GetValidYear(),
                GetValidModelYear(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                GetValidPlate(),
                Guid.NewGuid()
            );

        public string GetValidCarName()
            => Faker.Vehicle.Model();

        public int GetValidYear()
            => Faker.Random.Int(2000, 2026);

        public int GetValidModelYear()
            => Faker.Random.Int(2000, 2027);

        public string GetValidPlate()
            => $"{Faker.Random.String2(3, "ABCDEFGHIJKLMNOPQRSTUVWXYZ")}{Faker.Random.Int(1000, 9999)}";
    }
}

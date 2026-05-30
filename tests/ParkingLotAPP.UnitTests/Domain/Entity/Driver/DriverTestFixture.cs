using ParkingLotAPP.UnitTests.Base;
using DomainEntity = ParkingLotAPP.Domain.Entities;

namespace ParkingLotAPP.UnitTests.Domain.Entity.Driver
{
    [CollectionDefinition(nameof(DriverTestFixture))]
    public class DriverTestFixtureCollection : ICollectionFixture<DriverTestFixture> { }

    public class DriverTestFixture : BaseFixture
    {
        public DomainEntity.Driver GetValidDriver()
            => new DomainEntity.Driver(
                GetValidDriverName(),
                GetValidDocument(),
                GetValidContractNumber(),
                GetValidEmail(),
                GetValidPhoneNumber()
            );

        public string GetValidDriverName()
            => Faker.Name.FullName();

        public string GetValidDocument()
            => Faker.Random.ReplaceNumbers("###.###.###-##");

        public string GetValidContractNumber()
            => Faker.Random.ReplaceNumbers("C-####");

        public string GetValidEmail()
            => Faker.Internet.Email();

        public string GetValidPhoneNumber()
            => Faker.Phone.PhoneNumber("(##) #####-####");
    }
}

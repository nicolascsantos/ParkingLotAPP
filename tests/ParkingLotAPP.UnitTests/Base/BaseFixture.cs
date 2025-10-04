using Bogus;

namespace ParkingLotAPP.UnitTests.Base
{
    public class BaseFixture
    {
        public Faker Faker { get; set; }

        protected BaseFixture()
            => Faker = new Faker("pt_BR");
    }
}

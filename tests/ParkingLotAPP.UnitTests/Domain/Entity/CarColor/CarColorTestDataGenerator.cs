namespace ParkingLotAPP.UnitTests.Domain.Entity.CarColor
{
    public static class CarColorTestDataGenerator
    {
        public static IEnumerable<object[]> GetNamesWithLessThan3Characters(int numberOfTests = 6)
        {
            var fixture = new CarColorTestFixture();
            for (int i = 0; i < numberOfTests; i++)
            {
                var isOdd = i % 2 == 1;
                yield return new object[] { fixture.GetValidCarColorName()[..(isOdd ? 1 : 2)] };
            }
        }
    }
}

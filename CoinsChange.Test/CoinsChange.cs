namespace CoinsChange.Test
{
    using System.Collections.Generic;
    using Xunit;

    public class CoinsChange
    {

        [Fact]
        public void TestWithExampleFromGivenTask()
        {
            var typeOfCoins = new List<int> { 1, 2, 5, 10, 20, 50, 100, 200 };

            var taskExampleInput = 41;

            var expectedResult = 3;

            var actualResult =
                StartUp.MinCoinsCalculation(typeOfCoins, taskExampleInput);

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void TestWithOneAvailableCoinOfOneCent()
        {
            var typeOfCoins = new List<int> { 1 };

            var oneCentToReturn = 1;
            var zeroCentsToReturn = 0;
            var ninetyNineCentsToReturn = 99;

            var expectedCountWithOneCent = 1;
            var expectedCountWithZeroCent = 0;
            var expectedCountWithOneNinetyNineCents = 99;

            var resultCountWithOneCent =
                StartUp.MinCoinsCalculation(typeOfCoins,  oneCentToReturn);

            var resultCountWithZeroCent =
               StartUp.MinCoinsCalculation(typeOfCoins,  zeroCentsToReturn);

            var resultCountWithNinetyNineCents =
               StartUp.MinCoinsCalculation(typeOfCoins,  ninetyNineCentsToReturn);

            Assert.Equal(expectedCountWithOneCent, resultCountWithOneCent);
            Assert.Equal(expectedCountWithZeroCent, resultCountWithZeroCent);
            Assert.Equal(expectedCountWithOneNinetyNineCents, resultCountWithNinetyNineCents);
        }

    }
}

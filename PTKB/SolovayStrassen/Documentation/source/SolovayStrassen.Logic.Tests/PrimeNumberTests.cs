using Xunit;

namespace SolovayStrassen.Logic.Tests
{
    public class PrimeNumberTests : TestsBase
    {
        [Fact]
        public void test_for_5()
        {
            Execute("5", 100, true);
        }

        [Fact]
        public void test_for_274876858367()
        {
            Execute("274876858367", 1000, true);
        }

        [Fact]
        public void test_for_1125899839733759()
        {
            Execute("1125899839733759", 1000, true);
        }

        [Fact]
        public void test_for_18014398241046527()
        {
            Execute("18014398241046527", 1000, true);
        }

        [Fact]
        public void test_for_1298074214633706835075030044377087()
        {
            Execute("1298074214633706835075030044377087", 1000, true);
        }

        [Fact]
        public void test_for_35742549198872617291353508656626642567()
        {
            Execute("35742549198872617291353508656626642567", 1000, true);
        }
    }
}

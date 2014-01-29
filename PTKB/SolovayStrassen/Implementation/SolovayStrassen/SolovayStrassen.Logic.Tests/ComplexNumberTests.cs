using Xunit;

namespace SolovayStrassen.Logic.Tests
{
    public class ComplexNumberTests : TestsBase
    {
        [Fact]
        public void test_for_8()
        {
            Execute("8", 100, false);
        }

        [Fact]
        public void test_for_274846858363()
        {
            Execute("274846858363", 1000, false);
        }

        [Fact]
        public void test_for_1125844839733759()
        {
            Execute("1125844839733759", 1000, false);
        }

        [Fact]
        public void test_for_8014398241046527()
        {
            Execute("8014398241046527", 1000, false);
        }

        [Fact]
        public void test_for_1298374214633706835075030044377787()
        {
            Execute("1298374214633706835075030044377787", 1000, false);
        }

        [Fact]
        public void test_for_35747549198872617291353508656626642567()
        {
            Execute("35747549198872617291353508656626642567", 1000, false);
        }
    }
}

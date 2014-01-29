using System.Numerics;
using Xunit;

namespace SolovayStrassen.Logic.Tests
{
    public class TestsBase
    {
        public void Execute(string number, int iterations, bool expectedResult)
        {
            var result = SolovayStrassenAlgorithm.Execute(BigInteger.Parse(number), iterations);

            Assert.Equal(expectedResult, result);
            
        }
    }
}

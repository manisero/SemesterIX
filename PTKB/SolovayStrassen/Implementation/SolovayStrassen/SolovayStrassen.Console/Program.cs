using System.Diagnostics;
using System.Numerics;
using SolovayStrassen.Logic;

namespace SolovayStrassen.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = 0L;

            for (int i = 0; i < 1000; i++)
            {
                var stopwatch2 = new Stopwatch();
                stopwatch2.Start();
                SolovayStrassenAlgorithm.Execute(new BigInteger(16769023L), 100);
                stopwatch2.Stop();
                result += stopwatch2.ElapsedMilliseconds;
            }
        }
    }
}

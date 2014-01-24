using System.Diagnostics;
using System.Numerics;
using SolovayStrassen.Logic;

namespace SolovayStrassen.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            long result1 = 0;
            long result2 = 0;

            for (int i = 0; i < 1000; i++)
            {
                var stopwatch1 = new Stopwatch();
                stopwatch1.Start();
                var codeProjectResult = new Logic.CodeProject.BigInteger(16769023L).SolovayStrassenTest(100);
                stopwatch1.Stop();
                result1 += stopwatch1.ElapsedMilliseconds;

                var stopwatch2 = new Stopwatch();
                stopwatch2.Start();
                var myResult = SolovayStrassenAlgorithm.Execute(new BigInteger(16769023L), 100);
                stopwatch2.Stop();
                result2 += stopwatch2.ElapsedMilliseconds;
            }
        }
    }
}

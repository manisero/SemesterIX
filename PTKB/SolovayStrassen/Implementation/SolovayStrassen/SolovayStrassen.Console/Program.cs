using System.Numerics;
using SolovayStrassen.Logic;

namespace SolovayStrassen.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var codeProjectJacobi = Logic.CodeProject.BigInteger.Jacobi(256, 2079);

            var myJacobi = JacobiAlgorithm.Execute(256, 2079);

            var codeProjectResult = new Logic.CodeProject.BigInteger(16769023L).SolovayStrassenTest(100);

            var myResult = SolovayStrassenAlgorithm.Execute(new BigInteger(16769023L), 100);
        }
    }
}

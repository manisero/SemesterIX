using System;
using System.Numerics;
using SolovayStrassen.Logic;

namespace SolovayStrassen.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            BigInteger p;
            int iterations;

            if (!ParseArgs(args, out p, out iterations))
            {
                System.Console.WriteLine("Error");
            }

            System.Console.WriteLine("Executing Solovay-Strassen test");
            System.Console.WriteLine("p = {0}", p);
            System.Console.WriteLine("iterations: {0}", iterations);
            System.Console.WriteLine();

            var result = SolovayStrassenAlgorithm.Execute(p, iterations);

            if (result)
            {
                System.Console.WriteLine("p is probably primal");
            }
            else
            {
                System.Console.WriteLine("p is not primal");
            }
        }

        private static bool ParseArgs(string[] args, out BigInteger p, out int iterations)
        {
            if (args.Length != 2)
            {
                p = 0;
                iterations = 0;
                return false;
            }

            try
            {
                p = BigInteger.Parse(args[0]);
            }
            catch (Exception)
            {
                p = 0;
                iterations = 0;
                return false;
            }

            if (!int.TryParse(args[1], out iterations))
            {
                p = 0;
                iterations = 0;
                return false;
            }

            return true;
        }
    }
}

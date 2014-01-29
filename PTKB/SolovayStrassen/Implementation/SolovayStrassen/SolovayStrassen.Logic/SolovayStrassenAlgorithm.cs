using System;
using System.Numerics;

namespace SolovayStrassen.Logic
{
    public static class SolovayStrassenAlgorithm
    {
        public static bool Execute(BigInteger p, int iterations)
        {
            // Check for small numbers
            if (p.IsZero || p.IsOne)
            {
                return false;
            }

            if (p == 2 || p == 3)
            {
                return true;
            }

            // Check for even numbers
            if (p.IsEven)
            {
                return false;
            }

            // Perform primality test
            var numberBytesCount = p.ToByteArray().Length;
            var random = new Random();
            var pSub1 = p - 1;
            var pSub1Div2 = pSub1 >> 1;

            for (int i = 0; i < iterations; i++)
            {
                // Generate random a < p
                var a = GenerateA(p, numberBytesCount, random);

                // Calculate Jacobi symbol
                var jacobi = JacobiAlgorithm.Execute(a, p);

                if (jacobi == 0)
                {
                    return false;
                }

                // Calculate a^((p-1)/2) mod p
                var exponentResult = BigInteger.ModPow(a, pSub1Div2, p);

                if (exponentResult == pSub1)
                {
                    exponentResult = BigInteger.MinusOne;
                }

                if (jacobi != exponentResult)
                {
                    return false;
                }
            }

            return true;
        }

        private static BigInteger GenerateA(BigInteger number, int numberBytesCount, Random random)
        {
            BigInteger a;

            do
            {
                var bytes = new byte[numberBytesCount];
                random.NextBytes(bytes);

                a = new BigInteger(bytes);

                if (a.Sign < 0)
                {
                    a = -a;
                }
            } while (a.IsZero ||  a.IsOne || a >= number);

            return a;
        }
    }
}

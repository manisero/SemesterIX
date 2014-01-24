using System;
using System.Numerics;

namespace SolovayStrassen.Logic
{
    public class Algorithm
    {
        public bool SolovayStrassenTest(BigInteger p, int confidence)
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

            for (int round = 0; round < confidence; round++)
            {
                var a = GenerateA(p, numberBytesCount, random);

                // Check whether a factor exists
                var gcd = BigInteger.GreatestCommonDivisor(p, a);

                if (!gcd.IsOne)
                {
                    return false;
                }

                // Calculate a^((p-1)/2) mod p
                var exponentResult = BigInteger.ModPow(a, pSub1Div2, p);

                if (exponentResult == pSub1)
                {
                    exponentResult = BigInteger.MinusOne;
                }

                // Calculate Jacobi symbol
                var jacobi = JacobiAlgorithm.Jacobi(a, p);

                //Console.WriteLine("a = " + a.ToString(10) + " b = " + thisVal.ToString(10));
                //Console.WriteLine("expResult = " + expResult.ToString(10) + " Jacob = " + jacob.ToString(10));

                if (exponentResult != jacobi)
                {
                    return false;
                }
            }

            return true;
        }

        private BigInteger GenerateA(BigInteger number, int numberBytesCount, Random random)
        {
            BigInteger a;

            do
            {
                var bytes = new byte[numberBytesCount];
                random.NextBytes(bytes);

                a = new BigInteger(bytes);
            } while (a >= number);

            return a;
        }
    }
}

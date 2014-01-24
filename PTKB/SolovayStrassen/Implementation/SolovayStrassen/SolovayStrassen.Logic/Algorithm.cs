using System;
using System.Numerics;

namespace SolovayStrassen.Logic
{
    public class Algorithm
    {
        public bool SolovayStrassenTest(BigInteger number, int confidence)
        {
            // Check for small numbers
            if (number.IsZero || number.IsOne)
            {
                return false;
            }

            if (number == 2 || number == 3)
            {
                return true;
            }

            // Check for even numbers
            if (number.IsEven)
            {
                return false;
            }

            // Perform primality test
            var numberBytesCount = number.ToByteArray().Length;
            var random = new Random();

            // TODO:
            BigInteger p_sub1 = number - 1;
            BigInteger p_sub1_shift = p_sub1 >> 1;

            for (int round = 0; round < confidence; round++)
            {
                var a = GenerateA(number, numberBytesCount, random);

                // Check whether a factor exists
                var gcd = BigInteger.GreatestCommonDivisor(number, a);

                if (!gcd.IsOne)
                {
                    return false;
                }

                // calculate a^((p-1)/2) mod p
                BigInteger expResult = a.modPow(p_sub1_shift, number);

                if (expResult == p_sub1)
                {
                    expResult = -1;
                }

                // calculate Jacobi symbol
                BigInteger jacob = Jacobi(a, number);

                //Console.WriteLine("a = " + a.ToString(10) + " b = " + thisVal.ToString(10));
                //Console.WriteLine("expResult = " + expResult.ToString(10) + " Jacob = " + jacob.ToString(10));

                // if they are different then it is not prime
                if (expResult != jacob)
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

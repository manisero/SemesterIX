using System;
using System.Numerics;

namespace SolovayStrassen.Logic
{
    public class Algorithm
    {
        public bool SolovayStrassenTest(BigInteger thisVal, int confidence)
        {
            // Check for small numbers
            if (thisVal.IsZero || thisVal.IsOne)
            {
                return false;
            }

            if (thisVal == 2 || thisVal == 3)
            {
                return true;
            }

            // Check for even numbers
            if (thisVal.IsEven)
            {
                return false;
            }

            // TODO:
            int bits = thisVal.bitCount();
            BigInteger a = new BigInteger();
            BigInteger p_sub1 = thisVal - 1;
            BigInteger p_sub1_shift = p_sub1 >> 1;

            Random rand = new Random();

            for (int round = 0; round < confidence; round++)
            {
                bool done = false;

                while (!done)		// generate a < n
                {
                    int testBits = 0;

                    // make sure "a" has at least 2 bits
                    while (testBits < 2)
                        testBits = (int)(rand.NextDouble() * bits);

                    a.genRandomBits(testBits, rand);

                    int byteLen = a.dataLength;

                    // make sure "a" is not 0
                    if (byteLen > 1 || (byteLen == 1 && a.data[0] != 1))
                        done = true;
                }

                // check whether a factor exists (fix for version 1.03)
                BigInteger gcdTest = a.gcd(thisVal);
                if (gcdTest.dataLength == 1 && gcdTest.data[0] != 1)
                    return false;

                // calculate a^((p-1)/2) mod p

                BigInteger expResult = a.modPow(p_sub1_shift, thisVal);
                if (expResult == p_sub1)
                    expResult = -1;

                // calculate Jacobi symbol
                BigInteger jacob = Jacobi(a, thisVal);

                //Console.WriteLine("a = " + a.ToString(10) + " b = " + thisVal.ToString(10));
                //Console.WriteLine("expResult = " + expResult.ToString(10) + " Jacob = " + jacob.ToString(10));

                // if they are different then it is not prime
                if (expResult != jacob)
                    return false;
            }

            return true;
        }
    }
}

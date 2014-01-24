using System;
using System.Numerics;

namespace SolovayStrassen.Logic
{
    public static class JacobiAlgorithm
    {
        public static int Execute(BigInteger a, BigInteger b)
        {
            if (a.IsEven || b.IsEven)
            {
                throw new ArgumentException("Jacobi is defined for odd numbers only");
            }

            if (a >= b)
            {
                a %= b;
            }

            if (a.IsZero)
            {
                return 0;
            }

            if (a.IsOne)
            {
                return 1;
            }

            if (a < 0)
            {
                if (((b - 1) >> 1).IsEven)
                {
                    return Execute(-a, b);
                }
                else
                {
                    return -Execute(-a, b);
                }
            }

            var e = GenerateE(a.ToByteArray());
            var aDivE = a >> e;
            var s = 1;

            var bFirstByte = b.ToByteArray()[0];
            var aDivEBytes = aDivE.ToByteArray();

            if ((e & 0x1) != 0 && ((bFirstByte & 0x7) == 3 || (bFirstByte & 0x7) == 5))
            {
                s = -1;
            }

            if ((bFirstByte & 0x3) == 3 && (aDivEBytes[0] & 0x3) == 3)
            {
                s = -s;
            }

            if (aDivEBytes.Length == 1 && aDivEBytes[0] == 1)
            {
                return s;
            }
            else
            {
                return (s * Execute(b % aDivE, aDivE));
            }
        }

        private static int GenerateE(byte[] aBytes)
        {
            var e = 0;

            for (var index = 0; index < aBytes.Length; index++)
            {
                uint mask = 0x01;

                for (int i = 0; i < 32; i++)
                {
                    if ((aBytes[index] & mask) != 0)
                    {
                        return e;
                    }

                    mask <<= 1;
                    e++;
                }
            }

            return e;
        }
    }
}

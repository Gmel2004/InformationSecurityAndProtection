using System.Numerics;

namespace WindowsFormsApp1
{
    internal class RSA
    {
        public BigInteger e { get; private set; }
        private BigInteger d { get; set; }
        public BigInteger n { get; private set; }

        public RSA(int bitLength)
        {
            BigInteger p = BigIntegerHelper.GeneratePrime(bitLength);
            BigInteger q = BigIntegerHelper.GeneratePrime(bitLength);
            n = p * q;
            BigInteger f = (p - 1) * (q - 1);
            e = CalculateE(f);
            d = CalculateD(e, f);
        }

        private BigInteger CalculateE(BigInteger f)
        {
            BigInteger e = 3;

            while (BigInteger.GreatestCommonDivisor(e, f) != 1)
            {
                e += 2;
            }

            return e;
        }

        private BigInteger CalculateD(BigInteger e, BigInteger f)
        {
            BigInteger y = 0, d = 1;
            BigInteger fOriginal = f;
            BigInteger temp, quotient;

            while (e > 1)
            {
                quotient = e / f;
                temp = f;

                f = e % f;
                e = temp;
                temp = y;

                y = d - quotient * y;
                d = temp;
            }

            if (d < 0)
                d += fOriginal;

            return d;
        }

        public BigInteger Encrypt(BigInteger messageData)
        {
            return BigInteger.ModPow(messageData, e, n);
        }

        public BigInteger Decrypt(BigInteger cipher)
        {
            return BigInteger.ModPow(cipher, d, n);
        }
    }
}

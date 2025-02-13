using System;
using System.Numerics;

namespace WindowsFormsApp1
{
    internal class BigIntegerHelper
    {
        public static BigInteger Sqrt(BigInteger number)
        {
            if (number < 0)
            {
                throw new ArgumentException("Number cannot be negative");
            }

            if (number == 0 || number == 1)
            {
                return number;
            }

            BigInteger x0 = number / 2;
            BigInteger x1 = (x0 + number / x0) / 2;

            while (x1 < x0)
            {
                x0 = x1;
                x1 = (x0 + number / x0) / 2;
            }

            return x0;
        }

        public static bool IsPrime(BigInteger number)
        {
            if (number < 2)
            {
                return false;
            }

            BigInteger sqrtNumber = Sqrt(number);
            for (BigInteger i = 2; i <= sqrtNumber; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static BigInteger GeneratePrime(int bitLength)
        {
            Random rnd = new Random();
            BigInteger number = BigInteger.One << (bitLength - 1);
            do
            {
                number |= BigInteger.Abs(new BigInteger(rnd.Next())) | 1;
            } while (!IsPrime(number));

            return number;
        }
    }
}

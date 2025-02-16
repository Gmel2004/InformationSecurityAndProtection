using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Authentication.ExtendedProtection;

namespace WindowsFormsApp1
{
    internal class BigIntegerHelper
    {
        Random rnd = new Random();

        private BigInteger ExtendedGreatestCommonDivisor(
            BigInteger x, BigInteger y, ref BigInteger a, ref BigInteger b
            )
        {
            if (y == 0 || x == 0)
            {
                a = y == 0 ? 1 : 0;
                b = 1 - a;
                return y + x;
            }

            BigInteger gcd = 0;
            if (y < x)
            {
                gcd = ExtendedGreatestCommonDivisor(x % y, y, ref a, ref b);
                b -= x / y * a;
            }
            else
            {
                gcd = ExtendedGreatestCommonDivisor(x, y % x, ref a, ref b);
                a -= y / x * b;
            }

            return gcd;
        }

        public BigInteger Sqrt(BigInteger number)
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

        public bool IsPrime(BigInteger number)
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

        public BigInteger GeneratePrime(int bitLength)
        {
            BigInteger number = BigInteger.One << (bitLength - 1);
            do
            {
                number |= BigInteger.Abs(new BigInteger(rnd.Next())) | 1;
            } while (!IsPrime(number));

            return number;
        }

        public BigInteger CalculateModInverse(BigInteger number, BigInteger mod)
        {
            BigInteger a = 0, b = 0;
            BigInteger gcd = ExtendedGreatestCommonDivisor(number, mod, ref a, ref b);

            if (gcd != 1)
            {
                throw new ArgumentException("Error! Numbers are not coprime!");
            }

            return a < 0 ? a + mod : a;
        }

        public BigInteger CalculateCoprimeNumber(BigInteger f, int bitLenght)
        {
            BigInteger otherNumber = GeneratePrime(bitLenght);

            while
                (
                !IsPrime(otherNumber) ||
                BigInteger.GreatestCommonDivisor(otherNumber, f) != 1
                )
            {
                otherNumber += 2;
            }

            return otherNumber;
        }
    }

}

using System;
using System.Collections.Generic;
using System.Numerics;

namespace WindowsFormsApp1
{
    internal class BigIntegerHelper
    {
        private Random rnd = new Random();

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

        private static bool IsPrime(BigInteger n)
        {
            if (n < 2) return false;
            if (n == 2 || n == 3) return true;
            if (n % 2 == 0) return false;

            BigInteger d = n - 1;
            int s = 0;
            while (d % 2 == 0)
            {
                d /= 2;
                s++;
            }

            int[] bases = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37 };

            foreach (int a in bases)
            {
                if (a >= n) break;

                if (!MillerTest(n, d, s, a))
                    return false;
            }

            return true;
        }

        private static bool MillerTest(BigInteger n, BigInteger d, int s, int a)
        {
            BigInteger x = BigInteger.ModPow(a, d, n);

            if (x == 1 || x == n - 1)
                return true;

            for (int r = 1; r < s; r++)
            {
                x = x * x % n;
                if (x == n - 1)
                    return true;
            }

            return false;
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

        public BigInteger GeneratePrimitiveRoot(BigInteger value, int bitLength)
        {
            if (value < 2)
                throw new ArgumentException("value must be simple and more than 2");

            BigInteger phi = value - 1;

            List<BigInteger> factors = new List<BigInteger>();
            BigInteger n = phi;
            for (BigInteger i = 2; i * i <= n; i++)
            {
                if (n % i == 0)
                {
                    factors.Add(i);
                    while (n % i == 0)
                        n /= i;
                }
            }
            if (n > 1) factors.Add(n);

            Random rnd = new Random();
            while (true)
            {
                BigInteger g = GeneratePrime(bitLength) % (value - 1);

                if (g < 2) continue;

                bool isPrimitiveRoot = true;
                foreach (var factor in factors)
                {
                    if (BigInteger.ModPow(g, phi / factor, value) == 1)
                    {
                        isPrimitiveRoot = false;
                        break;
                    }
                }

                if (isPrimitiveRoot)
                    return g;
            }
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

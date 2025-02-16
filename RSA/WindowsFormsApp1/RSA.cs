using System;
using System.Collections.Generic;
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
            BigIntegerHelper biH = new BigIntegerHelper();
            BigInteger p = biH.GeneratePrime(bitLength);
            BigInteger q;

            do
            {
                q = biH.GeneratePrime(bitLength);
            } while (q == p);

            n = p * q;
            BigInteger f = (p - 1) * (q - 1);
            e = biH.CalculateCoprimeNumber(f, bitLength);
            d = biH.CalculateModInverse(e, f);

            #if DEBUG
            Console.Clear();
            Console.WriteLine(p);
            Console.WriteLine(q);
            Console.WriteLine(n);
            Console.WriteLine(f);
            Console.WriteLine(e);
            Console.WriteLine(d);
            #endif
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

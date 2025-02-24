using System;
using System.Linq;
using System.Numerics;
using System.Text;

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
            Console.WriteLine($"p = {p}");
            Console.WriteLine($"q = {q}");
            Console.WriteLine($"n = {n}");
            Console.WriteLine($"f = {f}");
            Console.WriteLine($"e = {e}");
            Console.WriteLine($"d = {d}");
#endif
        }

        public string Encrypt(string text)
        {
            if (text.Length == 0)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();
            byte[] textData = Encoding.ASCII.GetBytes(text);
            int maxBlockSize = n.ToByteArray().Length - 1;

            int index = 0;
            while (index < textData.Length)
            {
                int blockSize = Math.Min(maxBlockSize, textData.Length - index);
                var block = textData.Skip(index).Take(blockSize).ToArray();
                Array.Reverse(block);

                BigInteger biFromBlock = new BigInteger(block);
                sb.Append($" {BigInteger.ModPow(biFromBlock, e, n)}");

                index += blockSize;
            }


            return sb.ToString(1, sb.Length - 1);
        }

        public string Decrypt(string text)
        {
            if (text.Length == 0)
            {
                return string.Empty;
            }

            try
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in text.Split().Select(t => BigInteger.Parse(t)))
                {
                    var bytes = BigInteger.ModPow(item, d, n).ToByteArray();
                    Array.Reverse(bytes);

                    sb.Append(Encoding.ASCII.GetString(bytes));
                }

                return sb.ToString();
            }
            catch
            {
                throw new Exception("Error. The text is not decipherable");
            }
        }
    }
}

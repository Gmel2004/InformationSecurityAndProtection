using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace WindowsFormsApp1
{
    internal class ELGamal
    {
        private BigInteger x;
        private BigIntegerHelper biH = new BigIntegerHelper();

        public BigInteger p;
        public BigInteger g;
        public BigInteger y;

        public int PBitLength { get; private set; } = 20;
        public int KBitLength { get; private set; } = 20;
        public int XBitLength { get; private set; } = 12;
        public int GBitLength { get; private set; } = 12;


        public ELGamal()
        {
            p = biH.GeneratePrime(PBitLength);
            g = biH.GeneratePrimitiveRoot(p, KBitLength);
            x = biH.GeneratePrime(XBitLength);
            y = BigInteger.ModPow(g, x, p);

#if DEBUG
            Console.Clear();
            Console.WriteLine($"p = {p}");
            Console.WriteLine($"g = {g}");
            Console.WriteLine($"x = {x}");
            Console.WriteLine($"y = {y}");
#endif
        }

        public List<Message> GenerateMessages(string text)
        {
            if (text.Length == 0)
            {
                return new List<Message>();
            }
            
            byte[] textData = Encoding.UTF8.GetBytes(text);
            int maxBlockSize = p.ToByteArray().Length - 1;
            List<Message> messages = new List<Message>();

            int index = 0;
            while (index < textData.Length)
            {
                int blockSize = Math.Min(maxBlockSize, textData.Length - index);
                var block = textData.Skip(index).Take(blockSize).ToArray();
                Array.Reverse(block);
                block = block.Concat(new byte[] { 0 }).ToArray();

                BigInteger hash = new BigInteger(block);
                BigInteger k = biH.CalculateCoprimeNumber(p - 1, KBitLength);
                BigInteger r = BigInteger.ModPow(g, k, p);
                BigInteger kInverse = biH.CalculateModInverse(k, p - 1);
                //BigInteger s = kInverse * (hash - x * r) % (p - 1);
                //s = (s + (p - 1)) % (p - 1);
                BigInteger s = kInverse * ((hash - x * r) % (p - 1) + (p - 1)) % (p - 1);
                messages.Add(new Message(hash, r, s));

                index += blockSize;
            }


            return messages;
        }

        public bool CheckMessages(List<Message> messages)
        {
            if (messages.Count == 0)
            {
                return true;
            }

            try
            {
#if DEBUG
                Console.Clear();
                Console.WriteLine($"p = {p}");
                Console.WriteLine($"g = {g}");
                Console.WriteLine($"x = {x}");
                Console.WriteLine($"y = {y}");
                Console.WriteLine();
                Console.WriteLine("y^r * r^s == g^m mod p");
#endif

                bool isMatches = true;
                for (int i = 0; isMatches && i < messages.Count; i++)
                {
                    BigInteger leftPart = BigInteger.ModPow(y, messages[i].r, p) % p *
                       BigInteger.ModPow(messages[i].r, messages[i].s, p) % p;

                    BigInteger rightPart = BigInteger.ModPow(g, messages[i].hash, p);

                    isMatches = leftPart == rightPart;

#if DEBUG
                    Console.WriteLine($"{leftPart} == {rightPart}");
#endif
                }

                return isMatches;
            }
            catch
            {
                throw new Exception("Error. The text is not decipherable");
            }
        }
    }
}

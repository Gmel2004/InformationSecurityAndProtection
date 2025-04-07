using System;
using System.Collections.Generic;
using System.Linq;
using WindowsFormsApp1;

namespace LZWCompressor
{
    public class LZW
    {
        private const int MaxBits = 12;
        private const int MaxCountCodes = 1 << MaxBits;
        private const int BitsInByte = 8;
        private const int ASCIICountCodes = 256;

        public byte[] Compress(InputData input)
        {
            var dictionary =
                new Dictionary<(int prefixCode, byte next), int>(MaxCountCodes);

            for (int i = 0; i < ASCIICountCodes; i++)
            {
                dictionary.Add((-1, (byte)i), i);
            }

            int currentPrefixCode = -1;
            var outputCodes = new List<int>();

            while (input.CurrentIndex < input.Length)
            {
                byte nextByte = input.Data[input.CurrentIndex++];
                var key = (currentPrefixCode, nextByte);

                if (dictionary.TryGetValue(key, out int code))
                {
                    currentPrefixCode = code;
                    continue;
                }

                outputCodes.Add(currentPrefixCode);
                if (dictionary.Count < MaxCountCodes)
                {
                    dictionary.Add(key, dictionary.Count);
                }
                currentPrefixCode = nextByte;
            }

            outputCodes.Add(currentPrefixCode);
            return PackCodes(outputCodes);
        }

        public byte[] Decompress(OutData outData)
        {
            var dictionary = new Dictionary<int, (byte[] data, byte first)>();

            for (int i = 0; i < 256; i++)
            {
                dictionary[i] = (new[] { (byte)i }, (byte)i);
            }

            var output = new List<byte>(outData.Length * 2);
            int prevCode = outData.Data[0];
            outData.CurrentIndex = 1;

            if (dictionary.TryGetValue(prevCode, out var entry))
            {
                output.AddRange(entry.data);
            }

            while (outData.CurrentIndex < outData.Length)
            {
                int currCode = outData.Data[outData.CurrentIndex++];

                byte[] decoded;
                byte firstByte;

                if (dictionary.TryGetValue(currCode, out var currEntry))
                {
                    decoded = currEntry.data;
                    firstByte = currEntry.first;
                }
                else if (currCode == dictionary.Count)
                {
                    firstByte = dictionary[prevCode].first;
                    decoded = dictionary[prevCode].data.Append(firstByte).ToArray();
                }
                else
                {
                    throw new InvalidOperationException($"Invalid code: {currCode}");
                }

                output.AddRange(decoded);
                var prevEntry = dictionary[prevCode];
                var newData = prevEntry.data.Append(firstByte).ToArray();
                dictionary[dictionary.Count] = (newData, newData[0]);

                prevCode = currCode;
            }

            return output.ToArray();
        }

        private byte[] PackCodes(List<int> codes)
        {
            int bitBuffer = 0;
            int bitsInBuffer = 0;
            var output = new List<byte>();

            foreach (int code in codes)
            {
                bitBuffer |= code << bitsInBuffer;
                bitsInBuffer += MaxBits;

                while (bitsInBuffer >= BitsInByte)
                {
                    output.Add((byte)(bitBuffer & 0xFF));
                    bitBuffer >>= BitsInByte;
                    bitsInBuffer -= BitsInByte;
                }
            }

            if (bitsInBuffer > 0)
            {
                output.Add((byte)bitBuffer);
            }

            return output.ToArray();
        }
    }
}
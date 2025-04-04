using System;
using System.Collections.Generic;
using System.Linq;
using WindowsFormsApp1;

namespace LZWCompressor
{
    public static class LZW
    {
        private const int MaxBits = 12;
        private const int MaxCode = (1 << MaxBits) - 1;

        public static byte[] Compress(InputData input)
        {
            var dictionary = new Dictionary<(int prefix, byte next), int>(4096);

            // Инициализация словаря для отдельных байтов
            for (int i = 0; i < 256; i++)
                dictionary.Add((-1, (byte)i), i);

            int currentPrefix = -1;
            var outputCodes = new List<int>();

            while (input.CurrentIndex < input.Length)
            {
                byte nextByte = input.Data[input.CurrentIndex];
                input.CurrentIndex++;

                var key = (currentPrefix, nextByte);

                if (dictionary.TryGetValue(key, out int code))
                {
                    currentPrefix = code;
                }
                else
                {
                    outputCodes.Add(currentPrefix);

                    if (dictionary.Count < MaxCode)
                        dictionary.Add(key, dictionary.Count);

                    currentPrefix = nextByte;
                }
            }

            outputCodes.Add(currentPrefix);
            return PackCodes(outputCodes);
        }

        public static byte[] Decompress(OutData outData)
        {
            var dictionary = new Dictionary<int, (byte[] data, byte first)>();

            // Инициализация базового словаря
            for (int i = 0; i < 256; i++)
                dictionary[i] = (new[] { (byte)i }, (byte)i);

            var output = new List<byte>(outData.Length * 2);
            int prevCode = outData.Data[0];
            outData.CurrentIndex = 1;

            if (dictionary.TryGetValue(prevCode, out var entry))
                output.AddRange(entry.data);

            while (outData.CurrentIndex < outData.Length)
            {
                int currCode = outData.Data[outData.CurrentIndex];
                outData.CurrentIndex++;

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

                // Добавляем новую комбинацию в словарь
                var prevEntry = dictionary[prevCode];
                var newData = prevEntry.data.Append(firstByte).ToArray();
                dictionary[dictionary.Count] = (newData, newData[0]);

                prevCode = currCode;
            }

            return output.ToArray();
        }

        private static byte[] PackCodes(List<int> codes)
        {
            int bitBuffer = 0;
            int bitsInBuffer = 0;
            var output = new List<byte>();

            foreach (int code in codes)
            {
                bitBuffer |= code << bitsInBuffer;
                bitsInBuffer += MaxBits;

                while (bitsInBuffer >= 8)
                {
                    output.Add((byte)(bitBuffer & 0xFF));
                    bitBuffer >>= 8;
                    bitsInBuffer -= 8;
                }
            }

            if (bitsInBuffer > 0)
                output.Add((byte)bitBuffer);

            return output.ToArray();
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WindowsFormsApp1;

namespace LZWCompressor
{
    public static class LZW
    {
        public static byte[] Compress(InputData input)
        {
            var dictionary = new Dictionary<Sequence, int>(new SequenceComparer());
            for (int i = 0; i < 256; i++)
            {
                dictionary.Add(new Sequence(new byte[] { (byte)i }), i);
            }

            Sequence current = new Sequence(new byte[0]);
            List<int> output = new List<int>();

            while (input.CurrentIndex < input.Length)
            {
                byte nextByte = input.Data[input.CurrentIndex];
                input.CurrentIndex++;

                Sequence next = current.Append(nextByte);
                if (dictionary.ContainsKey(next))
                {
                    current = next;
                }
                else
                {
                    output.Add(dictionary[current]);
                    dictionary.Add(next, dictionary.Count);
                    current = new Sequence(new byte[] { nextByte });
                }
            }

            output.Add(dictionary[current]);
            return IntegerToBytes(output);
        }

        public static byte[] Decompress(OutData outData)
        {
            var dictionary = new Dictionary<int, List<byte>>();
            // Инициализация словаря: коды 0-255 соответствуют байтам 0x00-0xFF
            for (int i = 0; i < 256; i++)
            {
                dictionary.Add(i, new List<byte> { (byte)i });
            }

            List<byte> output = new List<byte>();
            int prevCode = outData.Data[0];
            output.AddRange(dictionary[prevCode]);
            outData.CurrentIndex = 1; // Прогресс декомпрессии

            for (int i = 1; i < outData.Data.Count; i++)
            {
                int currentCode = outData.Data[i];
                List<byte> entry;

                if (dictionary.ContainsKey(currentCode))
                {
                    entry = dictionary[currentCode];
                }
                else if (currentCode == dictionary.Count)
                {
                    // Специальный случай: prevEntry + первый байт prevEntry
                    entry = new List<byte>(dictionary[prevCode]);
                    entry.Add(dictionary[prevCode][0]);
                }
                else
                {
                    throw new InvalidDataException($"Некорректный код: {currentCode}.");
                }

                output.AddRange(entry);

                // Добавляем новую последовательность в словарь: prevEntry + первый байт entry
                List<byte> newEntry = new List<byte>(dictionary[prevCode]);
                newEntry.Add(entry[0]);
                dictionary.Add(dictionary.Count, newEntry);

                prevCode = currentCode;
                outData.CurrentIndex++; // Обновление прогресса
            }

            return output.ToArray();
        }

        private static byte[] IntegerToBytes(List<int> input)
        {
            byte[] bytes = new byte[input.Count * 4];
            for (int i = 0; i < input.Count; i++)
            {
                byte[] intBytes = BitConverter.GetBytes(input[i]);
                Buffer.BlockCopy(intBytes, 0, bytes, i * 4, 4);
            }
            return bytes;
        }
    }

    public class Sequence
    {
        public byte[] Data { get; }

        public Sequence(byte[] data)
        {
            Data = data;
        }

        public Sequence Append(byte b)
        {
            byte[] newData = new byte[Data.Length + 1];
            Data.CopyTo(newData, 0);
            newData[newData.Length - 1] = b;
            return new Sequence(newData);
        }
    }

    public class SequenceComparer : IEqualityComparer<Sequence>
    {
        public bool Equals(Sequence x, Sequence y) => x.Data.SequenceEqual(y.Data);
        public int GetHashCode(Sequence obj) => obj.Data.Aggregate(0, (a, b) => a ^ b);
    }
}
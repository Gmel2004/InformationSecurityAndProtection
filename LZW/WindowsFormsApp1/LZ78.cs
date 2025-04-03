using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WindowsFormsApp1
{
    public static class LZ78
    {
        //change to work with inputData
        public static byte[] Compress(byte[] input)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            for (int i = 0; i < 256; i++)
            {
                dictionary.Add(((char)i).ToString(), i);
            }

            string current = "";
            List<int> output = new List<int>();
            int dictSize = 256;

            foreach (byte b in input)
            {
                string next = current + (char)b;
                if (dictionary.ContainsKey(next))
                {
                    current = next;
                }
                else
                {
                    output.Add(dictionary[current]);
                    dictionary.Add(next, dictSize++);
                    current = ((char)b).ToString();
                }
            }

            if (!string.IsNullOrEmpty(current))
                output.Add(dictionary[current]);

            return ConvertToBytes(output);
        }

        public static byte[] Decompress(byte[] input)
        {
            List<int> compressed = ConvertToInts(input);
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            for (int i = 0; i < 256; i++)
            {
                dictionary.Add(i, ((char)i).ToString());
            }

            string entry = dictionary[compressed[0]];
            StringBuilder output = new StringBuilder(entry);
            int dictSize = 256;

            for (int i = 1; i < compressed.Count; i++)
            {
                string result;
                if (dictionary.ContainsKey(compressed[i]))
                    result = dictionary[compressed[i]];
                else if (compressed[i] == dictSize)
                    result = entry + entry[0];
                else
                    throw new InvalidDataException("Ошибка декодирования");

                output.Append(result);
                dictionary.Add(dictSize++, entry + result[0]);
                entry = result;
            }

            return Encoding.Default.GetBytes(output.ToString());
        }

        private static byte[] ConvertToBytes(List<int> input)
        {
            byte[] bytes = new byte[input.Count * 4];
            for (int i = 0; i < input.Count; i++)
            {
                byte[] intBytes = System.BitConverter.GetBytes(input[i]);
                System.Buffer.BlockCopy(intBytes, 0, bytes, i * 4, 4);
            }
            return bytes;
        }

        private static List<int> ConvertToInts(byte[] input)
        {
            List<int> integers = new List<int>();
            for (int i = 0; i < input.Length; i += 4)
            {
                integers.Add(System.BitConverter.ToInt32(input, i));
            }
            return integers;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsFormsApp1
{
    public class LZ78
    {
        private const int MaxBits = 12;
        private const int MaxDictionarySize = 1 << MaxBits;

        public byte[] Compress(InputDataLZ78 inputData)
        {
            List<byte> compressedData = new List<byte>();
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            StringBuilder current = new StringBuilder();

            while (inputData.CurrentIndex < inputData.Length)
            {
                byte currentByte = inputData.Data[inputData.CurrentIndex++];
                current.Append((char)currentByte);

                if (!dictionary.ContainsKey(current.ToString()))
                {
                    if (current.Length > 1)
                    {
                        string prefix = current.ToString(0, current.Length - 1);
                        if (dictionary.TryGetValue(prefix, out int index))
                        {
                            compressedData.Add((byte)(index >> 8));
                            compressedData.Add((byte)(index & 0xFF));
                        }
                        else
                        {
                            throw new InvalidOperationException
                                (
                                    $"Prefix not found in dictionary: {prefix}"
                                );
                        }
                    }
                    else
                    {
                        compressedData.Add(0);
                        compressedData.Add(0);
                    }

                    compressedData.Add(currentByte);

                    if (dictionary.Count < MaxDictionarySize)
                    {
                        dictionary[current.ToString()] = dictionary.Count + 1;
                    }
                    current.Clear();
                }
            }

            if (current.Length > 0)
            {
                if (current.Length > 1)
                {
                    string prefix = current.ToString(0, current.Length - 1);
                    if (dictionary.TryGetValue(prefix, out int index))
                    {
                        compressedData.Add((byte)(index >> 8));
                        compressedData.Add((byte)(index & 0xFF));
                    }
                    else
                    {
                        throw new InvalidOperationException
                            (
                                $"Prefix not found in dictionary: {prefix}"
                            );
                    }
                }
                else
                {
                    compressedData.Add(0);
                    compressedData.Add(0);
                }
                compressedData.Add((byte)current[current.Length - 1]);
            }

            return compressedData.ToArray();
        }

        public byte[] Decompress(OutData78 outData)
        {
            List<byte> decompressedData = new List<byte>();
            Dictionary<int, string> dictionary = new Dictionary<int, string>();

            while (outData.CurrentIndex < outData.Length)
            {
                int index = outData.Data[outData.CurrentIndex].Item1;
                byte value = outData.Data[outData.CurrentIndex].Item2;
                outData.CurrentIndex++;

                string currentString;
                if (index != 0)
                {
                    if (!dictionary.TryGetValue(index, out currentString))
                    {
                        throw new InvalidOperationException
                            (
                                $"Error: Index {index} not found in dictionary" +
                                $"during decompression"
                            );
                    }
                }
                else
                {
                    currentString = string.Empty;
                }

                currentString += (char)value;

                foreach (char c in currentString)
                {
                    decompressedData.Add((byte)c);
                }

                if (dictionary.Count < MaxDictionarySize)
                {
                    dictionary[dictionary.Count + 1] = currentString;
                }
            }

            return decompressedData.ToArray();
        }
    }
}

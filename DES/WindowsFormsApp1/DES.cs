using System;
using System.Collections.Generic;

namespace WindowsFormsApp1
{
    internal class DES
    {
        #region Tables
        private static readonly int[] IP =
        {
            58, 50, 42, 34, 26, 18, 10, 2,
            60, 52, 44, 36, 28, 20, 12, 4,
            62, 54, 46, 38, 30, 22, 14, 6,
            64, 56, 48, 40, 32, 24, 16, 8,
            57, 49, 41, 33, 25, 17, 9, 1,
            59, 51, 43, 35, 27, 19, 11, 3,
            61, 53, 45, 37, 29, 21, 13, 5,
            63, 55, 47, 39, 31, 23, 15, 7
        };

        private static readonly int[] FP =
        {
            40, 8, 48, 16, 56, 24, 64, 32,
            39, 7, 47, 15, 55, 23, 63, 31,
            38, 6, 46, 14, 54, 22, 62, 30,
            37, 5, 45, 13, 53, 21, 61, 29,
            36, 4, 44, 12, 52, 20, 60, 28,
            35, 3, 43, 11, 51, 19, 59, 27,
            34, 2, 42, 10, 50, 18, 58, 26,
            33, 1, 41, 9, 49, 17, 57, 25
        };

        private static readonly int[] E =
        {
            32, 1, 2, 3, 4, 5,
            4, 5, 6, 7, 8, 9,
            8, 9, 10, 11, 12, 13,
            12, 13, 14, 15, 16, 17,
            16, 17, 18, 19, 20, 21,
            20, 21, 22, 23, 24, 25,
            24, 25, 26, 27, 28, 29,
            28, 29, 30, 31, 32, 1
        };

        private static readonly int[] P =
        {
            16, 7, 20, 21, 29, 12, 28, 17,
            1, 15, 23, 26, 5, 18, 31, 10,
            2, 8, 24, 14, 32, 27, 3, 9,
            19, 13, 30, 6, 22, 11, 4, 25
        };

        private static readonly int[] PC1 =
        {
            57, 49, 41, 33, 25, 17, 9,
            1, 58, 50, 42, 34, 26, 18,
            10, 2, 59, 51, 43, 35, 27,
            19, 11, 3, 60, 52, 44, 36,
            63, 55, 47, 39, 31, 23, 15,
            7, 62, 54, 46, 38, 30, 22,
            14, 6, 61, 53, 45, 37, 29,
            21, 13, 5, 28, 20, 12, 4
        };

        private static readonly int[] PC2 =
        {
            14, 17, 11, 24, 1, 5,
            3, 28, 15, 6, 21, 10,
            23, 19, 12, 4, 26, 8,
            16, 7, 27, 20, 13, 2,
            41, 52, 31, 37, 47, 55,
            30, 40, 51, 45, 33, 48,
            44, 49, 39, 56, 34, 53,
            46, 42, 50, 36, 29, 32
        };

        private static readonly int[,,] SBoxes =
        {
            {
                {14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7},
                {0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8},
                {4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0},
                {15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13}
            },
            {
                {15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10},
                {3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5},
                {0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15},
                {13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9}
            },
            {
                {10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8},
                {13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1},
                {13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7},
                {1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12}
            },
            {
                {7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15},
                {13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9},
                {10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4},
                {3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14}
            },
            {
                {2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9},
                {14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6},
                {4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14},
                {11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3}
            },
            {
                {12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11},
                {10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8},
                {9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6},
                {4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13}
            },
            {
                {4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1},
                {13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6},
                {1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2},
                {6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12}
            },
            {
                {13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7},
                {1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2},
                {7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8},
                {2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11}
            }
        };

        private static readonly int[] ShiftBits = { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };

        private ulong[] subKeys;
        #endregion 

        public DES(byte[] key)
        {
            if (key.Length != 8)
                throw new ArgumentException("Key must be 8 bytes");

            ulong keyUlong = BytesToUlong(key, 0, true);
            subKeys = GenerateSubKeys(keyUlong);
        }

        #region Core Algorithm
        private ulong[] GenerateSubKeys(ulong key)
        {
            ulong permutedKey = Permute(key, PC1, 64, 56);

            uint c = (uint)(permutedKey >> 28);
            uint d = (uint)(permutedKey & 0x0FFFFFFF);

            ulong[] subKeys = new ulong[16];  
            for (int i = 0; i < 16; i++)
            {
                c = RotateLeft(c, ShiftBits[i]);
                d = RotateLeft(d, ShiftBits[i]);

                ulong combined = ((ulong)c << 28) | d;
                subKeys[i] = Permute(combined, PC2, 56, 48);
            }
            return subKeys;
        }

        private ulong TransformBlock(ulong block, bool decrypt)
        {
            block = InitialPermutation(block);

            uint left = (uint)(block >> 32);
            uint right = (uint)(block & 0xFFFFFFFF);

            int rounds = decrypt ? 15 : 0;
            int step = decrypt ? -1 : 1;

            for (int i = 0; i < 16; i++)
            {
                uint temp = right;
                right = left ^ Feistel(right, subKeys[rounds]);
                left = temp;
                rounds += step;
            }

            ulong result = ((ulong)right << 32) | left;
            return FinalPermutation(result);
        }
        #endregion

        #region Helper Methods
        public static ulong BytesToUlong(byte[] bytes, int startIndex, bool isBigEndian)
        {
            if (startIndex + 8 > bytes.Length)
                throw new ArgumentException("Not enough bytes");

            byte[] block = new byte[8];
            Array.Copy(bytes, startIndex, block, 0, 8);

            if (isBigEndian != BitConverter.IsLittleEndian)
                Array.Reverse(block);

            return BitConverter.ToUInt64(block, 0);
        }

        public static byte[] UlongToBytes(ulong value, bool isBigEndian)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            if (isBigEndian != BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            return bytes;
        }

        private uint Substitute(ulong value)
        {
            uint result = 0;
            for (int i = 0; i < 8; i++)
            {
                int shift = 42 - 6 * i;
                int block = (int)((value >> shift) & 0x3F);

                int row = ((block >> 4) & 0x2) | (block & 0x1);
                int col = (block >> 1) & 0xF;

                result = (result << 4) | (uint)SBoxes[7 - i, row, col];
            }
            return result;
        }

        // Улучшенное дополнение PKCS#7
        public static byte[] AddPKCS7Padding(byte[] data)
        {
            int blockSize = 8; // Для DES блок = 8 байт
            int padding = blockSize - (data.Length % blockSize);
            if (padding == 0) padding = blockSize;

            byte[] padded = new byte[data.Length + padding];
            Array.Copy(data, padded, data.Length);

            // Заполняем дополнение значением `padding`
            for (int i = data.Length; i < padded.Length; i++)
                padded[i] = (byte)padding;

            return padded;
        }

        public static byte[] RemovePKCS7Padding(byte[] data)
        {
            if (data == null || data.Length < 1)
                throw new ArgumentException("Некорректные данные");

            int padding = data[data.Length - 1];

            if (padding < 1 || padding > 8)
                throw new ArgumentException("Некорректное дополнение");

            for (int i = data.Length - padding; i < data.Length; i++)
            {
                if (data[i] != padding)
                    throw new ArgumentException("Некорректное дополнение");
            }

            byte[] result = new byte[data.Length - padding];
            Array.Copy(data, result, result.Length);
            return result;
        }
        #endregion

        #region Public Interface
        public byte[] Encrypt(byte[] data)
        {
            byte[] padded = AddPKCS7Padding(data);
            List<byte> result = new List<byte>();

            for (int i = 0; i < padded.Length; i += 8)
            {
                ulong block = BytesToUlong(padded, i, true);
                ulong encrypted = TransformBlock(block, false);
                result.AddRange(UlongToBytes(encrypted, true));
            }

            return result.ToArray();
        }

        public byte[] Decrypt(byte[] encrypted)
        {
            List<byte> result = new List<byte>();

            for (int i = 0; i < encrypted.Length; i += 8)
            {
                ulong block = BytesToUlong(encrypted, i, true);
                ulong decrypted = TransformBlock(block, true);
                result.AddRange(UlongToBytes(decrypted, true));
            }

            return RemovePKCS7Padding(result.ToArray());
        }
        #endregion

        #region Utility Functions
        private ulong Permute(ulong value, int[] table, int inputBits, int outputBits)
        {
            ulong result = 0;
            for (int i = 0; i < table.Length; i++)
            {
                int pos = inputBits - table[i];
                result = (result << 1) | ((value >> pos) & 0x1);
            }
            return result;
        }

        private uint RotateLeft(uint value, int bits) =>
            ((value << bits) | (value >> (28 - bits))) & 0x0FFFFFFF;

        private uint Feistel(uint right, ulong subKey)
        {
            ulong expanded = Permute(right, E, 32, 48);
            expanded ^= subKey;
            uint substituted = Substitute(expanded);
            return (uint)Permute(substituted, P, 32, 32);
        }

        private ulong InitialPermutation(ulong data) => Permute(data, IP, 64, 64);
        private ulong FinalPermutation(ulong data) => Permute(data, FP, 64, 64);
        #endregion
    }
}
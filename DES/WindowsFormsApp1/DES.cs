using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;

class DES
{
    private static readonly int BlockSize = 4; // 32 бита (4 байта)
    private static readonly int KeySize = 8; // 64 бита (8 байтов)

    // Начальная перестановка (IP)
    private static readonly int[] IP = {
        58, 50, 42, 34, 26, 18, 10, 2,
        60, 52, 44, 36, 28, 20, 12, 4,
        62, 54, 46, 38, 30, 22, 14, 6,
        64, 56, 48, 40, 32, 24, 16, 8,
        57, 49, 41, 33, 25, 17, 9, 1,
        59, 51, 43, 35, 27, 19, 11, 3,
        61, 53, 45, 37, 29, 21, 13, 5,
        63, 55, 47, 39, 31, 23, 15, 7
    };

    // Финальная перестановка (FP)
    private static readonly int[] FP = {
        40, 8, 48, 16, 56, 24, 64, 32,
        39, 7, 47, 15, 55, 23, 63, 31,
        38, 6, 46, 14, 54, 22, 62, 30,
        37, 5, 45, 13, 53, 21, 61, 29,
        36, 4, 44, 12, 52, 20, 60, 28,
        35, 3, 43, 11, 51, 19, 59, 27,
        34, 2, 42, 10, 50, 18, 58, 26,
        33, 1, 41, 9, 49, 17, 57, 25
    };

    // Таблица расширения (E)
    private static readonly int[] E = {
        32, 1, 2, 3, 4, 5,
        4, 5, 6, 7, 8, 9,
        8, 9, 10, 11, 12, 13,
        12, 13, 14, 15, 16, 17,
        16, 17, 18, 19, 20, 21,
        20, 21, 22, 23, 24, 25,
        24, 25, 26, 27, 28, 29,
        28, 29, 30, 31, 32, 1
    };

    // S-блоки
    private static readonly int[,,] SBoxes = {
        // S1
        {
            {14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7},
            {0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8},
            {4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0},
            {15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13}
        },
        // S2
        {
            {15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10},
            {3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5},
            {0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15},
            {13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9}
        },
        // S3
        {
            {10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8},
            {13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1},
            {13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7},
            {1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12}
        },
        // S4
        {
            {7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15},
            {13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9},
            {10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4},
            {3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14}
        },
        // S5
        {
            {2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9},
            {14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6},
            {4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14},
            {11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3}
        },
        // S6
        {
            {12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11},
            {10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8},
            {9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6},
            {4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13}
        },
        // S7
        {
            {4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1},
            {13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6},
            {1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2},
            {6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12}
        },
        // S8
        {
            {13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7},
            {1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2},
            {7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8},
            {2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11}
        }
    };

    // P-перестановка
    private static readonly int[] P = {
        16, 7, 20, 21,
        29, 12, 28, 17,
        1, 15, 23, 26,
        5, 18, 31, 10,
        2, 8, 24, 14,
        32, 27, 3, 9,
        19, 13, 30, 6,
        22, 11, 4, 25
    };

    // Таблица сдвигов для генерации ключей
    private static readonly int[] KeyShifts = {
        1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1
    };

    private byte[] _key;
    private byte[][] _subKeys;

    // Конструктор: принимает ключ или генерирует его, если не предоставлен
    public DES(byte[] key = null)
    {
        if (key == null)
        {
            // Генерация случайного ключа, если он не предоставлен
            using (var rng = new RNGCryptoServiceProvider())
            {
                key = new byte[KeySize];
                rng.GetBytes(key);
            }
        }
        else if (key.Length != KeySize)
        {
            throw new ArgumentException("Ключ должен быть 64 бита (8 байтов).");
        }

        _key = key;
        _subKeys = GenerateSubKeys(_key);
    }

    // Генерация подключей
    private byte[][] GenerateSubKeys(byte[] key)
    {
        byte[][] subKeys = new byte[16][];
        // Реализация генерации ключей...
        return subKeys;
    }

    // Начальная перестановка
    private byte[] InitialPermutation(byte[] block)
    {
        return Permute(block, IP);
    }

    // Финальная перестановка
    private byte[] FinalPermutation(byte[] block)
    {
        return Permute(block, FP);
    }

    // Перестановка битов
    private byte[] Permute(byte[] input, int[] table)
    {
        byte[] output = new byte[table.Length / 8];
        for (int i = 0; i < table.Length; i++)
        {
            int bitIndex = table[i] - 1;
            int byteIndex = bitIndex / 8;
            int bitOffset = bitIndex % 8;
            output[i / 8] |= (byte)(((input[byteIndex] >> (7 - bitOffset)) & 1) << (7 - (i % 8)));
        }
        return output;
    }

    // Функция Фейстеля
    private byte[] FeistelFunction(byte[] right, byte[] subKey)
    {
        byte[] expanded = Permute(right, E);
        byte[] xored = XOR(expanded, subKey);
        byte[] substituted = Substitute(xored);
        return Permute(substituted, P);
    }

    // Подстановка с использованием S-блоков
    private byte[] Substitute(byte[] input)
    {
        byte[] output = new byte[4];
        for (int i = 0; i < 8; i++)
        {
            int row = ((input[i / 2] >> (4 - 4 * (i % 2))) & 0x0F);
            int col = ((input[i / 2] >> (3 - 4 * (i % 2))) & 0x0F);
            output[i / 2] |= (byte)(SBoxes[i, row, col] << (4 - 4 * (i % 2)));
        }
        return output;
    }

    // XOR двух массивов
    private byte[] XOR(byte[] a, byte[] b)
    {
        byte[] result = new byte[a.Length];
        for (int i = 0; i < a.Length; i++)
        {
            result[i] = (byte)(a[i] ^ b[i]);
        }
        return result;
    }

    // Шифрование одного блока
    private byte[] EncryptBlock(byte[] block)
    {
        if (block.Length != BlockSize)
            throw new ArgumentException("Блок должен быть 32 бита (4 байта).");

        block = InitialPermutation(block);

        byte[] left = new byte[4];
        byte[] right = new byte[4];
        Array.Copy(block, 0, left, 0, 4);
        Array.Copy(block, 4, right, 0, 4);

        for (int i = 0; i < 16; i++)
        {
            Debug.WriteLine($"Раунд шифрования {i + 1}:");
            Debug.WriteLine($"  Левый блок: {BitConverter.ToString(left)}");
            Debug.WriteLine($"  Правый блок: {BitConverter.ToString(right)}");
            Debug.WriteLine($"  Подключ: {BitConverter.ToString(_subKeys[i])}");

            byte[] temp = right;
            right = XOR(left, FeistelFunction(right, _subKeys[i]));
            left = temp;

            Debug.WriteLine($"  Результат раунда:");
            Debug.WriteLine($"    Левый блок: {BitConverter.ToString(left)}");
            Debug.WriteLine($"    Правый блок: {BitConverter.ToString(right)}");
            Debug.WriteLine("");
        }

        byte[] finalBlock = new byte[8];
        Array.Copy(right, 0, finalBlock, 0, 4);
        Array.Copy(left, 0, finalBlock, 4, 4);
        return FinalPermutation(finalBlock);
    }

    // Расшифрование одного блока
    private byte[] DecryptBlock(byte[] block)
    {
        if (block.Length != BlockSize)
            throw new ArgumentException("Блок должен быть 32 бита (4 байта).");

        block = InitialPermutation(block);

        byte[] left = new byte[4];
        byte[] right = new byte[4];
        Array.Copy(block, 0, left, 0, 4);
        Array.Copy(block, 4, right, 0, 4);

        for (int i = 15; i >= 0; i--)
        {
            Debug.WriteLine($"Раунд расшифрования {16 - i}:");
            Debug.WriteLine($"  Левый блок: {BitConverter.ToString(left)}");
            Debug.WriteLine($"  Правый блок: {BitConverter.ToString(right)}");
            Debug.WriteLine($"  Подключ: {BitConverter.ToString(_subKeys[i])}");

            byte[] temp = left;
            left = XOR(right, FeistelFunction(left, _subKeys[i]));
            right = temp;

            Debug.WriteLine($"  Результат раунда:");
            Debug.WriteLine($"    Левый блок: {BitConverter.ToString(left)}");
            Debug.WriteLine($"    Правый блок: {BitConverter.ToString(right)}");
            Debug.WriteLine("");
        }

        byte[] finalBlock = new byte[8];
        Array.Copy(left, 0, finalBlock, 0, 4);
        Array.Copy(right, 0, finalBlock, 4, 4);
        return FinalPermutation(finalBlock);
    }

    // Шифрование данных
    public byte[] Encrypt(byte[] data)
    {
        // Дополнение данных до размера, кратного BlockSize
        int paddingSize = BlockSize - (data.Length % BlockSize);
        if (paddingSize == BlockSize) paddingSize = 0;
        byte[] paddedData = new byte[data.Length + paddingSize];
        Array.Copy(data, paddedData, data.Length);

        // Шифрование блоков
        List<byte> encryptedData = new List<byte>();
        for (int i = 0; i < paddedData.Length; i += BlockSize)
        {
            byte[] block = new byte[BlockSize];
            Array.Copy(paddedData, i, block, 0, BlockSize);
            byte[] encryptedBlock = EncryptBlock(block);
            encryptedData.AddRange(encryptedBlock);
        }

        return encryptedData.ToArray();
    }

    // Расшифрование данных
    public byte[] Decrypt(byte[] data)
    {
        if (data.Length % BlockSize != 0)
            throw new ArgumentException("Данные должны быть кратны размеру блока.");

        // Расшифрование блоков
        List<byte> decryptedData = new List<byte>();
        for (int i = 0; i < data.Length; i += BlockSize)
        {
            byte[] block = new byte[BlockSize];
            Array.Copy(data, i, block, 0, BlockSize);
            byte[] decryptedBlock = DecryptBlock(block);
            decryptedData.AddRange(decryptedBlock);
        }

        return decryptedData.ToArray();
    }
}
using System;
using System.Collections.Generic;

namespace WindowsFormsApp1
{
    public static class LZ78
    {
        // Метод сжатия данных с использованием LZ78
        public static byte[] Compress(InputDataLZ78 inputData)
        {
            List<byte> compressedData = new List<byte>();
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            string current = "";
            int nextIndex = 1; // Индексация для словаря

            // Процесс сжатия
            for (int i = 0; i < inputData.Length; i++)
            {
                inputData.CurrentIndex++;
                byte currentByte = inputData.Data[i];
                current += (char)currentByte;

                if (!dictionary.ContainsKey(current))
                {
                    // Если текущая строка не найдена в словаре, добавляем её
                    if (current.Length > 1)
                    {
                        // Если строка не пустая, сжимаем её
                        int index = dictionary[current.Substring(0, current.Length - 1)];
                        compressedData.Add((byte)(index >> 8)); // старший байт индекса
                        compressedData.Add((byte)(index & 0xFF)); // младший байт индекса
                    }
                    else
                    {
                        // Если строка состоит из одного символа, добавляем 0 в индекс
                        compressedData.Add(0); // индекс = 0
                    }

                    compressedData.Add(currentByte); // Добавляем текущий символ
                    dictionary[current] = nextIndex++;  // Добавляем строку в словарь
                    current = ""; // Обнуляем строку для следующей итерации
                }
            }

            // Обрабатываем оставшиеся данные
            if (!string.IsNullOrEmpty(current))
            {
                if (current.Length > 1)
                {
                    int index = dictionary[current.Substring(0, current.Length - 1)];
                    compressedData.Add((byte)(index >> 8)); // старший байт индекса
                    compressedData.Add((byte)(index & 0xFF)); // младший байт индекса
                }
                else
                {
                    compressedData.Add(0); // индекс = 0
                }
                compressedData.Add((byte)current[current.Length - 1]);
            }

            return compressedData.ToArray();
        }

        // Метод распаковки данных с использованием LZ78
        public static byte[] Decompress(OutData78 outData)
        {
            List<byte> decompressedData = new List<byte>();
            Dictionary<int, string> dictionary = new Dictionary<int, string>();

            // Процесс распаковки
            foreach (var (index, value) in outData.Data)
            {
                // Строка восстанавливается из словаря
                string currentString = string.Empty;

                // Если индекс не равен 0, восстанавливаем строку из словаря
                if (index != 0)
                {
                    if (dictionary.ContainsKey(index))
                    {
                        currentString = dictionary[index];  // Восстанавливаем строку из словаря
                    }
                    else
                    {
                        // Ошибка: если индекс отсутствует в словаре
                        throw new InvalidOperationException($"Ошибка: индекс {index} отсутствует в словаре.");
                    }
                }

                // Добавляем текущий символ
                currentString += (char)value;

                // Добавляем восстановленную строку в результат
                foreach (var byteChar in currentString)
                {
                    decompressedData.Add((byte)byteChar);
                }

                // Обновляем словарь
                dictionary[index] = currentString;
            }

            return decompressedData.ToArray();
        }
    }
}

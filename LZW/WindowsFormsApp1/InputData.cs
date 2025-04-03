using System;
using WindowsFormsApp1;

namespace LZWCompressor
{
    public class InputData: IData
    {
        public byte[] Data { get; }
        public int CurrentIndex { get; set; }
        public int Length => Data.Length;

        public InputData(byte[] data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
            CurrentIndex = 0;
        }
    }
}
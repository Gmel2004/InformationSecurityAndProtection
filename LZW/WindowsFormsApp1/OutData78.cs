using System.Collections.Generic;
using WindowsFormsApp1;

public class OutData78 : IData
{
    public List<(int, byte)> Data { get; }

    public int CurrentIndex { get; set; }
    public int Length => Data.Count;

    // Constructor that accepts the result of compression (list of tuples)
    public OutData78(List<(int, byte)> compressedData)
    {
        Data = compressedData;
        CurrentIndex = 0;
    }
}
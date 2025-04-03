using WindowsFormsApp1;

public class InputData78 : IData
{
    public byte[] Data { get; }
    public int CurrentIndex { get; set; }
    public int Length => Data.Length;

    public InputData78(byte[] data)
    {
        Data = data;
        CurrentIndex = 0;
    }
}
using WindowsFormsApp1;

public class InputDataLZ78 : IData
{
    public byte[] Data { get; }
    public int CurrentIndex { get; set; }
    public int Length => Data.Length;

    public InputDataLZ78(byte[] data)
    {
        Data = data;
        CurrentIndex = 0;
    }
}
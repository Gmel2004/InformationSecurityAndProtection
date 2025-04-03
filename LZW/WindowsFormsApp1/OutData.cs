using System.Collections.Generic;

namespace WindowsFormsApp1
{
    public class OutData: IData
    {
        public List<int> Data { get; }
        public int CurrentIndex { get; set; }
        public int Length => Data.Count;

        public OutData(List<int> codes)
        {
            Data = codes;
            CurrentIndex = 0;
        }
    }
}

using System.Numerics;

namespace WindowsFormsApp1
{
    internal class Message
    {
        public BigInteger hash;
        public BigInteger r;
        public BigInteger s;

        public Message (BigInteger hash, BigInteger r, BigInteger s)
        {
            this.hash = hash;
            this.r = r;
            this.s = s;
        }
    }
}

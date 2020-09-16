using System;
using System.IO;

namespace CpiFont
{
    public static class Extensions
    {
        public static SeekOrigin ToSeekOrigin(this Interop.Origin origin)
        {
            switch (origin) {
                case Interop.Origin.Beg: return SeekOrigin.Begin;
                case Interop.Origin.Cur: return SeekOrigin.Current;
                case Interop.Origin.End: return SeekOrigin.End;
            }

            throw new InvalidCastException("Invalid origin value");
        }
    }
}

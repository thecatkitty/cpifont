using System;
using System.Runtime.InteropServices;

namespace CpiFont
{
    public class Interop
    {
        public delegate UIntPtr ReadWriteFPtr(IntPtr ctx, byte[] buff, UIntPtr bytes);
        public delegate UIntPtr FlushTellFPtr(IntPtr ctx);
        public delegate bool SeekFPtr(IntPtr ctx, UIntPtr offset, int origin);

        [StructLayout(LayoutKind.Sequential)]
        public class Stream {
            public ReadWriteFPtr Read;
            public ReadWriteFPtr Write;
            public FlushTellFPtr Flush;
            public FlushTellFPtr Tell;
            public SeekFPtr Seek;
            public IntPtr Context;
        }

        [DllImport("cpifont.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int cpifont_get_type(Stream s);
    }
}

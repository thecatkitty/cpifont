using System;
using System.Runtime.InteropServices;

namespace CpiFont
{
    public partial class Interop
    {
        public delegate UIntPtr ReadFPtr(
            IntPtr ctx,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] buff,
            UIntPtr bytes);
        public delegate UIntPtr WriteFPtr(
            IntPtr ctx,
            [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] buff,
            UIntPtr bytes);
        public delegate UIntPtr FlushTellFPtr(
            IntPtr ctx);
        public delegate bool SeekFPtr(
            IntPtr ctx,
            UIntPtr offset,
            Origin origin);

        [StructLayout(LayoutKind.Sequential)]
        public class Stream {
            public ReadFPtr Read;
            public WriteFPtr Write;
            public FlushTellFPtr Flush;
            public FlushTellFPtr Tell;
            public SeekFPtr Seek;
            public IntPtr Context;
        }
    }
}

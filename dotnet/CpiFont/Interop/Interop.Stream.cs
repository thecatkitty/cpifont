using System;
using System.Runtime.InteropServices;

namespace CpiFont
{
    public partial class Interop
    {
        public enum Origin {
            Beg = 0,
            Cur = 1,
            End = 2
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate UIntPtr ReadFPtr(
            IntPtr ctx,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] buff,
            UIntPtr bytes);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate UIntPtr WriteFPtr(
            IntPtr ctx,
            [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] buff,
            UIntPtr bytes);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate UIntPtr FlushTellFPtr(
            IntPtr ctx);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
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

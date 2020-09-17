using System;
using System.Runtime.InteropServices;

namespace CpiFont
{
    public partial class Interop
    {
        public enum cpifont_origin
        {
            CPIFONT_ORIGIN_BEG = 0,
            CPIFONT_ORIGIN_CUR = 1,
            CPIFONT_ORIGIN_END = 2
        }

        [StructLayout(LayoutKind.Sequential)]
        public class cpifont_stream {
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
                cpifont_origin origin);

            public ReadFPtr read;
            public WriteFPtr write;
            public FlushTellFPtr flush;
            public FlushTellFPtr tell;
            public SeekFPtr seek;
            public IntPtr context;
        }
    }
}

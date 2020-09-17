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
            public delegate cpifont_status ReadFPtr(
                IntPtr ctx,
                [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] buff,
                UIntPtr bytes);

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate cpifont_status WriteFPtr(
                IntPtr ctx,
                [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] buff,
                UIntPtr bytes);

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate cpifont_status FlushFPtr(
                IntPtr ctx);

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate UIntPtr TellFPtr(
                IntPtr ctx);

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate cpifont_status SeekFPtr(
                IntPtr ctx,
                UIntPtr offset,
                cpifont_origin origin);

            public ReadFPtr read;
            public WriteFPtr write;
            public FlushFPtr flush;
            public TellFPtr tell;
            public SeekFPtr seek;
            public IntPtr context;
        }
    }
}

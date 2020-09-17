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
        public class cpifont_stream
        {
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate cpifont_status ReadCallback(
                IntPtr ctx,
                [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] buff,
                UIntPtr bytes);

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate cpifont_status WriteCallback(
                IntPtr ctx,
                [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] buff,
                UIntPtr bytes);

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate cpifont_status FlushCallback(
                IntPtr ctx);

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate UIntPtr TellCallback(
                IntPtr ctx);

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate cpifont_status SeekCallback(
                IntPtr ctx,
                UIntPtr offset,
                cpifont_origin origin);

            public ReadCallback read;
            public WriteCallback write;
            public FlushCallback flush;
            public TellCallback tell;
            public SeekCallback seek;
            public IntPtr context;
        }
    }
}

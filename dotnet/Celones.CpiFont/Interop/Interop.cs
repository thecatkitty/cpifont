using System;
using System.Runtime.InteropServices;

namespace Celones.CpiFont
{
    public enum cpifont_status
    {
        CPIFONT_OK             = 0x000,
        CPIFONT_LAST           = 0x001,
        CPIFONT_RANGE_ERROR    = 0x010,
        CPIFONT_VALUE_ERROR    = 0x011,
        CPIFONT_STREAM_ERROR   = 0x100,
        CPIFONT_STREAM_EOF     = 0x101,
        CPIFONT_STREAM_RANGE   = 0x102,
        CPIFONT_STREAM_FATAL   = 0x1FF,
        CPIFONT_UNKNOWN_TYPE   = 0x200,
        CPIFONT_INVALID_FORMAT = 0x201,
    }

    internal static partial class Interop
    {
        [DllImport("cpifont.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern cpifont_status cpifont_get_type(
            cpifont_stream s,
            out FileType type);

        [DllImport("cpifont.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern cpifont_status cpifont_get_entry_count(
            cpifont_stream s,
            out int entry_count);

        [DllImport("cpifont.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern cpifont_status cpifont_get_next_entry(
            cpifont_stream s,
            [In, Out] cpifont_entry_info entry);

        [DllImport("cpifont.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern cpifont_status cpifont_get_next_font(
            cpifont_stream s,
            cpifont_entry_info entry,
            [Out] cpifont_font_info font);

        [DllImport("cpifont.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern cpifont_status cpifont_get_glyph(
            cpifont_stream s,
            cpifont_font_info font,
            UIntPtr index,
            [Out, MarshalAs(UnmanagedType.LPArray)] byte[] glyph);

        static public void CheckStatus(cpifont_status status)
        {
            switch (status)
            {
                case cpifont_status.CPIFONT_OK:
                    return;
                case cpifont_status.CPIFONT_LAST:
                case cpifont_status.CPIFONT_RANGE_ERROR:
                    throw new IndexOutOfRangeException($"Native function call returned {status}");
                case cpifont_status.CPIFONT_VALUE_ERROR:
                    throw new ArgumentException($"Native function call returned {status}");
                case cpifont_status.CPIFONT_STREAM_ERROR:
                case cpifont_status.CPIFONT_STREAM_EOF:
                case cpifont_status.CPIFONT_STREAM_RANGE:
                case cpifont_status.CPIFONT_STREAM_FATAL:
                    throw new System.IO.IOException($"Native function call returned {status}");
                case cpifont_status.CPIFONT_UNKNOWN_TYPE:
                case cpifont_status.CPIFONT_INVALID_FORMAT:
                    throw new FormatException($"Native function call returned {status}");
            }
        }
    }
}

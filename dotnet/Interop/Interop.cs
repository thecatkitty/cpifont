using System;
using System.Runtime.InteropServices;

namespace CpiFont
{
    public partial class Interop
    {
        [DllImport("cpifont.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern FileType cpifont_get_type(
            Stream s);

        [DllImport("cpifont.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int cpifont_get_entry_count(
            Stream s);

        [DllImport("cpifont.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool cpifont_get_next_entry(
            Stream s,
            [In, Out] EntryInfo entry);

        [DllImport("cpifont.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool cpifont_get_next_font(
            Stream s,
            EntryInfo entry,
            [Out] FontInfo font);

        [DllImport("cpifont.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool cpifont_get_glyph(
            Stream s,
            FontInfo font,
            UIntPtr index,
            [Out, MarshalAs(UnmanagedType.LPArray)] byte[] glyph);
    }
}

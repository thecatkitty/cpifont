using System;
using System.Runtime.InteropServices;

namespace Celones.CpiFont
{
    internal static partial class Interop
    {
        [StructLayout(LayoutKind.Sequential)]
        public class cpifont_font_info
        {
            public byte glyph_width;
            public byte glyph_height;
            public UInt16 glyphs;
            public UIntPtr bitmap_offset;
            public UIntPtr bitmap_size;
        }
    }
}

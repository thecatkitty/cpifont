using System;
using System.Runtime.InteropServices;

namespace CpiFont
{
    public partial class Interop
    {
        [StructLayout(LayoutKind.Sequential)]
        public class FontInfo {
            public byte GlyphWidth;
            public byte GlyphHeight;
            public UInt16 Glyphs;
            public UIntPtr BitmapOffset;
            public UIntPtr BitmapSize;
        }
    }
}

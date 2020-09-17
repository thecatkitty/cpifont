using System;

namespace Celones.CpiFont
{
    class Font
    {
        Interop.cpifont_stream _stream;

        public Font(Interop.cpifont_stream stream, Interop.cpifont_font_info info)
        {
            _stream = stream;
            NativeInfo = info;
        }

        public byte[] GetGlyph(int index)
        {
            if (index < 0 || index >= NativeInfo.glyphs)
            {
                throw new System.ArgumentOutOfRangeException(
                    "Glyph index out of range");
            }

            byte[] glyph = new byte[GlyphSize];
            Interop.cpifont_get_glyph(_stream, NativeInfo, (UIntPtr)index, glyph);
            return glyph;
        }
        public int GlyphSize {
            get => ((NativeInfo.glyph_width - 1) / 8 + 1) * NativeInfo.glyph_height;
        }
        public Interop.cpifont_font_info NativeInfo { get; }
    }
}

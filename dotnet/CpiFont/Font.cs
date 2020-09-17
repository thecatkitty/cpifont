using System;

namespace CpiFont
{
    class Font
    {
        Interop.cpifont_font_info _info;
        Interop.cpifont_stream _stream;

        public Font(Interop.cpifont_stream stream, Interop.cpifont_font_info info)
        {
            _stream = stream;
            _info = info;
        }

        public byte[] GetGlyph(int index)
        {
            if (index < 0 || index >= _info.glyphs)
            {
                throw new System.ArgumentOutOfRangeException(
                    "Glyph index out of range");
            }

            byte[] glyph = new byte[GlyphSize];
            Interop.cpifont_get_glyph(_stream, _info, (UIntPtr) index, glyph);
            return glyph;
        }
        public int GlyphSize {
            get => ((_info.glyph_width - 1) / 8 + 1) * _info.glyph_height; }
        public Interop.cpifont_font_info NativeInfo { get => _info; }
    }
}

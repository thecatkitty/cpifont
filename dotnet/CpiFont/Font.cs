using System;

namespace CpiFont
{
    class Font
    {
        Interop.FontInfo _info;
        Interop.Stream _stream;

        public Font(Interop.Stream stream, Interop.FontInfo info)
        {
            _stream = stream;
            _info = info;
        }

        public byte[] GetGlyph(int index)
        {
            if (index < 0 || index >= _info.Glyphs)
            {
                throw new System.ArgumentOutOfRangeException(
                    "Glyph index out of range");
            }

            byte[] glyph = new byte[GlyphSize];
            Interop.cpifont_get_glyph(_stream, _info, (UIntPtr) index, glyph);
            return glyph;
        }
        public int GlyphSize {
            get => ((_info.GlyphWidth - 1) / 8 + 1) * _info.GlyphHeight; }
        public Interop.FontInfo NativeInfo { get => _info; }
    }
}

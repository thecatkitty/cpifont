using System;
using System.IO;
using System.Runtime.InteropServices;
using CommandLine;

partial class CpiTool
{
    [Verb("glyph", HelpText = "Print one glyph.")]
    class GlyphOptions {
        [Value(0, Required = true, MetaName = "file", HelpText = "CPI file path")]
        public FileInfo File { get; set; }

        [Value(1, Required = true, MetaName = "entry", HelpText = "Index of entry in file")]
        public int EntryIndex { get; set; }

        [Value(2, Required = true, MetaName = "font", HelpText = "Index of font in entry")]
        public int FontIndex { get; set; }

        [Value(3, Required = true, MetaName = "glyph", HelpText = "Index of glyph in font")]
        public int GlyphIndex { get; set; }
    }

    static int RunGlyph(GlyphOptions options)
    {
        var file = options.File.Open(FileMode.Open);

        var stream = new CpiFont.Interop.Stream{};
        var ctx = GCHandle.Alloc(file);
        stream.Read = StreamRead;
        stream.Tell = StreamTell;
        stream.Seek = StreamSeek;
        stream.Context = GCHandle.ToIntPtr(ctx);

        if (options.EntryIndex >= CpiFont.Interop.cpifont_get_entry_count(stream))
        {
            throw new ArgumentOutOfRangeException(
                "Entry index out of range");
        }

        var entry = new CpiFont.Interop.EntryInfo{};
        for (int e = 0; e <= options.EntryIndex; e++) {
            CpiFont.Interop.cpifont_get_next_entry(stream, entry);
        }

        if (options.FontIndex >= entry.Fonts)
        {
            throw new ArgumentOutOfRangeException(
                "Font index out of range");
        }

        var font = new CpiFont.Interop.FontInfo{};
        for (int f = 0; f <= options.FontIndex; f++) {
            CpiFont.Interop.cpifont_get_next_font(stream, entry, font);
        }

        if (options.GlyphIndex >= font.Glyphs)
        {
            throw new ArgumentOutOfRangeException(
                "Glyph index out of range");
        }

        var rowSize = (font.GlyphWidth - 1) / 8 + 1;
        var glyphSize = rowSize * font.GlyphHeight;
        byte[] glyph = new byte[glyphSize];
        CpiFont.Interop.cpifont_get_glyph(
            stream,
            font,
            (UIntPtr) options.GlyphIndex,
            glyph);
        PrintGlyph(glyph, font.GlyphWidth);

        ctx.Free();
        return 0;
    }
}

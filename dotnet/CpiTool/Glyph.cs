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
        var cpi = new CpiFont.CpiFile(file);
        var font = cpi.Entries[options.EntryIndex].Fonts[options.FontIndex];
        PrintGlyph(
            font.GetGlyph(options.GlyphIndex),
            font.NativeInfo.GlyphWidth);
        return 0;
    }
}

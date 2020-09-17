using System;
using System.IO;
using CommandLine;

partial class CpiTool
{
    [Verb("glyph", HelpText = "Print one glyph.")]
    class GlyphOptions
    {
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
        CpiFont.CpiFile cpi;
        try
        {
            cpi = OpenCpiFile(options.File);
        }
        catch (IOException ioex)
        {
            Console.Error.WriteLine($"error: {ioex.Message}");
            return 2;
        }
        catch (FormatException fex)
        {
            Console.Error.WriteLine($"error: {fex.Message}");
            return 3;
        }

        CpiFont.CodePage entry;
        try
        {
            entry = cpi.Entries[options.EntryIndex];
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.Error.WriteLine($"error: Entry index out of range.");
            return 21;
        }

        CpiFont.Font font;
        try
        {
            font = entry.Fonts[options.FontIndex];
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.Error.WriteLine($"error: Font index out of range.");
            return 22;
        }

        try
        {
            PrintGlyph(
                font.GetGlyph(options.GlyphIndex),
                font.NativeInfo.glyph_width);
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.Error.WriteLine($"error: Glyph index out of range.");
            return 23;
        }

        return 0;
    }
}

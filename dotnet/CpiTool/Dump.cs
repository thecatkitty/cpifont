using System;
using System.IO;
using System.Runtime.InteropServices;
using CommandLine;

partial class CpiTool
{
    [Verb("dump", HelpText = "Dump CPI file information.")]
    class DumpOptions {
        [Value(0, Required = true, MetaName = "file", HelpText = "CPI file path")]
        public System.IO.FileInfo File { get; set; }

        [Option(
            'g', "show-glyphs",
            Required = false, HelpText = "Show glyphs.")]
        public bool ShowGlyphs { get; set; }
    }

    static int RunDump(DumpOptions options)
    {
        var file = options.File.Open(FileMode.Open);
        var cpi = new CpiFont.CpiFile(file);

        Console.WriteLine($"file: type    = {cpi.Type}");
        Console.WriteLine($"file: entries = {cpi.Entries.Count}");

        for (int e = 0; e < cpi.Entries.Count; e++)
        {
            var entry = cpi.Entries[e];
            PrintObject($"entry {e}: ", entry.NativeEntry);

            for (int f = 0; f < entry.Fonts.Count; f++)
            {
                var font = entry.Fonts[f];
                PrintObject($"font {e},{f}: ", font.NativeInfo);

                var rowSize = (font.NativeInfo.GlyphWidth - 1) / 8 + 1;
                var glyphSize = rowSize * font.NativeInfo.GlyphHeight;
                if (options.ShowGlyphs) {
                    for (int g = 0; g < font.NativeInfo.Glyphs; g++)
                    {
                        var glyph = font.GetGlyph(g);
                        Console.WriteLine($"glyph {e},{f},{g}");
                        PrintGlyph(glyph, font.NativeInfo.GlyphWidth);
                    }
                }
            }
        }

        return 0;
    }
}

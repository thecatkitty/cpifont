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

        var stream = new CpiFont.Interop.Stream{};
        var ctx = GCHandle.Alloc(file);
        stream.Read = StreamRead;
        stream.Tell = StreamTell;
        stream.Seek = StreamSeek;
        stream.Context = GCHandle.ToIntPtr(ctx);

        var type = CpiFont.Interop.cpifont_get_type(stream);
        Console.WriteLine($"file: type    = {type}");

        var entries = CpiFont.Interop.cpifont_get_entry_count(stream);
        Console.WriteLine($"file: entries = {entries}");

        var entry = new CpiFont.Interop.EntryInfo{};
        entry.NextOffset = (UIntPtr) 0;
        for (int e = 0; e < entries; e++) {
            CpiFont.Interop.cpifont_get_next_entry(stream, entry);
            PrintObject($"entry {e}: ", entry);

            for (int f = 0; f < entry.Fonts; f++)
            {
                var font = new CpiFont.Interop.FontInfo{};
                CpiFont.Interop.cpifont_get_next_font(stream, entry, font);
                PrintObject($"font {e},{f}: ", font);

                var rowSize = (font.GlyphWidth - 1) / 8 + 1;
                var glyphSize = rowSize * font.GlyphHeight;
                if (options.ShowGlyphs) {
                    for (int g = 0; g < font.Glyphs; g++)
                    {
                        byte[] glyph = new byte[glyphSize];
                        CpiFont.Interop.cpifont_get_glyph(
                            stream, font, (UIntPtr) g, glyph);
                            Console.WriteLine($"glyph {e},{f},{g}");
                        PrintGlyph(glyph, font.GlyphWidth);
                    }
                }
            }
        }

        ctx.Free();
        return 0;
    }
}

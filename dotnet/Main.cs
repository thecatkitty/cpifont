using System;
using System.IO;
using System.Runtime.InteropServices;

partial class CpiTool
{
    static void Main(string[] args)
    {
        var file = new FileStream(args[0], FileMode.Open);

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

        ctx.Free();
    }

    static UIntPtr StreamRead(IntPtr ctx, byte[] buff, UIntPtr bytes)
    {
        var stream = GCHandle.FromIntPtr(ctx).Target as Stream;
        return (UIntPtr) stream.Read(buff, 0, (int) bytes);
    }

    static UIntPtr StreamTell(IntPtr ctx)
    {
        var stream = GCHandle.FromIntPtr(ctx).Target as Stream;
        return (UIntPtr) stream.Position;
    }

    static bool StreamSeek(IntPtr ctx, UIntPtr offset, CpiFont.Interop.Origin origin)
    {
        var stream = GCHandle.FromIntPtr(ctx).Target as Stream;
        var seekOrigin = (origin == CpiFont.Interop.Origin.Beg) ? SeekOrigin.Begin :
                         (origin == CpiFont.Interop.Origin.Cur) ? SeekOrigin.Current :
                         SeekOrigin.End;
        try {
            stream.Seek((long) offset, seekOrigin);
            return true;
        } catch {
            return false;
        }
    }
}

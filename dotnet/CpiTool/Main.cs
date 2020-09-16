using System;
using System.Linq;
using CommandLine;

partial class CpiTool
{
    static int Main(string[] args) =>
        Parser.Default.ParseArguments<DumpOptions, GlyphOptions>(args)
            .MapResult(
                (DumpOptions options) => RunDump(options),
                (GlyphOptions options) => RunGlyph(options),
                errors => 1);
}

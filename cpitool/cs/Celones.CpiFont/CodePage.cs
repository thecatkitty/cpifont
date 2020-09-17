using System.Collections.Generic;

namespace Celones.CpiFont
{
    class CodePage
    {
        Interop.cpifont_stream _stream;
        List<Font> _fonts;

        public CodePage(Interop.cpifont_stream stream, Interop.cpifont_entry_info entry)
        {
            _stream = stream;
            _fonts = new List<Font>();
            NativeEntry = entry;
        }

        public IList<Font> Fonts {
            get {
                if (_fonts.Count == 0)
                {
                    for (int e = 0; e < NativeEntry.fonts; e++)
                    {
                        var font = new Interop.cpifont_font_info { };
                        Interop.cpifont_get_next_font(_stream, NativeEntry, font);
                        _fonts.Add(new Font(_stream, font));
                    }
                }
                return _fonts;
            }
        }
        public Interop.cpifont_entry_info NativeEntry { get; }
    }
}

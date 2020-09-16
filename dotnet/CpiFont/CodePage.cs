using System.Collections.Generic;

namespace CpiFont
{
    class CodePage
    {
        Interop.EntryInfo _entry;
        Interop.Stream _stream;
        List<Font> _fonts;

        public CodePage(Interop.Stream stream, Interop.EntryInfo entry)
        {
            _stream = stream;
            _entry = entry;
            _fonts = new List<Font>();
        }
        
        public IList<Font> Fonts {
            get {
                if (_fonts.Count == 0)
                {
                    for (int e = 0; e < _entry.Fonts; e++) {
                        var font = new Interop.FontInfo{};
                        Interop.cpifont_get_next_font(_stream, _entry, font);
                        _fonts.Add(new Font(_stream, font));
                    }
                }
                return _fonts;
            }
        }
        public Interop.EntryInfo NativeEntry { get => _entry; }
    }
}

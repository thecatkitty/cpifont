using System.Collections.Generic;

namespace CpiFont
{
    class CodePage
    {
        Interop.cpifont_entry_info _entry;
        Interop.cpifont_stream _stream;
        List<Font> _fonts;

        public CodePage(Interop.cpifont_stream stream, Interop.cpifont_entry_info entry)
        {
            _stream = stream;
            _entry = entry;
            _fonts = new List<Font>();
        }
        
        public IList<Font> Fonts
        {
            get
            {
                if (_fonts.Count == 0)
                {
                    for (int e = 0; e < _entry.fonts; e++)
                    {
                        var font = new Interop.cpifont_font_info{};
                        Interop.cpifont_get_next_font(_stream, _entry, font);
                        _fonts.Add(new Font(_stream, font));
                    }
                }
                return _fonts;
            }
        }
        public Interop.cpifont_entry_info NativeEntry { get => _entry; }
    }
}

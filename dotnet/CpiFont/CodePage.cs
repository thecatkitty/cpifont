using System.Collections.Generic;

namespace CpiFont
{
    class CodePage
    {
        Interop.EntryInfo _entry;
        Interop.Stream _stream;

        public CodePage(Interop.Stream stream, Interop.EntryInfo entry)
        {
            _stream = stream;
            _entry = entry;
            Fonts = new List<Font>();
            for (int e = 0; e < entry.Fonts; e++) {
                var font = new Interop.FontInfo{};
                Interop.cpifont_get_next_font(stream, entry, font);
                Fonts.Add(new Font(stream, font));
            }
        }
        
        public IList<Font> Fonts { get; private set; }
        public Interop.EntryInfo NativeEntry { get => _entry; }
    }
}

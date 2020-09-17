using System.Collections.Generic;

namespace CpiFont
{
    class CpiFile
    {
        StreamAdapter _stream;
        List<CodePage> _entries;

        public CpiFile(System.IO.Stream stream)
        {
            _stream = new StreamAdapter(stream);

            FileType type;
            Interop.cpifont_get_type(_stream, out type);
            Type = type;
            _entries = new List<CodePage>();
        }

        public FileType Type { get; private set; }

        public IList<CodePage> Entries {
            get {
                if (_entries.Count == 0 && Type != FileType.Unknown)
                {
                    int entryCount;
                    Interop.cpifont_get_entry_count(_stream, out entryCount);
                    var entry = new Interop.EntryInfo{};
                    for (int e = 0; e < entryCount; e++) {
                        Interop.cpifont_get_next_entry(_stream, entry);
                        _entries.Add(new CodePage(_stream, entry));
                        var next = new Interop.EntryInfo{};
                        next.NextOffset = entry.NextOffset;
                        next.FileType = entry.FileType;
                        entry = next;
                    }
                }
                return _entries;
            }
        }
    
        public StreamAdapter Stream { get => _stream; }
    }
}

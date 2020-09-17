using System.Collections.Generic;

namespace CpiFont
{
    partial class CpiFile
    {
        StreamAdapter _stream;
        List<CodePage> _entries;

        public CpiFile(System.IO.Stream stream)
        {
            _stream = new StreamAdapter(stream);
            _entries = new List<CodePage>();
            Type = NativeGetType();
        }

        public FileType Type { get; private set; }

        public IList<CodePage> Entries
        {
            get
            {
                if (_entries.Count == 0 && Type != FileType.Unknown)
                {
                    var entryCount = NativeGetEntryCount();
                    var entry = new Interop.cpifont_entry_info{};
                    for (int e = 0; e < entryCount; e++)
                    {
                        NativeGetNextEntry(entry);
                        _entries.Add(new CodePage(_stream, entry));
                        var next = new Interop.cpifont_entry_info{};
                        next.next_offset = entry.next_offset;
                        next.file_type = entry.file_type;
                        entry = next;
                    }
                }
                return _entries;
            }
        }
    
        public StreamAdapter Stream { get => _stream; }
    }
}

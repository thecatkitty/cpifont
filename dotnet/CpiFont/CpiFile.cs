using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CpiFont
{
    partial class CpiFile
    {
        Interop.Stream _stream;
        GCHandle _ctx;
        List<CodePage> _entries;

        public CpiFile(System.IO.Stream stream)
        {
            _stream = new Interop.Stream{};
            _ctx = GCHandle.Alloc(stream);
            _stream.Read = StreamRead;
            _stream.Tell = StreamTell;
            _stream.Seek = StreamSeek;
            _stream.Context = GCHandle.ToIntPtr(_ctx);

            Type = Interop.cpifont_get_type(_stream);
            _entries = new List<CodePage>();
        }

        ~CpiFile()
        {
            _ctx.Free();
        }

        public FileType Type { get; private set; }

        public IList<CodePage> Entries {
            get {
                if (_entries.Count == 0 && Type != FileType.Unknown)
                {
                    var entryCount = Interop.cpifont_get_entry_count(_stream);
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
    
        public Interop.Stream NativeStream { get => _stream; }
    }
}

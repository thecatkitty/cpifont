using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CpiFont
{
    partial class CpiFile
    {
        Interop.Stream _stream;
        GCHandle _ctx;

        public CpiFile(System.IO.Stream stream)
        {
            _stream = new Interop.Stream{};
            _ctx = GCHandle.Alloc(stream);
            _stream.Read = StreamRead;
            _stream.Tell = StreamTell;
            _stream.Seek = StreamSeek;
            _stream.Context = GCHandle.ToIntPtr(_ctx);

            Type = Interop.cpifont_get_type(_stream);
            Entries = new List<CodePage>();

            if (Type != FileType.Unknown)
            {
                var entryCount = Interop.cpifont_get_entry_count(_stream);
                var entry = new Interop.EntryInfo{};
                for (int e = 0; e < entryCount; e++) {
                    Interop.cpifont_get_next_entry(_stream, entry);
                    Entries.Add(new CodePage(_stream, entry));
                    var next = new Interop.EntryInfo{};
                    next.NextOffset = entry.NextOffset;
                    entry = next;
                }
            }
        }

        ~CpiFile()
        {
            _ctx.Free();
        }

        public FileType Type { get; private set; }

        public IList<CodePage> Entries { get; private set; }
    
        public Interop.Stream NativeStream { get => _stream; }
    }
}

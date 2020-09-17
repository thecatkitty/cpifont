using System.Runtime.InteropServices;
using System;

namespace CpiFont
{
    class StreamAdapter
    {
        System.IO.Stream _stream;
        Interop.cpifont_stream _native;
        GCHandle _handle;

        public StreamAdapter(System.IO.Stream stream)
        {
            _handle = GCHandle.Alloc(this);
            _stream = stream;

            _native = new Interop.cpifont_stream{};
            _native.read = ReadCallback;
            _native.seek = SeekCallback;
            _native.tell = TellCallback;
            _native.context = GCHandle.ToIntPtr(_handle);
        }

        ~StreamAdapter()
        {
            _handle.Free();
        }

        protected int Read(byte[] buff, int bytes) =>
            _stream.Read(buff, 0, bytes);

        protected int Tell() =>
            (int) _stream.Position;

        protected bool Seek(int offset, System.IO.SeekOrigin origin)
        {
            try {
                _stream.Seek((long) offset, origin);
                return true;
            } catch {
                return false;
            }
        }

        static private cpifont_status ReadCallback(IntPtr ctx, byte[] buff, UIntPtr bytes)
        {
            var obj = GCHandle.FromIntPtr(ctx).Target as StreamAdapter;
            try {
                if (obj.Read(buff, (int) bytes) != (int) bytes)
                {
                    return cpifont_status.CPIFONT_STREAM_EOF;
                }
                return cpifont_status.CPIFONT_OK;
            } catch(System.IO.IOException) {
                return cpifont_status.CPIFONT_STREAM_ERROR;
            } catch {
                return cpifont_status.CPIFONT_STREAM_FATAL;
            }
        }

        static private UIntPtr TellCallback(IntPtr ctx)
        {
            var obj = GCHandle.FromIntPtr(ctx).Target as StreamAdapter;
            return (UIntPtr) obj.Tell();
        }

        static private cpifont_status SeekCallback(IntPtr ctx, UIntPtr offset, Interop.cpifont_origin origin)
        {
            System.IO.SeekOrigin seekOrigin;
            switch (origin) {
                case Interop.cpifont_origin.CPIFONT_ORIGIN_BEG:
                    seekOrigin = System.IO.SeekOrigin.Begin;
                    break;
                case Interop.cpifont_origin.CPIFONT_ORIGIN_CUR:
                    seekOrigin = System.IO.SeekOrigin.Current;
                    break;
                case Interop.cpifont_origin.CPIFONT_ORIGIN_END:
                    seekOrigin = System.IO.SeekOrigin.End;
                    break;
                default:
                    return cpifont_status.CPIFONT_VALUE_ERROR;
            }

            var obj = GCHandle.FromIntPtr(ctx).Target as StreamAdapter;
            try {
                if (!obj.Seek((int) offset, seekOrigin))
                {
                    return cpifont_status.CPIFONT_STREAM_RANGE;
                }
                return cpifont_status.CPIFONT_OK;
            } catch(System.IO.IOException) {
                return cpifont_status.CPIFONT_STREAM_ERROR;
            } catch {
                return cpifont_status.CPIFONT_STREAM_FATAL;
            }
        }

        public static implicit operator Interop.cpifont_stream(StreamAdapter adapter) =>
            adapter._native;
    }
}

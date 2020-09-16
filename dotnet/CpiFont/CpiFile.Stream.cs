
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace CpiFont
{
    partial class CpiFile
    {
        static UIntPtr StreamRead(IntPtr ctx, byte[] buff, UIntPtr bytes)
        {
            var stream = GCHandle.FromIntPtr(ctx).Target as Stream;
            return (UIntPtr) stream.Read(buff, 0, (int) bytes);
        }

        static UIntPtr StreamTell(IntPtr ctx)
        {
            var stream = GCHandle.FromIntPtr(ctx).Target as Stream;
            return (UIntPtr) stream.Position;
        }

        static bool StreamSeek(IntPtr ctx, UIntPtr offset, Interop.Origin origin)
        {
            var stream = GCHandle.FromIntPtr(ctx).Target as Stream;
            try {
                stream.Seek((long) offset, origin.ToSeekOrigin());
                return true;
            } catch {
                return false;
            }
        }
    }
}

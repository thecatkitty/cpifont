
using System;
using System.IO;
using System.Runtime.InteropServices;

partial class CpiTool
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

    static bool StreamSeek(IntPtr ctx, UIntPtr offset, CpiFont.Interop.Origin origin)
    {
        var stream = GCHandle.FromIntPtr(ctx).Target as Stream;
        var seekOrigin = (origin == CpiFont.Interop.Origin.Beg) ? SeekOrigin.Begin :
                         (origin == CpiFont.Interop.Origin.Cur) ? SeekOrigin.Current :
                         SeekOrigin.End;
        try {
            stream.Seek((long) offset, seekOrigin);
            return true;
        } catch {
            return false;
        }
    }
}
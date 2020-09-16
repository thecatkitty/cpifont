using System;
using System.IO;
using System.Runtime.InteropServices;

class CpiTool
{
    static void Main(string[] args)
    {
        var file = new FileStream(args[0], FileMode.Open);

        var stream = new CpiFont.Interop.Stream{};
        var ctx = GCHandle.Alloc(file);
        stream.Read = StreamRead;
        stream.Tell = StreamTell;
        stream.Seek = StreamSeek;
        stream.Context = GCHandle.ToIntPtr(ctx);

        var ret = CpiFont.Interop.cpifont_get_type(stream);
        Console.WriteLine(ret);

        ctx.Free();
    }

    static UIntPtr StreamRead(IntPtr ctx, byte[] buff, UIntPtr bytes)
    {
        var stream = GCHandle.FromIntPtr(ctx).Target as Stream;
        Console.Write("read bytes: ");
        Console.WriteLine((int) bytes);
        return (UIntPtr) stream.Read(buff, 0, (int) bytes);
    }

    static UIntPtr StreamTell(IntPtr ctx)
    {
        var stream = GCHandle.FromIntPtr(ctx).Target as Stream;
        return (UIntPtr) stream.Position;
    }

    static bool StreamSeek(IntPtr ctx, UIntPtr offset, int origin)
    {
        var stream = GCHandle.FromIntPtr(ctx).Target as Stream;
        var seekOrigin = (origin == 0) ? SeekOrigin.Begin :
                         (origin == 1) ? SeekOrigin.Current :
                         SeekOrigin.End;
        try {
            stream.Seek((long) offset, seekOrigin);
            return true;
        } catch {
            return false;
        }
    }
}

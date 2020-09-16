using System;
using System.Runtime.InteropServices;

namespace CpiFont
{
    public partial class Interop
    {
        [StructLayout(LayoutKind.Sequential)]
        public class EntryInfo {
            public UIntPtr NextOffset;
            public FileType FileType;
            public Device Device;
            public DeviceType DeviceType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)] public char[] DeviceName;
            public UInt16 Codepage;
            public UInt16 Fonts;
            public UIntPtr FontsOffset;
            public UIntPtr FontsSize;
        }
    }
}

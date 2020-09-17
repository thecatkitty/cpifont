using System;
using System.Runtime.InteropServices;

namespace CpiFont
{
    public partial class Interop
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class cpifont_entry_info {
            public UIntPtr next_offset;
            public FileType file_type;
            public Device device;
            public DeviceType device_type;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
            public string device_name;
            public UInt16 codepage;
            public UInt16 fonts;
            public UIntPtr fonts_offset;
            public UIntPtr fonts_size;
        }
    }
}

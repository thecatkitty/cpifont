using System;

namespace CpiFont
{
    public partial class Interop
    {
        static public void CheckStatus(cpifont_status status)
        {
            switch (status)
            {
                case cpifont_status.CPIFONT_OK:
                    return;
                case cpifont_status.CPIFONT_LAST:
                case cpifont_status.CPIFONT_RANGE_ERROR:
                    throw new IndexOutOfRangeException($"Native function call returned {status}");
                case cpifont_status.CPIFONT_VALUE_ERROR:
                    throw new ArgumentException($"Native function call returned {status}");
                case cpifont_status.CPIFONT_STREAM_ERROR:
                case cpifont_status.CPIFONT_STREAM_EOF:
                case cpifont_status.CPIFONT_STREAM_RANGE:
                case cpifont_status.CPIFONT_STREAM_FATAL:
                    throw new System.IO.IOException($"Native function call returned {status}");
                case cpifont_status.CPIFONT_UNKNOWN_TYPE:
                case cpifont_status.CPIFONT_INVALID_FORMAT:
                    throw new FormatException($"Native function call returned {status}");
            }
        }
    }
}

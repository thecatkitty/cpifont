namespace Celones.CpiFont
{
    partial class CpiFile
    {
        FileType NativeGetType()
        {
            FileType type;
            Interop.CheckStatus(Interop.cpifont_get_type(Stream, out type));
            return type;
        }

        int NativeGetEntryCount()
        {
            Interop.CheckStatus(Interop.cpifont_get_entry_count(Stream, out int count));
            return count;
        }


        void NativeGetNextEntry(Interop.cpifont_entry_info entry)
        {
            Interop.CheckStatus(Interop.cpifont_get_next_entry(Stream, entry));
        }
    }
}

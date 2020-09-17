namespace CpiFont
{
    partial class CpiFile
    {
        FileType NativeGetType()
        {
            FileType type;
            Interop.CheckStatus(Interop.cpifont_get_type(_stream, out type));
            return type;
        }
        
        int NativeGetEntryCount()
        {
            int count;
            Interop.CheckStatus(Interop.cpifont_get_entry_count(_stream, out count));
            return count;
        }
        
        
        void NativeGetNextEntry(Interop.cpifont_entry_info entry)
        {
            Interop.CheckStatus(Interop.cpifont_get_next_entry(_stream, entry));
        }
    }
}

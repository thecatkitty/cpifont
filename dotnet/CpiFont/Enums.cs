namespace CpiFont
{
    public enum FileType
    {
        Unknown = 0,
        Dos     = 1,
        Nt      = 2
    }

    public enum Device
    {
        Unknown = 0x0000,
        Ega     = 0x1000,
        Ibm5140 = 0x1001,
        Cga     = 0x1002,
        Mono    = 0x1003,
        Ibm4201 = 0x2000,
        Ibm4208 = 0x2001,
        Ibm5205 = 0x2002,
        Ibm1050 = 0x2003,
        Eps     = 0x2004,
        Ppds    = 0x2005
    }

    public enum DeviceType
    {
        Unknown = 0,
        Display  = 1,
        Printer  = 2
    }
}

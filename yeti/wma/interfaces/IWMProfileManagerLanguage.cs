using System.Runtime.InteropServices;

namespace yeti.wma.interfaces
{
    [ComImport]
    [Guid("BA4DCC78-7EE0-4ab8-B27A-DBCE8BC51454")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMProfileManagerLanguage
    {
        void GetUserLanguageID([Out] out ushort wLangID);
        void SetUserLanguageID([In] ushort wLangID);
    };
}
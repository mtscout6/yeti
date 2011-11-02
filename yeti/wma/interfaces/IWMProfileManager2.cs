using System;
using System.Runtime.InteropServices;
using System.Text;
using yeti.wma.enums;

namespace yeti.wma.interfaces
{
    [ComImport]
    [Guid("7A924E51-73C1-494d-8019-23D37ED9B89A")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMProfileManager2 : IWMProfileManager
    {
        //IWMProfileManager
        new void CreateEmptyProfile([In] WMT_VERSION dwVersion,
                                    [Out, MarshalAs(UnmanagedType.Interface)] out IWMProfile ppProfile);
        new void LoadProfileByID([In] ref Guid guidProfile,
                                 [Out, MarshalAs(UnmanagedType.Interface)] out IWMProfile ppProfile);
        new void LoadProfileByData([In, MarshalAs(UnmanagedType.LPWStr)] string pwszProfile,
                                   [Out, MarshalAs(UnmanagedType.Interface)] out IWMProfile ppProfile);
        new void SaveProfile([In, MarshalAs(UnmanagedType.Interface)] IWMProfile pIWMProfile,
                             [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszProfile,
                             [In, Out] ref uint pdwLength);
        new void GetSystemProfileCount([Out] out uint pcProfiles);
        new void LoadSystemProfile([In] uint dwProfileIndex,
                                   [Out, MarshalAs(UnmanagedType.Interface)] out IWMProfile ppProfile);
        //IWMProfileManager2
        void GetSystemProfileVersion([Out] out WMT_VERSION pdwVersion);
        void SetSystemProfileVersion([In] WMT_VERSION dwVersion);
    }
}
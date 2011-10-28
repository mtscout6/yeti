using System;
using System.Runtime.InteropServices;
using System.Text;
using yeti.wma.enums;

namespace yeti.wma.interfaces
{
    [ComImport]
    [Guid("d16679f2-6ca0-472d-8d31-2f5d55aee155")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMProfileManager
    {
        void CreateEmptyProfile([In] WMT_VERSION dwVersion,
                                [Out, MarshalAs(UnmanagedType.Interface)] out IWMProfile ppProfile);
        void LoadProfileByID([In] ref Guid guidProfile,
                             [Out, MarshalAs(UnmanagedType.Interface)] out IWMProfile ppProfile);
        void LoadProfileByData([In, MarshalAs(UnmanagedType.LPWStr)] string pwszProfile,
                               [Out, MarshalAs(UnmanagedType.Interface)] out IWMProfile ppProfile);
        void SaveProfile([In, MarshalAs(UnmanagedType.Interface)] IWMProfile pIWMProfile,
                         [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszProfile,
                         [In, Out] ref uint pdwLength);
        void GetSystemProfileCount([Out] out uint pcProfiles);
        void LoadSystemProfile([In] uint dwProfileIndex,
                               [Out, MarshalAs(UnmanagedType.Interface)] out IWMProfile ppProfile);
    }
}
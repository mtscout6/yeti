using System;
using System.Runtime.InteropServices;
using System.Text;
using yeti.wma.enums;

namespace yeti.wma.interfaces
{
    [ComImport]
    [Guid("07E72D33-D94E-4be7-8843-60AE5FF7E5F5")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMProfile2 : IWMProfile
    {
        //IWMProfile
        new void GetVersion([Out] out WMT_VERSION pdwVersion);
        new void GetName([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszName,
                         [In, Out] ref uint pcchName);
        new void SetName([In, MarshalAs(UnmanagedType.LPWStr)] string pwszName);
        new void GetDescription([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszDescription,
                                [In, Out] ref uint pcchDescription);
        new void SetDescription([In, MarshalAs(UnmanagedType.LPWStr)] string pwszDescription);
        new void GetStreamCount([Out] out uint pcStreams);
        new void GetStream([In] uint dwStreamIndex, [Out, MarshalAs(UnmanagedType.Interface)] out IWMStreamConfig ppConfig);
        new void GetStreamByNumber([In] ushort wStreamNum, [Out, MarshalAs(UnmanagedType.Interface)] out IWMStreamConfig ppConfig);
        new void RemoveStream([In, MarshalAs(UnmanagedType.Interface)] IWMStreamConfig pConfig);
        new void RemoveStreamByNumber([In] ushort wStreamNum);
        new void AddStream([In, MarshalAs(UnmanagedType.Interface)] IWMStreamConfig pConfig);
        new void ReconfigStream([In, MarshalAs(UnmanagedType.Interface)] IWMStreamConfig pConfig);
        new void CreateNewStream([In] ref Guid guidStreamType,
                                 [Out, MarshalAs(UnmanagedType.Interface)] out IWMStreamConfig ppConfig);
        new void GetMutualExclusionCount([Out] out uint pcME);
        new void GetMutualExclusion([In] uint dwMEIndex,
                                    [Out, MarshalAs(UnmanagedType.Interface)] out IWMMutualExclusion ppME);
        new void RemoveMutualExclusion([In, MarshalAs(UnmanagedType.Interface)] IWMMutualExclusion pME);
        new void AddMutualExclusion([In, MarshalAs(UnmanagedType.Interface)] IWMMutualExclusion pME);
        new void CreateNewMutualExclusion([Out, MarshalAs(UnmanagedType.Interface)] out IWMMutualExclusion ppME);
        //IWMProfile2
        void GetProfileID([Out] out Guid pguidID);
    }
}
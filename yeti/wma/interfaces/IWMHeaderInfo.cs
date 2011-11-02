using System;
using System.Runtime.InteropServices;
using System.Text;
using yeti.wma.enums;

namespace yeti.wma.interfaces
{
    [ComImport]
    [Guid("96406BDA-2B2B-11d3-B36B-00C04F6108FF")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMHeaderInfo
    {

        void GetAttributeCount([In] ushort wStreamNum, [Out] out ushort pcAttributes);

        void GetAttributeByIndex([In] ushort wIndex,
                                 [In, Out] ref ushort pwStreamNum,
                                 [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszName,
                                 [In, Out] ref ushort pcchNameLen,
                                 [Out] out WMT_ATTR_DATATYPE pType,
                                 IntPtr pValue,
                                 [In, Out] ref ushort pcbLength);

        void GetAttributeByName([In, Out] ref ushort pwStreamNum,
                                [In, MarshalAs(UnmanagedType.LPWStr)] string pszName,
                                [Out] out WMT_ATTR_DATATYPE pType,
                                IntPtr pValue,
                                [In, Out] ref ushort pcbLength);

        void SetAttribute([In] ushort wStreamNum,
                          [In, MarshalAs(UnmanagedType.LPWStr)] string pszName,
                          [In] WMT_ATTR_DATATYPE Type,
                          IntPtr pValue,
                          [In] ushort cbLength);

        void GetMarkerCount([Out] out ushort pcMarkers);

        void GetMarker([In] ushort wIndex,
                       [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszMarkerName,
                       [In, Out] ref ushort pcchMarkerNameLen,
                       [Out] out ulong pcnsMarkerTime);

        void AddMarker([In, MarshalAs(UnmanagedType.LPWStr)] string pwszMarkerName,
                       [In] ulong cnsMarkerTime);

        void RemoveMarker([In] ushort wIndex);

        void GetScriptCount([Out] out ushort pcScripts);

        void GetScript([In] ushort wIndex,
                       [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszType,
                       [In, Out] ref ushort pcchTypeLen,
                       [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszCommand,
                       [In, Out] ref ushort pcchCommandLen,
                       [Out] out ulong pcnsScriptTime);

        void AddScript([In, MarshalAs(UnmanagedType.LPWStr)] string pwszType,
                       [In, MarshalAs(UnmanagedType.LPWStr)] string pwszCommand,
                       [In] ulong cnsScriptTime);

        void RemoveScript([In] ushort wIndex);
    }
}
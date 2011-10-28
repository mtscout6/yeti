using System;
using System.Runtime.InteropServices;
using System.Text;
using yeti.wma.enums;

namespace yeti.wma.interfaces
{
    [ComImport]
    [Guid("15CC68E3-27CC-4ecd-B222-3F5D02D80BD5")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMHeaderInfo3 : IWMHeaderInfo2
    {
        //IWMHeaderInfo
        new void GetAttributeCount([In] ushort wStreamNum, [Out] out ushort pcAttributes);
        new void GetAttributeByIndex([In] ushort wIndex,
                                     [In, Out] ref ushort pwStreamNum,
                                     [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszName,
                                     [In, Out] ref ushort pcchNameLen,
                                     [Out] out WMT_ATTR_DATATYPE pType,
                                     IntPtr pValue,
                                     [In, Out] ref ushort pcbLength);
        new void GetAttributeByName([In, Out] ref ushort pwStreamNum,
                                    [In, MarshalAs(UnmanagedType.LPWStr)] string pszName,
                                    [Out] out WMT_ATTR_DATATYPE pType,
                                    IntPtr pValue,
                                    [In, Out] ref ushort pcbLength);
        new void SetAttribute([In] ushort wStreamNum,
                              [In, MarshalAs(UnmanagedType.LPWStr)] string pszName,
                              [In] WMT_ATTR_DATATYPE Type,
                              IntPtr pValue,
                              [In] ushort cbLength);
        new void GetMarkerCount([Out] out ushort pcMarkers);
        new void GetMarker([In] ushort wIndex,
                           [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszMarkerName,
                           [In, Out] ref ushort pcchMarkerNameLen,
                           [Out] out ulong pcnsMarkerTime);
        new void AddMarker([In, MarshalAs(UnmanagedType.LPWStr)] string pwszMarkerName,
                           [In] ulong cnsMarkerTime);
        new void RemoveMarker([In] ushort wIndex);
        new void GetScriptCount([Out] out ushort pcScripts);
        new void GetScript([In] ushort wIndex,
                           [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszType,
                           [In, Out] ref ushort pcchTypeLen,
                           [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszCommand,
                           [In, Out] ref ushort pcchCommandLen,
                           [Out] out ulong pcnsScriptTime);
        new void AddScript([In, MarshalAs(UnmanagedType.LPWStr)] string pwszType,
                           [In, MarshalAs(UnmanagedType.LPWStr)] string pwszCommand,
                           [In] ulong cnsScriptTime);
        new void RemoveScript([In] ushort wIndex);
        //IWMHeaderInfo2
        new void GetCodecInfoCount([Out] out uint pcCodecInfos);
        new void GetCodecInfo([In] uint wIndex,
                              [In, Out] ref ushort pcchName,
                              [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszName,
                              [In, Out] ref ushort pcchDescription,
                              [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszDescription,
                              [Out] out WMT_CODEC_INFO_TYPE pCodecType,
                              [In, Out] ref ushort pcbCodecInfo,
                              [Out, MarshalAs(UnmanagedType.LPArray)] byte[] pbCodecInfo);
        //IWMHeaderInfo3
        void GetAttributeCountEx([In] ushort wStreamNum, [Out] out ushort pcAttributes);

        void GetAttributeIndices([In] ushort wStreamNum,
                                 [In, MarshalAs(UnmanagedType.LPWStr)] string pwszName,
                                 /* DWORD* */IntPtr pwLangIndex,
                                 [Out, MarshalAs(UnmanagedType.LPArray)] ushort[] pwIndices,
                                 [In, Out] ref ushort pwCount);

        void GetAttributeByIndexEx([In] ushort wStreamNum,
                                   [In] ushort wIndex,
                                   [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszName,
                                   [In, Out] ref ushort pwNameLen,
                                   [Out] out WMT_ATTR_DATATYPE pType,
                                   [Out] out ushort pwLangIndex,
                                   IntPtr pValue,
                                   [In, Out] ref uint pdwDataLength);

        void ModifyAttribute([In] ushort wStreamNum,
                             [In] ushort wIndex,
                             [In] WMT_ATTR_DATATYPE Type,
                             [In] ushort wLangIndex,
                             IntPtr pValue,
                             [In] uint dwLength);

        void AddAttribute([In] ushort wStreamNum,
                          [In, MarshalAs(UnmanagedType.LPWStr)] string pszName,
                          [Out] out ushort pwIndex,
                          [In] WMT_ATTR_DATATYPE Type,
                          [In] ushort wLangIndex,
                          IntPtr pValue,
                          [In] uint dwLength);

        void DeleteAttribute([In] ushort wStreamNum, [In] ushort wIndex);

        void AddCodecInfo([In, MarshalAs(UnmanagedType.LPWStr)] string pwszName,
                          [In, MarshalAs(UnmanagedType.LPWStr)] string pwszDescription,
                          [In] WMT_CODEC_INFO_TYPE codecType,
                          [In] ushort cbCodecInfo,
                          [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] pbCodecInfo);
    }
}
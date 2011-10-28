using System;
using System.Runtime.InteropServices;
using System.Text;
using yeti.wma.enums;

namespace yeti.wma.interfaces
{
    [ComImport]
    [Guid("7688D8CB-FC0D-43BD-9459-5A8DEC200CFA")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMStreamConfig2 : IWMStreamConfig
    {
        //IWMStreamConfig
        new void GetStreamType([Out] out Guid pguidStreamType);
        new void GetStreamNumber([Out] out ushort pwStreamNum);
        new void SetStreamNumber([In] ushort wStreamNum);
        new void GetStreamName([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszStreamName,
                               [In, Out] ref ushort pcchStreamName);
        new void SetStreamName([In, MarshalAs(UnmanagedType.LPWStr)] string pwszStreamName);
        new void GetConnectionName([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszInputName,
                                   [In, Out] ref ushort pcchInputName);
        new void SetConnectionName([In, MarshalAs(UnmanagedType.LPWStr)] string pwszInputName);
        new void GetBitrate([Out] out uint pdwBitrate);
        new void SetBitrate([In] uint pdwBitrate);
        new void GetBufferWindow([Out] out uint pmsBufferWindow);
        new void SetBufferWindow([In] uint msBufferWindow);
        //IWMStreamConfig2
        void GetTransportType([Out] out WMT_TRANSPORT_TYPE pnTransportType);
        void SetTransportType([In] WMT_TRANSPORT_TYPE nTransportType);
        void AddDataUnitExtension([In] Guid guidExtensionSystemID,
                                  [In] ushort cbExtensionDataSize,
                                  [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] pbExtensionSystemInfo,
                                  [In] uint cbExtensionSystemInfo);

        void GetDataUnitExtensionCount([Out] out ushort pcDataUnitExtensions);
        void GetDataUnitExtension([In] uint wDataUnitExtensionNumber,
                                  [Out] out Guid pguidExtensionSystemID,
                                  [Out] out ushort pcbExtensionDataSize,
                                  /*[out, size_is( *pcbExtensionSystemInfo )]*/ IntPtr pbExtensionSystemInfo,
                                  [In, Out] ref uint pcbExtensionSystemInfo);
        void RemoveAllDataUnitExtensions();
    }
}
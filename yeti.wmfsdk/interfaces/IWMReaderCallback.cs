using System;
using System.Runtime.InteropServices;
using yeti.wma.enums;

namespace yeti.wma.interfaces
{
    [ComImport]
    [Guid("96406BD8-2B2B-11d3-B36B-00C04F6108FF")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMReaderCallback : IWMStatusCallback
    {
        //IWMStatusCallback
        new void OnStatus([In] WMT_STATUS Status,
                          [In] IntPtr hr,
                          [In] WMT_ATTR_DATATYPE dwType,
                          [In] IntPtr pValue,
                          [In] IntPtr pvContext);
        //IWMReaderCallback
        void OnSample([In] uint dwOutputNum,
                      [In] ulong cnsSampleTime,
                      [In] ulong cnsSampleDuration,
                      [In] uint dwFlags,
                      [In, MarshalAs(UnmanagedType.Interface)] INSSBuffer pSample,
                      [In] IntPtr pvContext);
    }
}
using System;
using System.Runtime.InteropServices;
using yeti.wma.enums;

namespace yeti.wma.interfaces
{
    [ComImport]
    [Guid("6d7cdc70-9888-11d3-8edc-00c04f6109cf")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMStatusCallback
    {

        void OnStatus([In] WMT_STATUS Status,
                      [In] IntPtr hr,
                      [In] WMT_ATTR_DATATYPE dwType,
                      [In] IntPtr pValue,
                      [In] IntPtr pvContext);
    }
}
using System;
using System.Runtime.InteropServices;
using yeti.wma.enums;

namespace yeti.wma.interfaces
{
    [ComImport]
    [Guid("B70F1E42-6255-4df0-A6B9-02B212D9E2BB")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMIndexer2 : IWMIndexer
    {
        //IWMIndexer
        new void StartIndexing([In, MarshalAs(UnmanagedType.LPWStr)] string pwszURL,
                               [In, MarshalAs(UnmanagedType.Interface)] IWMStatusCallback pCallback,
                               [In] IntPtr pvContext);
        new void Cancel();
        //IWMIndexer2
        void Configure([In] ushort wStreamNum,
                       [In] WMT_INDEXER_TYPE nIndexerType,
                       [In] IntPtr pvInterval,
                       [In] IntPtr pvIndexType);
    }
}
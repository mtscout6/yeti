using System;
using System.Runtime.InteropServices;

namespace yeti.wma.interfaces
{
    [ComImport]
    [Guid("6d7cdc71-9888-11d3-8edc-00c04f6109cf")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMIndexer
    {

        void StartIndexing([In, MarshalAs(UnmanagedType.LPWStr)] string pwszURL,
                           [In, MarshalAs(UnmanagedType.Interface)] IWMStatusCallback pCallback,
                           [In] IntPtr pvContext);

        void Cancel();
    }
}
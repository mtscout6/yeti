using System.Runtime.InteropServices;

namespace yeti.wma.interfaces
{
    [ComImport]
    [Guid("96406BE4-2B2B-11d3-B36B-00C04F6108FF")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMWriterSink
    {
        void OnHeader([In, MarshalAs(UnmanagedType.Interface)] INSSBuffer pHeader);
        void IsRealTime([Out, MarshalAs(UnmanagedType.Bool)] out bool pfRealTime);
        void AllocateDataUnit([In] uint cbDataUnit,
                              [Out, MarshalAs(UnmanagedType.Interface)] out INSSBuffer ppDataUnit);
        void OnDataUnit([In, MarshalAs(UnmanagedType.Interface)] INSSBuffer pDataUnit);
        void OnEndWriting();
    }
}
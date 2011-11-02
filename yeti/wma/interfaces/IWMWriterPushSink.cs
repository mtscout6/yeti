using System.Runtime.InteropServices;

namespace yeti.wma.interfaces
{
    [ComImport]
    [Guid("dc10e6a5-072c-467d-bf57-6330a9dde12a")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMWriterPushSink : IWMWriterSink
    {
        //IWMWriterSink
        new void OnHeader([In, MarshalAs(UnmanagedType.Interface)] INSSBuffer pHeader);
        new void IsRealTime([Out, MarshalAs(UnmanagedType.Bool)] out bool pfRealTime);
        new void AllocateDataUnit([In] uint cbDataUnit,
                                  [Out, MarshalAs(UnmanagedType.Interface)] out INSSBuffer ppDataUnit);
        new void OnDataUnit([In, MarshalAs(UnmanagedType.Interface)] INSSBuffer pDataUnit);
        new void OnEndWriting();
        //IWMWriterPushSink
        void Connect([In, MarshalAs(UnmanagedType.LPWStr)] string pwszURL,
                     [In, MarshalAs(UnmanagedType.LPWStr)] string pwszTemplateURL,
                     [In, MarshalAs(UnmanagedType.Bool)] bool fAutoDestroy);
        void Disconnect();
        void EndSession();
    }
}
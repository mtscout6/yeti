using System.Runtime.InteropServices;

namespace yeti.wma.interfaces
{
    [ComImport]
    [Guid("14282BA7-4AEF-4205-8CE5-C229035A05BC")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMWriterFileSink2 : IWMWriterFileSink
    {
        //IWMWriterSink
        new void OnHeader([In, MarshalAs(UnmanagedType.Interface)] INSSBuffer pHeader);
        new void IsRealTime([Out, MarshalAs(UnmanagedType.Bool)] out bool pfRealTime);
        new void AllocateDataUnit([In] uint cbDataUnit,
                                  [Out, MarshalAs(UnmanagedType.Interface)] out INSSBuffer ppDataUnit);
        new void OnDataUnit([In, MarshalAs(UnmanagedType.Interface)] INSSBuffer pDataUnit);
        new void OnEndWriting();
        //IWMWriterFileSink
        new void Open([In, MarshalAs(UnmanagedType.LPWStr)] string pwszFilename);
        //IWMWriterFileSink2
        void Start([In] ulong cnsStartTime);
        void Stop([In] ulong cnsStopTime);
        void IsStopped([Out, MarshalAs(UnmanagedType.Bool)] out bool pfStopped);
        void GetFileDuration([Out] out ulong pcnsDuration);
        void GetFileSize([Out] out ulong pcbFile);
        void Close();
        void IsClosed([Out, MarshalAs(UnmanagedType.Bool)] out bool pfClosed);
    }
}
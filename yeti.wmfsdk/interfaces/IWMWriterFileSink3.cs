using System.Runtime.InteropServices;
using yeti.wma.structs;

namespace yeti.wma.interfaces
{
    [ComImport]
    [Guid("3FEA4FEB-2945-47A7-A1DD-C53A8FC4C45C")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMWriterFileSink3 : IWMWriterFileSink2
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
        new void Start([In] ulong cnsStartTime);
        new void Stop([In] ulong cnsStopTime);
        new void IsStopped([Out, MarshalAs(UnmanagedType.Bool)] out bool pfStopped);
        new void GetFileDuration([Out] out ulong pcnsDuration);
        new void GetFileSize([Out] out ulong pcbFile);
        new void Close();
        new void IsClosed([Out, MarshalAs(UnmanagedType.Bool)] out bool pfClosed);
        //IWMWriterFileSink3
        void SetAutoIndexing([In, MarshalAs(UnmanagedType.Bool)] bool fDoAutoIndexing);
        void GetAutoIndexing([Out, MarshalAs(UnmanagedType.Bool)] out bool pfAutoIndexing);
        void SetControlStream([In] ushort wStreamNumber,
                              [In, MarshalAs(UnmanagedType.Bool)] bool fShouldControlStartAndStop);
        void GetMode([Out] out uint pdwFileSinkMode);
        void OnDataUnitEx([In] ref WMT_FILESINK_DATA_UNIT pFileSinkDataUnit);
        void SetUnbufferedIO([In, MarshalAs(UnmanagedType.Bool)] bool fUnbufferedIO,
                             [In, MarshalAs(UnmanagedType.Bool)] bool fRestrictMemUsage);
        void GetUnbufferedIO([Out, MarshalAs(UnmanagedType.Bool)] out bool pfUnbufferedIO);
        void CompleteOperations();
    }
}
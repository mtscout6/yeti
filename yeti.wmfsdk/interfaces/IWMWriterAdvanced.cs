using System.Runtime.InteropServices;
using yeti.wma.structs;

namespace yeti.wma.interfaces
{
    [ComImport]
    [Guid("96406BE3-2B2B-11d3-B36B-00C04F6108FF")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMWriterAdvanced
    {
        void GetSinkCount([Out] out uint pcSinks);

        void GetSink([In] uint dwSinkNum,
                     [Out, MarshalAs(UnmanagedType.Interface)] out IWMWriterSink ppSink);
        void AddSink([In, MarshalAs(UnmanagedType.Interface)] IWMWriterSink pSink);
        void RemoveSink([In, MarshalAs(UnmanagedType.Interface)] IWMWriterSink pSink);
        void WriteStreamSample([In] ushort wStreamNum,
                               [In] ulong cnsSampleTime,
                               [In] uint msSampleSendTime,
                               [In] ulong cnsSampleDuration,
                               [In] uint dwFlags,
                               [In, MarshalAs(UnmanagedType.Interface)] INSSBuffer pSample);
        void SetLiveSource([MarshalAs(UnmanagedType.Bool)]bool fIsLiveSource);
        void IsRealTime([Out, MarshalAs(UnmanagedType.Bool)] out bool pfRealTime);
        void GetWriterTime([Out] out ulong pcnsCurrentTime);
        void GetStatistics([In] ushort wStreamNum,
                           [Out] out WM_WRITER_STATISTICS pStats);
        void SetSyncTolerance([In] uint msWindow);
        void GetSyncTolerance([Out] out uint pmsWindow);
    }
}
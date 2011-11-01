using System.Runtime.InteropServices;
using yeti.wma.enums;
using yeti.wma.structs;

namespace yeti.wma.interfaces
{
    [ComImport]
    [Guid("2cd6492d-7c37-4e76-9d3b-59261183a22e")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMWriterAdvanced3 : IWMWriterAdvanced2
    {
        //IWMWriterAdvanced
        new void GetSinkCount([Out] out uint pcSinks);
        new void GetSink([In] uint dwSinkNum,
                         [Out, MarshalAs(UnmanagedType.Interface)] out IWMWriterSink ppSink);
        new void AddSink([In, MarshalAs(UnmanagedType.Interface)] IWMWriterSink pSink);
        new void RemoveSink([In, MarshalAs(UnmanagedType.Interface)] IWMWriterSink pSink);
        new void WriteStreamSample([In] ushort wStreamNum,
                                   [In] ulong cnsSampleTime,
                                   [In] uint msSampleSendTime,
                                   [In] ulong cnsSampleDuration,
                                   [In] uint dwFlags,
                                   [In, MarshalAs(UnmanagedType.Interface)] INSSBuffer pSample);
        new void SetLiveSource([MarshalAs(UnmanagedType.Bool)]bool fIsLiveSource);
        new void IsRealTime([Out, MarshalAs(UnmanagedType.Bool)] out bool pfRealTime);
        new void GetWriterTime([Out] out ulong pcnsCurrentTime);
        new void GetStatistics([In] ushort wStreamNum,
                               [Out] out WM_WRITER_STATISTICS pStats);
        new void SetSyncTolerance([In] uint msWindow);
        new void GetSyncTolerance([Out] out uint pmsWindow);
        //IWMWriterAdvanced2
        new void GetInputSetting([In] uint dwInputNum,
                                 [In, MarshalAs(UnmanagedType.LPWStr)] string pszName,
                                 [Out] out WMT_ATTR_DATATYPE pType,
                                 [Out, MarshalAs(UnmanagedType.LPArray)] byte[] pValue,
                                 [In, Out] ref ushort pcbLength);
        new void SetInputSetting([In] uint dwInputNum,
                                 [In, MarshalAs(UnmanagedType.LPWStr)] string pszName,
                                 [In] WMT_ATTR_DATATYPE Type,
                                 [In, MarshalAs(UnmanagedType.LPWStr, SizeParamIndex = 4)] byte[] pValue,
                                 [In] ushort cbLength);
        //IWMWriterAdvanced3
        void GetStatisticsEx([In] ushort wStreamNum,
                             [Out] out WM_WRITER_STATISTICS_EX pStats);
        void SetNonBlocking();
    };
}
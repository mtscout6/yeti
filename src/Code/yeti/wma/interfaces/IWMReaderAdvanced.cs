using System.Runtime.InteropServices;
using yeti.wma.enums;
using yeti.wma.structs;

namespace yeti.wma.interfaces
{
    [ComImport]
    [Guid("96406BEA-2B2B-11d3-B36B-00C04F6108FF")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMReaderAdvanced
    {
        void SetUserProvidedClock([In, MarshalAs(UnmanagedType.Bool)] bool fUserClock);
        void GetUserProvidedClock([Out, MarshalAs(UnmanagedType.Bool)] out bool pfUserClock);
        void DeliverTime([In] ulong cnsTime);
        void SetManualStreamSelection([In, MarshalAs(UnmanagedType.Bool)] bool fSelection);
        void GetManualStreamSelection([Out, MarshalAs(UnmanagedType.Bool)] out bool pfSelection);
        void SetStreamsSelected([In] ushort cStreamCount,
                                [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] ushort[] pwStreamNumbers,
                                [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] WMT_STREAM_SELECTION[] pSelections);
        void GetStreamSelected([In] ushort wStreamNum, [Out] out WMT_STREAM_SELECTION pSelection);
        void SetReceiveSelectionCallbacks([In, MarshalAs(UnmanagedType.Bool)] bool fGetCallbacks);
        void GetReceiveSelectionCallbacks([Out, MarshalAs(UnmanagedType.Bool)] out bool pfGetCallbacks);
        void SetReceiveStreamSamples([In] ushort wStreamNum, [In, MarshalAs(UnmanagedType.Bool)] bool fReceiveStreamSamples);
        void GetReceiveStreamSamples([In] ushort wStreamNum, [Out, MarshalAs(UnmanagedType.Bool)] out bool pfReceiveStreamSamples);
        void SetAllocateForOutput([In] uint dwOutputNum, [In, MarshalAs(UnmanagedType.Bool)] bool fAllocate);
        void GetAllocateForOutput([In] uint dwOutputNum, [Out, MarshalAs(UnmanagedType.Bool)] out bool pfAllocate);
        void SetAllocateForStream([In] ushort wStreamNum, [In, MarshalAs(UnmanagedType.Bool)] bool fAllocate);
        void GetAllocateForStream([In] ushort dwSreamNum, [Out, MarshalAs(UnmanagedType.Bool)] out bool pfAllocate);
        void GetStatistics([In, Out] ref WM_READER_STATISTICS pStatistics);
        void SetClientInfo([In] ref WM_READER_CLIENTINFO pClientInfo);
        void GetMaxOutputSampleSize([In] uint dwOutput, [Out] out uint pcbMax);
        void GetMaxStreamSampleSize([In] ushort wStream, [Out] out uint pcbMax);
        void NotifyLateDelivery(ulong cnsLateness);
    }
}
using System.Runtime.InteropServices;
using yeti.wma.structs;

namespace yeti.wma.interfaces
{
    [ComImport]
    [Guid("8C1C6090-F9A8-4748-8EC3-DD1108BA1E77")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMStreamPrioritization
    {

        void GetPriorityRecords([Out, MarshalAs(UnmanagedType.LPArray)] WM_STREAM_PRIORITY_RECORD[] pRecordArray,
                                [In, Out] ref ushort pcRecords);

        void SetPriorityRecords([In, MarshalAs(UnmanagedType.LPArray)] WM_STREAM_PRIORITY_RECORD[] pRecordArray,
                                [In] ushort cRecords);
    }
}
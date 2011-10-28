using System.Runtime.InteropServices;

namespace yeti.wma.interfaces
{
    [ComImport]
    [Guid("96406BDD-2B2B-11d3-B36B-00C04F6108FF")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMStreamList
    {

        void GetStreams([Out, MarshalAs(UnmanagedType.LPArray)] ushort[] pwStreamNumArray,
                        [In, Out] ref ushort pcStreams);

        void AddStream([In] ushort wStreamNum);

        void RemoveStream([In] ushort wStreamNum);
    };
}
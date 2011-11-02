using System.Runtime.InteropServices;
using System.Text;
using yeti.wma.enums;

namespace yeti.wma.interfaces
{
    [ComImport]
    [Guid("96406BE7-2B2B-11d3-B36B-00C04F6108FF")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMWriterNetworkSink : IWMWriterSink
    {
        //IWMWriterSink
        new void OnHeader([In, MarshalAs(UnmanagedType.Interface)] INSSBuffer pHeader);
        new void IsRealTime([Out, MarshalAs(UnmanagedType.Bool)] out bool pfRealTime);
        new void AllocateDataUnit([In] uint cbDataUnit,
                                  [Out, MarshalAs(UnmanagedType.Interface)] out INSSBuffer ppDataUnit);
        new void OnDataUnit([In, MarshalAs(UnmanagedType.Interface)] INSSBuffer pDataUnit);
        new void OnEndWriting();
        //IWMWriterNetworkSink
        void SetMaximumClients([In] uint dwMaxClients);
        void GetMaximumClients([Out] out uint pdwMaxClients);
        void SetNetworkProtocol([In] WMT_NET_PROTOCOL protocol);
        void GetNetworkProtocol([Out] out WMT_NET_PROTOCOL pProtocol);
        void GetHostURL([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszURL,
                        [In, Out] ref uint pcchURL);
        void Open([In, Out] ref uint pdwPortNum);
        void Disconnect();
        void Close();
    }
}
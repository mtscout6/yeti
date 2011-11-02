using System;
using System.Runtime.InteropServices;

namespace yeti.wma.interfaces
{
    [ComImport]
    [Guid("AD694AF1-F8D9-42F8-BC47-70311B0C4F9E")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMBandwidthSharing : IWMStreamList
    {
        //IWMStreamList
        new void GetStreams([Out, MarshalAs(UnmanagedType.LPArray)] ushort[] pwStreamNumArray,
                            [In, Out] ref ushort pcStreams);
        new void AddStream([In] ushort wStreamNum);
        new void RemoveStream([In] ushort wStreamNum);
        //IWMBandwidthSharing    
        void GetType([Out] out Guid pguidType);

        void SetType([In] ref Guid guidType);

        void GetBandwidth([Out] out uint pdwBitrate, [Out] out uint pmsBufferWindow);

        void SetBandwidth([In] uint dwBitrate, [In] uint msBufferWindow);
    }
}
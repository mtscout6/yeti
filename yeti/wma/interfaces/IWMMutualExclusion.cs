using System;
using System.Runtime.InteropServices;

namespace yeti.wma.interfaces
{
    [ComImport]
    [Guid("96406BDE-2B2B-11d3-B36B-00C04F6108FF")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMMutualExclusion : IWMStreamList
    {
        //IWMStreamList
        new void GetStreams([Out, MarshalAs(UnmanagedType.LPArray)] ushort[] pwStreamNumArray,
                            [In, Out] ref ushort pcStreams);
        new void AddStream([In] ushort wStreamNum);
        new void RemoveStream([In] ushort wStreamNum);
        //IWMMutualExclusion
        void GetType([Out] out Guid pguidType);

        void SetType([In] ref Guid guidType);
    };
}
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace yeti.wma.interfaces
{
    [ComImport]
    [Guid("0302B57D-89D1-4ba2-85C9-166F2C53EB91")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMMutualExclusion2 : IWMMutualExclusion
    {
        //IWMStreamList
        new void GetStreams([Out, MarshalAs(UnmanagedType.LPArray)] ushort[] pwStreamNumArray,
                            [In, Out] ref ushort pcStreams);
        new void AddStream([In] ushort wStreamNum);
        new void RemoveStream([In] ushort wStreamNum);
        //IWMMutualExclusion
        new void GetType([Out] out Guid pguidType);
        new void SetType([In] ref Guid guidType);
        //IWMMutualExclusion2
        void GetName([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszName,
                     [In, Out] ref ushort pcchName);

        void SetName([In, MarshalAs(UnmanagedType.LPWStr)] string pwszName);

        void GetRecordCount([Out] out ushort pwRecordCount);

        void AddRecord();

        void RemoveRecord([In] ushort wRecordNumber);

        void GetRecordName([In] ushort wRecordNumber,
                           [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszRecordName,
                           [In, Out] ref ushort pcchRecordName);

        void SetRecordName([In] ushort wRecordNumber,
                           [In, MarshalAs(UnmanagedType.LPWStr)] string pwszRecordName);

        void GetStreamsForRecord([In] ushort wRecordNumber,
                                 [Out, MarshalAs(UnmanagedType.LPArray)] ushort[] pwStreamNumArray,
                                 [In, Out] ref ushort pcStreams);

        void AddStreamForRecord([In] ushort wRecordNumber, [In] ushort wStreamNumber);

        void RemoveStreamForRecord([In] ushort wRecordNumber, [In] ushort wStreamNumber);
    }
}
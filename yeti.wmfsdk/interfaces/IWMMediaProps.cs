using System;
using System.Runtime.InteropServices;
using yeti.wma.structs;

namespace yeti.wma.interfaces
{
    [ComImport]
    [Guid("96406BCE-2B2B-11d3-B36B-00C04F6108FF")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMMediaProps
    {

        void GetType([Out] out Guid pguidType);

        void GetMediaType( /*[out] WM_MEDIA_TYPE* */ IntPtr pType,
                                                     [In, Out] ref uint pcbType);

        void SetMediaType([In] ref WM_MEDIA_TYPE pType);
    }
}
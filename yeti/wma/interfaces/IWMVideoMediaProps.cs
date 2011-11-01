using System;
using System.Runtime.InteropServices;
using yeti.wma.structs;

namespace yeti.wma.interfaces
{
    [ComImport]
    [Guid("96406BCF-2B2B-11d3-B36B-00C04F6108FF")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMVideoMediaProps : IWMMediaProps
    {
        //IWMMediaProps
        new void GetType([Out] out Guid pguidType);
        new void GetMediaType( /*[out] WM_MEDIA_TYPE* */ IntPtr pType,
                                                         [In, Out] ref uint pcbType);
        new void SetMediaType([In] ref WM_MEDIA_TYPE pType);
        //IWMVideoMediaProps
        void GetMaxKeyFrameSpacing([Out] out long pllTime);

        void SetMaxKeyFrameSpacing([In] long llTime);

        void GetQuality([Out] out uint pdwQuality);

        void SetQuality([In] uint dwQuality);
    }
}
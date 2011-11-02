using System;
using System.Runtime.InteropServices;

namespace yeti.wma.interfaces
{
    [ComImport]
    [Guid("B6B8FD5A-32E2-49d4-A910-C26CC85465ED")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface INSSBuffer4 : INSSBuffer3
    {
        //INSSBuffer
        new void GetLength([Out] out uint pdwLength);
        new void SetLength([In] uint dwLength);
        new void GetMaxLength([Out] out uint pdwLength);
        new void GetBuffer([Out] out IntPtr ppdwBuffer);
        new void GetBufferAndLength([Out] out IntPtr ppdwBuffer, [Out] out uint pdwLength);
        //INSSBuffer2
        new void GetSampleProperties([In] uint cbProperties, [Out] out byte pbProperties);
        new void SetSampleProperties([In] uint cbProperties, [In] ref byte pbProperties);
        //INSSBuffer3
        new void SetProperty([In] Guid guidBufferProperty,
                             [In] IntPtr pvBufferProperty,
                             [In] uint dwBufferPropertySize);
        new void GetProperty([In] Guid guidBufferProperty,
                             /*out]*/ IntPtr pvBufferProperty,
                             [In, Out] ref uint pdwBufferPropertySize);
        //INSSBuffer4
        void GetPropertyCount([Out] out uint pcBufferProperties);

        void GetPropertyByIndex([In] uint dwBufferPropertyIndex,
                                [Out] out Guid pguidBufferProperty,
                                /*[out]*/   IntPtr pvBufferProperty,
                                [In, Out] ref uint pdwBufferPropertySize);
    }
}
using System;
using System.Runtime.InteropServices;

namespace yeti.wma.interfaces
{
    [ComImport]
    [Guid("C87CEAAF-75BE-4bc4-84EB-AC2798507672")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface INSSBuffer3 : INSSBuffer2
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
        void SetProperty([In] Guid guidBufferProperty,
                         [In] IntPtr pvBufferProperty,
                         [In] uint dwBufferPropertySize);

        void GetProperty([In] Guid guidBufferProperty,
                         /*out]*/ IntPtr pvBufferProperty,
                         [In, Out] ref uint pdwBufferPropertySize);
    }
}
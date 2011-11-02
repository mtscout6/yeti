using System;
using System.Runtime.InteropServices;

namespace yeti.wma.interfaces
{
    [ComImport]
    [Guid("4F528693-1035-43fe-B428-757561AD3A68")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface INSSBuffer2 : INSSBuffer
    {
        //INSSBuffer
        new void GetLength([Out] out uint pdwLength);
        new void SetLength([In] uint dwLength);
        new void GetMaxLength([Out] out uint pdwLength);
        new void GetBuffer([Out] out IntPtr ppdwBuffer);
        new void GetBufferAndLength([Out] out IntPtr ppdwBuffer, [Out] out uint pdwLength);
        //INSSBuffer2
        void GetSampleProperties([In] uint cbProperties, [Out] out byte pbProperties);

        void SetSampleProperties([In] uint cbProperties, [In] ref byte pbProperties);
    };
}
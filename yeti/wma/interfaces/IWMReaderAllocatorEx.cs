using System;
using System.Runtime.InteropServices;

namespace yeti.wma.interfaces
{
    [ComImport]
    [Guid("9F762FA7-A22E-428d-93C9-AC82F3AAFE5A")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMReaderAllocatorEx
    {
        void AllocateForStreamEx([In] ushort wStreamNum,
                                 [In] uint cbBuffer,
                                 [Out] out INSSBuffer ppBuffer,
                                 [In] uint dwFlags,
                                 [In] ulong cnsSampleTime,
                                 [In] ulong cnsSampleDuration,
                                 [In] IntPtr pvContext);

        void AllocateForOutputEx([In] uint dwOutputNum,
                                 [In] uint cbBuffer,
                                 [Out] out INSSBuffer ppBuffer,
                                 [In] uint dwFlags,
                                 [In] ulong cnsSampleTime,
                                 [In] ulong cnsSampleDuration,
                                 [In] IntPtr pvContext);
    }
}
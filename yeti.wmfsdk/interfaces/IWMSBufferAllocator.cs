using System.Runtime.InteropServices;

namespace yeti.wma.interfaces
{
    [ComImport]
    [Guid("61103CA4-2033-11d2-9EF1-006097D2D7CF")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMSBufferAllocator
    {

        void AllocateBuffer([In] uint dwMaxBufferSize,
                            [Out, MarshalAs(UnmanagedType.Interface)] out INSSBuffer ppBuffer);

        void AllocatePageSizeBuffer([In] uint dwMaxBufferSize,
                                    [Out, MarshalAs(UnmanagedType.Interface)] out INSSBuffer ppBuffer);
    };
}
using System.Runtime.InteropServices;

namespace yeti.wma.interfaces
{
    [Guid("203CFFE3-2E18-4fdf-B59D-6E71530534CF")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMMetadataEditor2 : IWMMetadataEditor
    {
        //IWMMetadataEditor
        new void Open([In, MarshalAs(UnmanagedType.LPWStr)] string pwszFilename);
        new void Close();
        new void Flush();
        //IWMMetadataEditor2
        void OpenEx([In, MarshalAs(UnmanagedType.LPWStr)] string pwszFilename,
                    [In] uint dwDesiredAccess,
                    [In] uint dwShareMode);
    }
}
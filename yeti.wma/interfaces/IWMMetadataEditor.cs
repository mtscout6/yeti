using System.Runtime.InteropServices;

namespace yeti.wma.interfaces
{
    [ComImport]
    [Guid("96406BD9-2B2B-11d3-B36B-00C04F6108FF")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMMetadataEditor
    {

        void Open([In, MarshalAs(UnmanagedType.LPWStr)] string pwszFilename);

        void Close();

        void Flush();
    }
}
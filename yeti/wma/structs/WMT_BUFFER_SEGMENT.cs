using System.Runtime.InteropServices;
using yeti.wma.interfaces;

namespace yeti.wma.structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct WMT_BUFFER_SEGMENT
    {
        public INSSBuffer pBuffer;
        public uint cbOffset;
        public uint cbLength;
    };
}
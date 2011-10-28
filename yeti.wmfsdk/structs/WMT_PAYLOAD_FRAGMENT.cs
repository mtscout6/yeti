using System.Runtime.InteropServices;

namespace yeti.wma.structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct WMT_PAYLOAD_FRAGMENT
    {
        public uint dwPayloadIndex;
        public WMT_BUFFER_SEGMENT segmentData;
    };
}
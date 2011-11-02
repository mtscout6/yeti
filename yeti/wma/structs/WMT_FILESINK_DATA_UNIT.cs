using System;
using System.Runtime.InteropServices;

namespace yeti.wma.structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct WMT_FILESINK_DATA_UNIT
    {
        public WMT_BUFFER_SEGMENT packetHeaderBuffer;
        public uint cPayloads;
        /*WMT_BUFFER_SEGMENT* */
        public IntPtr pPayloadHeaderBuffers;
        public uint cPayloadDataFragments;
        /*WMT_PAYLOAD_FRAGMENT* */
        public IntPtr pPayloadDataFragments;
    };
}
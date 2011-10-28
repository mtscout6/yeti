using System.Runtime.InteropServices;

namespace yeti.wma.structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct WM_READER_STATISTICS
    {
        public uint cbSize;
        public uint dwBandwidth;
        public uint cPacketsReceived;
        public uint cPacketsRecovered;
        public uint cPacketsLost;
        public uint wQuality;
    };
}
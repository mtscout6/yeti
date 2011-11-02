using System.Runtime.InteropServices;

namespace yeti.wma.structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct WM_WRITER_STATISTICS_EX
    {
        public uint dwBitratePlusOverhead;
        public uint dwCurrentSampleDropRateInQueue;
        public uint dwCurrentSampleDropRateInCodec;
        public uint dwCurrentSampleDropRateInMultiplexer;
        public uint dwTotalSampleDropsInQueue;
        public uint dwTotalSampleDropsInCodec;
        public uint dwTotalSampleDropsInMultiplexer;
    };
}
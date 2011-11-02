using System;
using System.Runtime.InteropServices;

namespace yeti.mp3.configuration
{
    [StructLayout(LayoutKind.Sequential), Serializable]
    public class BE_CONFIG
    {
        // encoding formats
        public const uint BE_CONFIG_MP3 = 0;
        public const uint BE_CONFIG_LAME = 256;

        public uint dwConfig;
        public Format format;

        public BE_CONFIG(WaveFormat format, uint MpeBitRate)
        {
            this.dwConfig = BE_CONFIG_LAME;
            this.format = new Format(format, MpeBitRate);
        }
        public BE_CONFIG(WaveFormat format)
            : this(format, 128)
        {
        }
    }
}
using System;
using System.Runtime.InteropServices;
using yeti.mp3.structs;

namespace yeti.mp3.configuration
{
    [StructLayout(LayoutKind.Explicit), Serializable]
    public class Format
    {
        [FieldOffset(0)]
        public MP3 mp3;
        [FieldOffset(0)]
        public LHV1 lhv1;
        [FieldOffset(0)]
        public ACC acc;

        public Format(WaveFormat format, uint MpeBitRate)
        {
            lhv1 = new LHV1(format, MpeBitRate);
        }
    }
}
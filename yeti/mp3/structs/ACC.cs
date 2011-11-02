using System;
using System.Runtime.InteropServices;

namespace yeti.mp3.structs
{
    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct ACC
    {
        public uint dwSampleRate;
        public byte byMode;
        public ushort wBitrate;
        public byte byEncodingMethod;
    }
}
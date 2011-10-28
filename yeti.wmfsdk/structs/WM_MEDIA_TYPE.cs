using System;
using System.Runtime.InteropServices;

namespace yeti.wma.structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct WM_MEDIA_TYPE
    {

        public Guid majortype;

        public Guid subtype;

        [MarshalAs(UnmanagedType.Bool)]
        public bool bFixedSizeSamples;

        [MarshalAs(UnmanagedType.Bool)]
        public bool bTemporalCompression;

        public uint lSampleSize;

        public Guid formattype;

        public IntPtr pUnk;

        public uint cbFormat;

        public IntPtr pbFormat;
    };
}
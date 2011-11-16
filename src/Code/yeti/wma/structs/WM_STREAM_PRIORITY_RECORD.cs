using System.Runtime.InteropServices;

namespace yeti.wma.structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct WM_STREAM_PRIORITY_RECORD
    {

        public ushort wStreamNumber;

        [MarshalAs(UnmanagedType.Bool)]
        public bool fMandatory;
    };
}
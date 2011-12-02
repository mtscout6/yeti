using System;

namespace yeti
{
    public static class CPU
    {
        private static readonly bool _is32Bit = IntPtr.Size == 4;
        public static bool Is32Bit 
        {
            get { return _is32Bit; }
        }
    }
}
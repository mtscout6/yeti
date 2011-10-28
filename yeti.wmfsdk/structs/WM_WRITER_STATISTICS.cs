//Widows Media Format Interfaces
//
//  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
//  PURPOSE. IT CAN BE DISTRIBUTED FREE OF CHARGE AS LONG AS THIS HEADER 
//  REMAINS UNCHANGED.
//
//  Email:  yetiicb@hotmail.com
//
//  Copyright (C) 2002-2004 Idael Cardoso. 
//

using System.Runtime.InteropServices;

namespace yeti.wma.structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct WM_WRITER_STATISTICS
    {
        public ulong qwSampleCount;
        public ulong qwByteCount;
        public ulong qwDroppedSampleCount;
        public ulong qwDroppedByteCount;
        public uint dwCurrentBitrate;
        public uint dwAverageBitrate;
        public uint dwExpectedBitrate;
        public uint dwCurrentSampleRate;
        public uint dwAverageSampleRate;
        public uint dwExpectedSampleRate;
    };
}

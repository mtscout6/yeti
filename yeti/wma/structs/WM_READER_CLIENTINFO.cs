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

using System;
using System.Runtime.InteropServices;

namespace yeti.wma.structs
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct WM_READER_CLIENTINFO
    {
        public uint cbSize;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string wszLang;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string wszBrowserUserAgent;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string wszBrowserWebPage;
        ulong qwReserved;
        public IntPtr pReserved;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string wszHostExe;
        public ulong qwHostVersion;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string wszPlayerUserAgent;
    };
}
//
//
//  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
//  PURPOSE. IT CAN BE DISTRIBUTED FREE OF CHARGE AS LONG AS THIS HEADER 
//  REMAINS UNCHANGED.
//
//  Email:  yetiicb@hotmail.com
//
//  Copyright (C) 2002-2003 Idael Cardoso. 
//

using System.Runtime.InteropServices;

namespace yeti.common.controls
{
    /// <summary>
    /// Win32 API functions
    /// </summary>
    public sealed class Win32
    {
        [DllImport("User32.dll", SetLastError = true)]
        public static extern bool MessageBeep(BeepType Type);
    }
}

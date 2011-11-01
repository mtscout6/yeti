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

namespace yeti.wma.interfaces
{

    [ComImport]
    [Guid("E1CD3524-03D7-11d2-9EED-006097D2D7CF")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface INSSBuffer
    {

        void GetLength([Out] out uint pdwLength);

        void SetLength([In] uint dwLength);

        void GetMaxLength([Out] out uint pdwLength);

        void GetBuffer([Out] out IntPtr ppdwBuffer);

        void GetBufferAndLength([Out] out IntPtr ppdwBuffer, [Out] out uint pdwLength);
    }
}
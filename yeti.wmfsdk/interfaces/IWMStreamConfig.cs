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
using System.Text;
using System.Runtime.InteropServices;

namespace yeti.wma.interfaces
{
    [ComImport]
    [Guid("96406BDC-2B2B-11d3-B36B-00C04F6108FF")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMStreamConfig
    {
        void GetStreamType([Out] out Guid pguidStreamType);
        void GetStreamNumber([Out] out ushort pwStreamNum);
        void SetStreamNumber([In] ushort wStreamNum);
        void GetStreamName([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszStreamName,
                           [In, Out] ref ushort pcchStreamName);
        void SetStreamName([In, MarshalAs(UnmanagedType.LPWStr)] string pwszStreamName);
        void GetConnectionName([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszInputName,
                               [In, Out] ref ushort pcchInputName);
        void SetConnectionName([In, MarshalAs(UnmanagedType.LPWStr)] string pwszInputName);
        void GetBitrate([Out] out uint pdwBitrate);
        void SetBitrate([In] uint pdwBitrate);
        void GetBufferWindow([Out] out uint pmsBufferWindow);
        void SetBufferWindow([In] uint msBufferWindow);
    };
}
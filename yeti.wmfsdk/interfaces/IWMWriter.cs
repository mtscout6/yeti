using System;
using System.Runtime.InteropServices;

namespace yeti.wma.interfaces
{
    [ComImport]
    [Guid("96406BD4-2B2B-11d3-B36B-00C04F6108FF")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMWriter
    {
        void SetProfileByID([In] ref Guid guidProfile);
        void SetProfile([In, MarshalAs(UnmanagedType.Interface)] IWMProfile pProfile);
        void SetOutputFilename([In, MarshalAs(UnmanagedType.LPWStr)] string pwszFilename);
        void GetInputCount([Out] out uint pcInputs);
        void GetInputProps([In] uint dwInputNum,
                           [Out, MarshalAs(UnmanagedType.Interface)] out IWMInputMediaProps ppInput);
        void SetInputProps([In] uint dwInputNum,
                           [In, MarshalAs(UnmanagedType.Interface)] IWMInputMediaProps pInput);
        void GetInputFormatCount([In] uint dwInputNumber, [Out] out uint pcFormats);
        void GetInputFormat([In] uint dwInputNumber,
                            [In] uint dwFormatNumber,
                            [Out, MarshalAs(UnmanagedType.Interface)] out IWMInputMediaProps pProps);
        void BeginWriting();
        void EndWriting();
        void AllocateSample([In] uint dwSampleSize,
                            [Out, MarshalAs(UnmanagedType.Interface)] out INSSBuffer ppSample);
        void WriteSample([In] uint dwInputNum,
                         [In] ulong cnsSampleTime,
                         [In] uint dwFlags,
                         [In, MarshalAs(UnmanagedType.Interface)] INSSBuffer pSample);
        void Flush();
    }
}
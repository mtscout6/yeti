using System;
using System.Runtime.InteropServices;

namespace yeti.wma.interfaces
{
    [ComImport]
    [Guid("96406BD6-2B2B-11d3-B36B-00C04F6108FF")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMReader
    {
        void Open([In, MarshalAs(UnmanagedType.LPWStr)] string pwszURL,
                  [In, MarshalAs(UnmanagedType.Interface)] IWMReaderCallback pCallback,
                  [In] IntPtr pvContext);
        void Close();
        void GetOutputCount([Out] out uint pcOutputs);
        void GetOutputProps([In] uint dwOutputNum,
                            [Out, MarshalAs(UnmanagedType.Interface)] out IWMOutputMediaProps ppOutput);
        void SetOutputProps([In] uint dwOutputNum,
                            [In, MarshalAs(UnmanagedType.Interface)] IWMOutputMediaProps pOutput);
        void GetOutputFormatCount([In] uint dwOutputNumber, [Out] out uint pcFormats);
        void GetOutputFormat([In] uint dwOutputNumber,
                             [In] uint dwFormatNumber,
                             [Out, MarshalAs(UnmanagedType.Interface)] out IWMOutputMediaProps ppProps);
        void Start([In] ulong cnsStart,
                   [In] ulong cnsDuration,
                   [In] float fRate,
                   [In] IntPtr pvContext);
        void Stop();
        void Pause();
        void Resume();
    }
}
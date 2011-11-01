using System.Runtime.InteropServices;

namespace yeti.wma.interfaces
{
    [ComImport]
    [Guid("fc54a285-38c4-45b5-aa23-85b9f7cb424b")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMWriterPreprocess
    {
        void GetMaxPreprocessingPasses([In] uint dwInputNum,
                                       [In] uint dwFlags,
                                       [Out] out uint pdwMaxNumPasses);
        void SetNumPreprocessingPasses([In] uint dwInputNum,
                                       [In] uint dwFlags,
                                       [In] uint dwNumPasses);
        void BeginPreprocessingPass([In] uint dwInputNum, [In] uint dwFlags);
        void PreprocessSample([In] uint dwInputNum,
                              [In] ulong cnsSampleTime,
                              [In] uint dwFlags,
                              [In, MarshalAs(UnmanagedType.Interface)] INSSBuffer pSample);
        void EndPreprocessingPass([In] uint dwInputNum, [In] uint dwFlags);
    }
}
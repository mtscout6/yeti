using System.IO;
using System.Runtime.InteropServices;

namespace yeti.wma.interfaces
{
    public interface IWmaWriter
    {
        void OnHeader([In, MarshalAs(UnmanagedType.Interface)] INSSBuffer pHeader);
        void IsRealTime([Out, MarshalAs(UnmanagedType.Bool)] out bool pfRealTime);

        void AllocateDataUnit([In] uint cbDataUnit,
                              [Out, MarshalAs(UnmanagedType.Interface)] out INSSBuffer ppDataUnit);

        void OnDataUnit([In, MarshalAs(UnmanagedType.Interface)] INSSBuffer pDataUnit);
        void OnEndWriting();
        void Close();

        /// <summary>
        /// Write a buffer of uncompressed PCM data to the buffer.
        /// </summary>
        /// <param name="buffer">Byte array defining the buffer to write.</param>
        /// <param name="index">Index of first value to write</param>
        /// <param name="count">NUmber of byte to write. Must be multiple of PCM sample size <see cref="Yeti.WMFSdk.WmaWriterConfig.Format.nBlockAlign"/></param>
        void Write(byte[] buffer, int index, int count);

        void Write(byte[] buffer);
        AudioWriterConfig WriterConfig { get; }

        /// <summary>
        /// Optimal size of the buffer used in each write operation to obtain best performance.
        /// This value must be greater than 0 
        /// </summary>
        int OptimalBufferSize { get; }

        Stream BaseStream { get; }

        void Write(string value);
        void Write(float value);
        void Write(ulong value);
        void Write(long value);
        void Write(uint value);
        void Write(int value);
        void Write(ushort value);
        void Write(short value);
        void Write(decimal value);
        void Write(double value);
        void Write(char[] chars, int index, int count);
        void Write(char[] chars);
        void Write(char ch);
        void Write(sbyte value);
        void Write(byte value);
        void Write(bool value);
        void Flush();
        long Seek(int offset, SeekOrigin origin);
    }
}
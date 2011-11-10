using System.IO;
using yeti.mp3.configuration;

namespace yeti.mp3.interfaces
{
    public interface IMp3Writer
    {
        /// <summary>
        /// MP3 Config of final data
        /// </summary>
        BE_CONFIG Mp3Config { get; }

        AudioWriterConfig WriterConfig { get; }

        /// <summary>
        /// Optimal size of the buffer used in each write operation to obtain best performance.
        /// This value must be greater than 0 
        /// </summary>
        int OptimalBufferSize { get; }

        Stream BaseStream { get; }

        void Close();

        /// <summary>
        /// Send to the compressor an array of bytes.
        /// </summary>
        /// <param name="buffer">Input buffer</param>
        /// <param name="index">Start position</param>
        /// <param name="count">Bytes to process. The optimal size, to avoid buffer copy, is a multiple of <see cref="AudioWriter.OptimalBufferSize"/></param>
        void Write(byte[] buffer, int index, int count);

        /// <summary>
        /// Send to the compressor an array of bytes.
        /// </summary>
        /// <param name="buffer">The optimal size, to avoid buffer copy, is a multiple of <see cref="AudioWriter.OptimalBufferSize"/></param>
        void Write(byte[] buffer);

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
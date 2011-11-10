using System;
using System.IO;
using System.Runtime.Remoting;

namespace yeti.wav
{
    public interface IWaveStream : IDisposable
    {
        WaveFormat Format { get; }
        bool CanRead { get; }
        bool CanSeek { get; }
        bool CanWrite { get; }
        long Length { get; }
        long Position { get; set; }
        bool CanTimeout { get; }
        int ReadTimeout { get; set; }
        int WriteTimeout { get; set; }
        void Close();
        void Flush();
        void SetLength(long len);
        long Seek(long pos, SeekOrigin o);
        int Read(byte[] buf, int ofs, int count);
        void Write(byte[] buf, int ofs, int count);
        IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state);
        int EndRead(IAsyncResult asyncResult);
        IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state);
        void EndWrite(IAsyncResult asyncResult);
        int ReadByte();
        void WriteByte(byte value);
        object GetLifetimeService();
        object InitializeLifetimeService();
        ObjRef CreateObjRef(Type requestedType);
    }
}
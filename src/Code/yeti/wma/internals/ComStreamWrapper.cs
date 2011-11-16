using System;
using System.IO;
using System.Runtime.InteropServices;
using STATSTG = System.Runtime.InteropServices.STATSTG;

namespace yeti.wma.internals
{
    /// <summary>
    /// Incomplete readonly implementation of the UCOMIStream interface. 
    /// Its simply for our use so we can load file streams in 
    /// the WmaStream class.
    /// </summary>
    internal class ComStreamWrapper : UCOMIStream
    {
        private readonly Stream _wrappedStream;

        public ComStreamWrapper(Stream stream)
        {
            _wrappedStream = stream;
        }

        public void Read(byte[] pv, int cb, IntPtr pcbRead)
        {
            var readByteCount = _wrappedStream.Read(pv, 0, cb);
            Marshal.StructureToPtr(readByteCount, pcbRead, false);
        }

        public void Write(byte[] pv, int cb, IntPtr pcbWritten)
        {
            throw new NotImplementedException();
        }

        public void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition)
        {
            _wrappedStream.Seek(dlibMove, (SeekOrigin)dwOrigin);
        }

        public void SetSize(long libNewSize)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(UCOMIStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten)
        {
            throw new NotImplementedException();
        }

        public void Commit(int grfCommitFlags)
        {
            throw new NotImplementedException();
        }

        public void Revert()
        {
            throw new NotImplementedException();
        }

        public void LockRegion(long libOffset, long cb, int dwLockType)
        {
            throw new NotImplementedException();
        }

        public void UnlockRegion(long libOffset, long cb, int dwLockType)
        {
            throw new NotImplementedException();
        }

        public void Stat(out STATSTG pstatstg, int grfStatFlag)
        {
            pstatstg = new STATSTG 
            {
                cbSize = _wrappedStream.Length,
                type = (int)STGTY.Stream,
            };
        }

        public void Clone(out UCOMIStream ppstm)
        {
            throw new NotImplementedException();
        }
    }

    internal enum STGTY
    {
        Storage = 1,
        Stream = 2,
        ByteArray = 3,
        Property = 4
    }
}
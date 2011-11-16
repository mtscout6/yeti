using System;
using System.Runtime.InteropServices;
using yeti.wma.interfaces;

namespace yeti.wma.internals
{
    /// <summary>
    /// Managed buffer that implements the INSSBuffer interface. 
    /// When passing this buffer to unmanaged code you must 
    /// take in account that it is not a safe operation because 
    /// managed heap could be exposed through the pointer returned by
    /// INSSBuffer methods.
    /// </summary>
    internal class ManBuffer : INSSBuffer, IDisposable
    {
        private uint m_UsedLength;
        private uint m_MaxLength;
        private byte[] m_Buffer;
        private GCHandle handle;
        private bool isDisposed;

        /// <summary>
        /// Create a buffer with specified size
        /// </summary>
        /// <param name="size">Maximun size of buffer</param>
        public ManBuffer(uint size)
        {
            m_Buffer = new byte[size];
            m_MaxLength = m_UsedLength = size;
            handle = GCHandle.Alloc(m_Buffer, GCHandleType.Pinned);
        }

        ~ManBuffer()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (!isDisposed)
            {
                isDisposed = true;
                handle.Free();
            }
        }

        /// <summary>
        /// How many bytes are currently used in the buffer. 
        /// Equivalent to INSSBuffer.GetLentgh and INSSBuffer.SetLength
        /// </summary>
        public uint UsedLength
        {
            get { return m_UsedLength; }
            set
            {
                if (value <= m_MaxLength)
                {
                    m_UsedLength = value;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        /// <summary>
        /// Maximun buffer size. Equivalent to INSSBuffer.GetMaxLength
        /// </summary>
        public uint MaxLength
        {
            get { return m_MaxLength; }
        }

        /// <summary>
        /// Internal byte array that contain buffer data.
        /// </summary>
        public byte[] Buffer
        {
            get { return m_Buffer; }
        }

        #region INSSBuffer Members

        public void GetLength(out uint pdwLength)
        {
            pdwLength = m_UsedLength;
        }

        public void SetLength(uint dwLength)
        {
            UsedLength = dwLength;
        }

        public void GetMaxLength(out uint pdwLength)
        {
            pdwLength = m_MaxLength;
        }

        public void GetBuffer(out IntPtr ppdwBuffer)
        {
            ppdwBuffer = handle.AddrOfPinnedObject();
        }

        public void GetBufferAndLength(out IntPtr ppdwBuffer, out uint pdwLength)
        {
            ppdwBuffer = handle.AddrOfPinnedObject();
            pdwLength = m_UsedLength;
        }

        #endregion

    }
}
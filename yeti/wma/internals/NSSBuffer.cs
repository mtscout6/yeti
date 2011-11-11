//  Widows Media Format Utils classes
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
using yeti.wma.interfaces;

namespace yeti.wma.internals
{
    /// <summary>
    /// Helper class who encapsulates INSSBuffer buffers.
    /// </summary>
    internal class NSSBuffer : IDisposable
    {
        private INSSBuffer m_Buffer;
        private uint m_Length;
        private uint m_MaxLength;
        private IntPtr m_BufferPtr;
        private uint m_Position = 0;

        /// <summary>
        /// NSSBuffer constructor
        /// </summary>
        /// <param name="buff">INSSBuffer to wrap</param>
        public NSSBuffer(INSSBuffer buff)
        {
            Reset(buff);
        }

        public void Reset(INSSBuffer buff)
        {
            ReleaseBuffer();
            m_Position = 0;
            m_Buffer = buff;
            m_Buffer.GetBufferAndLength(out m_BufferPtr, out m_Length);
            m_Buffer.GetMaxLength(out m_MaxLength);
        }

        /// <summary>
        /// Length of the buffer. Wraps INSSBuffer.GetLength and INSSBuffer.SetLength
        /// </summary>
        public uint Length
        {
            get
            {
                return m_Length;
            }
            set
            {
                if (value <= m_MaxLength)
                {
                    m_Buffer.SetLength(value);
                    m_Length = value;
                    if (m_Position > m_Length)
                    {
                        m_Position = m_Length;
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// Read/Write the position for Read or Write operations
        /// </summary>
        public uint Position
        {
            get
            {
                return m_Position;
            }
            set
            {
                if (value <= m_Length)
                {
                    m_Position = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// Reference to the internal INSSBuffer
        /// </summary>
        public INSSBuffer Buffer
        {
            get { return m_Buffer; }
        }

        /// <summary>
        /// Reads from the wrapped buffer to a byte array. 
        /// Position is increased by the number of bytes read.
        /// </summary>
        /// <param name="buffer">Destination byte array</param>
        /// <param name="offset">Position in the destination array where to start copying</param>
        /// <param name="count">Number of bytes to read</param>
        /// <returns>Number of bytes read. Zero means than Position was at buffer length.</returns>
        public int Read(byte[] buffer, int offset, int count)
        {
            if (m_Position < m_Length)
            {
                var src = (IntPtr)(m_BufferPtr.ToInt32() + m_Position);
                int toCopy = Math.Min(count, (int)(this.Length - this.Position));
                Marshal.Copy(src, buffer, offset, toCopy);
                m_Position += (uint)toCopy;
                return toCopy;
            }
            return 0;
        }

        /// <summary>
        /// Write to the wrapped buffer from a byte array. 
        /// Position is increased by the number of byte written
        /// </summary>
        /// <param name="buffer">Source byte array</param>
        /// <param name="offset">Index from where start copying</param>
        /// <param name="count">Number of bytes to copy</param>
        public void Write(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException("offset");
            }
            if ((count <= 0) || ((m_Position + count) > m_Length))
            {
                throw new ArgumentOutOfRangeException("count");
            }
            var dest = (IntPtr)(m_BufferPtr.ToInt32() + m_Position);
            Marshal.Copy(buffer, offset, dest, count);
            m_Position += (uint)count;
        }

        public void Dispose()
        {
            ReleaseBuffer();
        }

        private void ReleaseBuffer()
        {
            if (m_Buffer != null)
            {
                Marshal.ReleaseComObject(m_Buffer);
            }
        }
    }
}

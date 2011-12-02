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
        private INSSBuffer _buffer;
        private uint _length;
        private uint _maxLength;
        private IntPtr _bufferPtr;
        private uint _position;

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
            _position = 0;
            _buffer = buff;
            _buffer.GetBufferAndLength(out _bufferPtr, out _length);
            _buffer.GetMaxLength(out _maxLength);
        }

        /// <summary>
        /// Length of the buffer. Wraps INSSBuffer.GetLength and INSSBuffer.SetLength
        /// </summary>
        public uint Length
        {
            get
            {
                return _length;
            }
            set
            {
                if (value <= _maxLength)
                {
                    _buffer.SetLength(value);
                    _length = value;
                    if (_position > _length)
                    {
                        _position = _length;
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
                return _position;
            }
            set
            {
                if (value <= _length)
                {
                    _position = value;
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
            get { return _buffer; }
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
            if (_position < _length)
            {
                IntPtr src;
                if (CPU.Is32Bit)
                {
                    src = (IntPtr)(_bufferPtr.ToInt32() + _position);
                }
                else
                {
                    src = (IntPtr)(_bufferPtr.ToInt64() + _position);
                }
                var toCopy = Math.Min(count, (int)(Length - Position));
                Marshal.Copy(src, buffer, offset, toCopy);
                _position += (uint)toCopy;
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
            if ((count <= 0) || ((_position + count) > _length))
            {
                throw new ArgumentOutOfRangeException("count");
            }

            IntPtr dest;
            if (CPU.Is32Bit)
            {
                dest = (IntPtr)(_bufferPtr.ToInt32() + _position);
            }
            else
            {
                dest = (IntPtr)(_bufferPtr.ToInt64() + _position);
            }

            Marshal.Copy(buffer, offset, dest, count);
            _position += (uint)count;
        }

        public void Dispose()
        {
            ReleaseBuffer();
        }

        private void ReleaseBuffer()
        {
            if (_buffer != null)
            {
                Marshal.ReleaseComObject(_buffer);
            }
        }
    }
}

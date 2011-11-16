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
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using yeti.wma.interfaces;
using yeti.wma.internals;
using yeti.wma.structs;

namespace yeti.wma
{
    /// <summary>
    /// Audio Writer to write Windows Media Audio data to a stream.
    /// </summary>
    public class WmaWriter : AudioWriter, IWMWriterSink, IWmaWriter
    {
        protected IWMWriter m_Writer;
        private IWMProfile m_Profile;
        private bool m_HeaderWrote = false;
        private long m_HeaderPosition;
        private uint m_HeaderLength;
        private ulong m_MsAudioTime = 0;
        /// <summary>
        /// Create the writer indicating Metadata information
        /// </summary>
        /// <param name="output"><see cref="System.IO.Stream"/> Where resulting WMA string will be written</param>
        /// <param name="format">PCM format of input data received in <see cref="WmaWriter.Write"/> method</param>
        /// <param name="profile">IWMProfile that describe the resulting compressed stream</param>
        /// <param name="metadataAttributes">Array of <see cref="yeti.wma.structs.WM_Attr"/> structures describing the metadata information that will be in the result stream</param>
        public WmaWriter(Stream output, WaveFormat format, IWMProfile profile, IEnumerable<WM_Attr> metadataAttributes)
            : base(output, format)
        {
            m_Writer = WM.CreateWriter();
            var wa = (IWMWriterAdvanced)m_Writer;
            wa.AddSink((IWMWriterSink)this);
            m_Writer.SetProfile(profile);
            uint inputs;
            m_Writer.GetInputCount(out inputs);
            if (inputs == 1)
            {
                IWMInputMediaProps inpProps;
                Guid type;
                m_Writer.GetInputProps(0, out inpProps);
                inpProps.GetType(out type);
                if (type == MediaTypes.WMMEDIATYPE_Audio)
                {
                    WM_MEDIA_TYPE mt;
                    mt.majortype = MediaTypes.WMMEDIATYPE_Audio;
                    mt.subtype = MediaTypes.WMMEDIASUBTYPE_PCM;
                    mt.bFixedSizeSamples = true;
                    mt.bTemporalCompression = false;
                    mt.lSampleSize = (uint)m_InputDataFormat.nBlockAlign;
                    mt.formattype = MediaTypes.WMFORMAT_WaveFormatEx;
                    mt.pUnk = IntPtr.Zero;
                    mt.cbFormat = (uint)Marshal.SizeOf(m_InputDataFormat);

                    GCHandle h = GCHandle.Alloc(m_InputDataFormat, GCHandleType.Pinned);
                    try
                    {
                        mt.pbFormat = h.AddrOfPinnedObject();
                        inpProps.SetMediaType(ref mt);
                    }
                    finally
                    {
                        h.Free();
                    }
                    m_Writer.SetInputProps(0, inpProps);
                    if (metadataAttributes != null)
                    {
                        var info = new WMHeaderInfo((IWMHeaderInfo)m_Writer);
                        foreach (WM_Attr attr in metadataAttributes)
                        {
                            info.SetAttribute(attr);
                        }
                        info = null;
                    }
                    m_Writer.BeginWriting();
                    m_Profile = profile;
                }
                else
                {
                    throw new ArgumentException("Invalid profile", "profile");
                }
            }
            else
            {
                throw new ArgumentException("Invalid profile", "profile");
            }
        }
        /// <summary>
        /// Create the writer without metadata Information
        /// </summary>
        /// <param name="output"><see cref="System.IO.Stream"/> Where resulting WMA string will be written</param>
        /// <param name="format">PCM format of input data received in <see cref="WmaWriter.Write"/> method</param>
        /// <param name="profile">IWMProfile that describe the resulting compressed stream</param>
        public WmaWriter(Stream output, WaveFormat format, IWMProfile profile)
            : this(output, format, profile, null)
        {
        }

        /// <summary>
        /// Create the writer from information stored in <see cref="WmaWriterConfig"/>
        /// </summary>
        /// <param name="output"><see cref="System.IO.Stream"/> Where resulting WMA string will be written</param>
        /// <param name="config"><see cref="WmaWriterConfig"/> instance from where IWMProfile and WaveFormat will be taken to construct the writer</param>
        public WmaWriter(Stream output, WmaWriterConfig config)
            : this(output, config.Format, config.Profile, null)
        {
        }

        #region IWMWriterSink implementation
        public void OnHeader([In, MarshalAs(UnmanagedType.Interface)] INSSBuffer pHeader)
        {
            byte[] buffer;
            uint length;
            var manBuff = pHeader as ManBuffer;
            if (manBuff != null)
            {
                buffer = manBuff.Buffer;
                length = manBuff.UsedLength;
                manBuff.Dispose();
            }
            else
            {
                using (var b = new NSSBuffer(pHeader))
                {
                    length = b.Length;
                    buffer = new byte[length];
                    b.Read(buffer, 0, (int)length);
                }
            }
            if (!m_HeaderWrote)
            {
                if (BaseStream.CanSeek)
                {
                    m_HeaderPosition = BaseStream.Position;
                    m_HeaderLength = length;
                }
                m_HeaderWrote = true;
                base.Write(buffer, 0, (int)length);
            }
            else if (BaseStream.CanSeek && (length == m_HeaderLength))
            {
                long pos = BaseStream.Position;
                BaseStream.Position = m_HeaderPosition;
                BaseStream.Write(buffer, 0, (int)length);
                BaseStream.Position = pos;
            }
        }

        public void IsRealTime([Out, MarshalAs(UnmanagedType.Bool)] out bool pfRealTime)
        {
            pfRealTime = false;
        }

        public void AllocateDataUnit([In] uint cbDataUnit,
         [Out, MarshalAs(UnmanagedType.Interface)] out INSSBuffer ppDataUnit)
        {
            ppDataUnit = (INSSBuffer)(new ManBuffer(cbDataUnit));
        }

        public void OnDataUnit([In, MarshalAs(UnmanagedType.Interface)] INSSBuffer pDataUnit)
        {
            byte[] buffer;
            int length;
            var manBuff = pDataUnit as ManBuffer;
            if (manBuff != null)
            {
                buffer = manBuff.Buffer;
                length = (int)manBuff.UsedLength;
                manBuff.Dispose();
            }
            else
            {
                using (var b = new NSSBuffer(pDataUnit))
                {
                    length = (int)b.Length;
                    buffer = new byte[length];
                    b.Read(buffer, 0, length);
                }
            }
            base.Write(buffer, 0, length);
        }

        public void OnEndWriting()
        {
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            Close();
        }

        public override void Close()
        {
            try
            {
                if (m_Writer != null)
                {
                    m_Writer.EndWriting();
                    var wa = (IWMWriterAdvanced)m_Writer;
                    wa.RemoveSink((IWMWriterSink)this);
                    Marshal.ReleaseComObject(m_Writer);
                    m_Writer = null;
                    m_Profile = null;
                }
            }
            finally
            {
                //Purposefully leaving the stream open since it gets passed in, 
                //its the responsibility of the calling class to manage its own resources.
                base.Flush();
            }
        }


        /// <summary>
        /// Return the optimal size of buffer in each write operations. Other value could be
        /// more optimal. The only requirement for buffer size is that it must be multiple
        /// of PCM sample size <see cref="yeti.wma.WmaWriterConfig.Format.nBlockAlign"/>
        /// </summary>
        /// <returns>Size equivalent to 100 milliseconds.</returns>
        protected override int GetOptimalBufferSize()
        {
            int res = m_InputDataFormat.nAvgBytesPerSec / 10;
            res += (res % m_InputDataFormat.nBlockAlign);
            return res;
        }

        protected override AudioWriterConfig GetWriterConfig()
        {
            return new WmaWriterConfig(m_InputDataFormat, m_Profile);
        }

        /// <summary>
        /// Write a buffer of uncompressed PCM data to the buffer.
        /// </summary>
        /// <param name="buffer">Byte array defining the buffer to write.</param>
        /// <param name="index">Index of first value to write</param>
        /// <param name="count">NUmber of byte to write. Must be multiple of PCM sample size <see cref="yeti.wma.WmaWriterConfig.Format.nBlockAlign"/></param>
        public override void Write(byte[] buffer, int index, int count)
        {
            if ((count % m_InputDataFormat.nBlockAlign) == 0)
            {
                INSSBuffer buff;
                NSSBuffer nssBuff;
                m_Writer.AllocateSample((uint)count, out buff);
                using (nssBuff = new NSSBuffer(buff))
                {
                    nssBuff.Write(buffer, index, count);
                    nssBuff.Length = (uint)count;
                    m_Writer.WriteSample(0, m_MsAudioTime * 10000, 0, buff);
                }
                m_MsAudioTime += ((ulong)count * 1000) / (ulong)m_InputDataFormat.nAvgBytesPerSec;
            }
            else
            {
                throw new ArgumentException(string.Format("Invalid buffer size. Buffer size must be aligned to {0} bytes.", m_InputDataFormat.nBlockAlign), "count");
            }
        }

        public override void Write(byte[] buffer)
        {
            this.Write(buffer, 0, buffer.Length);
        }


    }
}
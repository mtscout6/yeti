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
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;
using yeti.wma.enums;
using yeti.wma.interfaces;
using yeti.wma.internals;
using yeti.wma.structs;

namespace yeti.wma
{
    /// <summary>
    /// Stream that provides uncompressed audio data from any file that
    /// can be read using the WMF (WMA, WMV, MP3, MPE, ASF, etc)
    /// </summary>
    public class WmaStreamReader : Stream, IWmaStreamReader
    {
        private IWMSyncReader m_Reader = null;
        private uint m_OutputNumber;
        private ushort m_OuputStream;
        private int m_OutputFormatNumber;
        private long m_Position = 0;
        private long m_Length = -1;
        private bool m_Seekable = false;
        private uint m_SampleSize = 0;
        private WaveFormat m_OutputFormat = null;
        private const uint InvalidOuput = 0xFFFFFFFF;

        /// <summary>
        /// Create WmaStream with specific format for for uncompressed audio data.
        /// </summary>
        /// <param name="FileName">Name of asf file</param>
        /// <param name="OutputFormat">WaveFormat that define the desired audio data format</param>
        public WmaStreamReader(string FileName, WaveFormat OutputFormat)
        {
            m_Reader = WM.CreateSyncReader(WMT_RIGHTS.WMT_RIGHT_NO_DRM);
            try
            {
                m_Reader.Open(FileName);
                Init(OutputFormat);
            }
            catch
            {
                try
                {
                    m_Reader.Close();
                }
                finally
                {
                    m_Reader = null;
                }
                throw;
            }
        }

        /// <summary>
        /// Create WmaStream with specific format for for uncompressed audio data.
        /// </summary>
        /// <param name="inputStream">Name of asf stream</param>
        /// <param name="OutputFormat">WaveFormat that define the desired audio data format</param>
        public WmaStreamReader(Stream inputStream, WaveFormat OutputFormat)
        {
            m_Reader = WM.CreateSyncReader(WMT_RIGHTS.WMT_RIGHT_NO_DRM);
            try
            {
                m_Reader.OpenStream(new ComStreamWrapper(inputStream));
                Init(OutputFormat);
            }
            catch
            {
                try
                {
                    m_Reader.Close();
                }
                finally
                {
                    m_Reader = null;
                }
                throw;
            }
        }

        /// <summary>
        /// Create WmaStream. The first PCM available for audio outputs will be used as output format.
        /// Output format can be checked in <see cref="Format"/> property.
        /// </summary>
        /// <param name="FileName">Name of asf file</param>
        public WmaStreamReader(string FileName)
            : this(FileName, null)
        {
        }

        /// <summary>
        /// Create WmaStream. The first PCM available for audio outputs will be used as output format.
        /// Output format can be checked in <see cref="Format"/> property.
        /// </summary>
        /// <param name="inputStream">Name of asf stream</param>
        public WmaStreamReader(Stream inputStream)
            : this(inputStream, null)
        {
        }

        ~WmaStreamReader()
        {
            Dispose(false);
        }

        /// <summary>
        /// Give the <see cref="WaveFormat"/> that describes the format of ouput data in each Read operation.
        /// </summary>
        public WaveFormat Format
        {
            get { return new WaveFormat(m_OutputFormat.nSamplesPerSec, m_OutputFormat.wBitsPerSample, m_OutputFormat.nChannels); }
        }

        /// <summary>
        /// IWMProfile of the input ASF file.
        /// </summary>
        public IWMProfile Profile
        {
            get { return (IWMProfile)m_Reader; }
        }

        /// <summary>
        /// IWMHeaderInfo related to the input ASF file.
        /// </summary>
        public IWMHeaderInfo HeaderInfo
        {
            get { return (IWMHeaderInfo)m_Reader; }
        }

        /// <summary>
        /// Recomended size of buffer in each <see cref="Read"/> operation
        /// </summary>
        public int SampleSize
        {
            get { return (int)m_SampleSize; }
        }

        /// <summary>
        /// If Seek if allowed every seek operation must be to a value multiple of SeekAlign
        /// </summary>
        public long SeekAlign
        {
            get { return Math.Max(SampleTime2BytePosition(1), (long)m_OutputFormat.nBlockAlign); }
        }

        /// <summary>
        /// Convert a time value in 100 nanosecond unit to a byte position 
        /// of byte array containing the PCM data. <seealso cref="BytePosition2SampleTime"/>
        /// </summary>
        /// <param name="SampleTime">Sample time in 100 nanosecond units</param>
        /// <returns>Byte position</returns>
        protected long SampleTime2BytePosition(ulong SampleTime)
        {
            ulong res = SampleTime * (ulong)m_OutputFormat.nAvgBytesPerSec / 10000000;
            //Align to sample position
            res -= (res % (ulong)m_OutputFormat.nBlockAlign);
            return (long)res;
        }

        /// <summary>
        /// Returns the sample time in 100 nanosecond units corresponding to
        /// byte position in a byte array of PCM data. <see cref="SampleTime2BytePosition"/>
        /// </summary>
        /// <param name="pos">Byte position</param>
        /// <returns>Sample time in 100 nanosecond units</returns>
        protected ulong BytePosition2SampleTime(long pos)
        {
            //Align to sample position
            pos -= (pos % (long)m_OutputFormat.nBlockAlign);
            return (ulong)pos * 10000000 / (ulong)m_OutputFormat.nAvgBytesPerSec;
        }

        /// <summary>
        /// Index that give the string representation of the Metadata attribute whose
        /// name is the string index. If the Metadata is not present returns <code>null</code>. 
        /// This only return the file level Metadata info, to read stream level metadata use <see cref="HeaderInfo"/>
        /// </summary>
        /// <example>
        /// <code>
        /// using (WmaStream str = new WmaStream("somefile.asf") )
        /// {
        ///   string Title = str[WM.g_wszWMTitle];
        ///   if ( Title != null )
        ///   {
        ///     Console.WriteLine("Title: {0}", Title);
        ///   }
        /// }
        /// </code>
        /// </example>
        [System.Runtime.CompilerServices.IndexerName("Attributes")]
        public string this[string AttrName]
        {
            get
            {
                var head = new WMHeaderInfo(HeaderInfo);
                try
                {
                    return head[AttrName].Value.ToString();
                }
                catch (COMException e)
                {
                    if (e.ErrorCode == WM.ASF_E_NOTFOUND)
                    {
                        return null;
                    }
                    throw;
                }
            }
        }

        #region Private methods to interact with the WMF

        private void Init(WaveFormat outputFormat)
        {
            m_OutputNumber = GetAudioOutputNumber(m_Reader);
            if (m_OutputNumber == InvalidOuput)
            {
                throw new ArgumentException("An audio stream was not found");
            }
            int[] formatIndexes = GetPCMOutputNumbers(m_Reader, (uint)m_OutputNumber);
            if (formatIndexes.Length == 0)
            {
                throw new ArgumentException("An audio stream was not found");
            }
            if (outputFormat != null)
            {
                m_OutputFormatNumber = -1;
                for (int i = 0; i < formatIndexes.Length; i++)
                {
                    WaveFormat fmt = GetOutputFormat(m_Reader, (uint)m_OutputNumber, (uint)formatIndexes[i]);
                    if ((fmt.wFormatTag == outputFormat.wFormatTag) &&
                      (fmt.nAvgBytesPerSec == outputFormat.nAvgBytesPerSec) &&
                      (fmt.nBlockAlign == outputFormat.nBlockAlign) &&
                      (fmt.nChannels == outputFormat.nChannels) &&
                      (fmt.nSamplesPerSec == outputFormat.nSamplesPerSec) &&
                      (fmt.wBitsPerSample == outputFormat.wBitsPerSample))
                    {
                        m_OutputFormatNumber = formatIndexes[i];
                        m_OutputFormat = fmt;
                        break;
                    }
                }
                if (m_OutputFormatNumber < 0)
                {
                    throw new ArgumentException("No PCM output found");
                }
            }
            else
            {
                m_OutputFormatNumber = formatIndexes[0];
                m_OutputFormat = GetOutputFormat(m_Reader, (uint)m_OutputNumber, (uint)formatIndexes[0]);
            }
            uint outputCtns = 0;
            m_Reader.GetOutputCount(out outputCtns);
            var streamNumbers = new ushort[outputCtns];
            var streamSelections = new WMT_STREAM_SELECTION[outputCtns];
            for (uint i = 0; i < outputCtns; i++)
            {
                m_Reader.GetStreamNumberForOutput(i, out streamNumbers[i]);
                if (i == m_OutputNumber)
                {
                    streamSelections[i] = WMT_STREAM_SELECTION.WMT_ON;
                    m_OuputStream = streamNumbers[i];
                    m_Reader.SetReadStreamSamples(m_OuputStream, false);
                }
                else
                {
                    streamSelections[i] = WMT_STREAM_SELECTION.WMT_OFF;
                }
            }
            m_Reader.SetStreamsSelected((ushort)outputCtns, streamNumbers, streamSelections);
            IWMOutputMediaProps props = null;
            m_Reader.GetOutputFormat((uint)m_OutputNumber, (uint)m_OutputFormatNumber, out props);
            m_Reader.SetOutputProps((uint)m_OutputNumber, props);

            uint size = 0;
            props.GetMediaType(IntPtr.Zero, ref size);
            IntPtr buffer = Marshal.AllocCoTaskMem((int)size);
            try
            {
                props.GetMediaType(buffer, ref size);
                var mt = (WM_MEDIA_TYPE)Marshal.PtrToStructure(buffer, typeof(WM_MEDIA_TYPE));
                m_SampleSize = mt.lSampleSize;
            }
            finally
            {
                Marshal.FreeCoTaskMem(buffer);
                props = null;
            }

            m_Seekable = false;
            m_Length = -1;
            var head = new WMHeaderInfo(HeaderInfo);
            try
            {
                m_Seekable = (bool)head[WM.g_wszWMSeekable];
                m_Length = SampleTime2BytePosition((ulong)head[WM.g_wszWMDuration]);
            }
            catch (COMException e)
            {
                if (e.ErrorCode != WM.ASF_E_NOTFOUND)
                {
                    throw;
                }
            }

        }

        private static uint GetAudioOutputNumber(IWMSyncReader Reader)
        {
            uint res = InvalidOuput;
            uint outCounts = 0;
            Reader.GetOutputCount(out outCounts);
            for (uint i = 0; i < outCounts; i++)
            {
                IWMOutputMediaProps props = null;
                Reader.GetOutputProps(i, out props);
                Guid mt;
                props.GetType(out mt);
                if (mt == MediaTypes.WMMEDIATYPE_Audio)
                {
                    res = i;
                    break;
                }
            }
            return res;
        }

        protected const uint WAVE_FORMAT_EX_SIZE = 18;

        private static int[] GetPCMOutputNumbers(IWMSyncReader Reader, uint OutputNumber)
        {
            var result = new ArrayList();
            uint formatCount = 0;
            Reader.GetOutputFormatCount(OutputNumber, out formatCount);
            int bufferSize = Marshal.SizeOf(typeof(WM_MEDIA_TYPE)) + Marshal.SizeOf(typeof(WaveFormat));
            IntPtr buffer = Marshal.AllocCoTaskMem(bufferSize);
            try
            {
                for (int i = 0; i < formatCount; i++)
                {
                    IWMOutputMediaProps props = null;
                    uint size = 0;
                    Reader.GetOutputFormat(OutputNumber, (uint)i, out props);
                    props.GetMediaType(IntPtr.Zero, ref size);
                    if ((int)size > bufferSize)
                    {
                        bufferSize = (int)size;
                        Marshal.FreeCoTaskMem(buffer);
                        buffer = Marshal.AllocCoTaskMem(bufferSize);
                    }
                    props.GetMediaType(buffer, ref size);
                    var mt = (WM_MEDIA_TYPE)Marshal.PtrToStructure(buffer, typeof(WM_MEDIA_TYPE));
                    if ((mt.majortype == MediaTypes.WMMEDIATYPE_Audio) &&
                         (mt.subtype == MediaTypes.WMMEDIASUBTYPE_PCM) &&
                         (mt.formattype == MediaTypes.WMFORMAT_WaveFormatEx) &&
                         (mt.cbFormat >= WAVE_FORMAT_EX_SIZE))
                    {
                        result.Add(i);
                    }
                }
            }
            finally
            {
                Marshal.FreeCoTaskMem(buffer);
            }
            return (int[])result.ToArray(typeof(int));
        }

        private static WaveFormat GetOutputFormat(IWMSyncReader Reader, uint OutputNumber, uint FormatNumber)
        {
            IWMOutputMediaProps props = null;
            uint size = 0;
            WaveFormat fmt = null;
            Reader.GetOutputFormat(OutputNumber, FormatNumber, out props);
            props.GetMediaType(IntPtr.Zero, ref size);
            IntPtr buffer = Marshal.AllocCoTaskMem(Math.Max((int)size, Marshal.SizeOf(typeof(WM_MEDIA_TYPE)) + Marshal.SizeOf(typeof(WaveFormat))));
            try
            {
                props.GetMediaType(buffer, ref size);
                var mt = (WM_MEDIA_TYPE)Marshal.PtrToStructure(buffer, typeof(WM_MEDIA_TYPE));
                if ((mt.majortype == MediaTypes.WMMEDIATYPE_Audio) &&
                     (mt.subtype == MediaTypes.WMMEDIASUBTYPE_PCM) &&
                     (mt.formattype == MediaTypes.WMFORMAT_WaveFormatEx) &&
                     (mt.cbFormat >= WAVE_FORMAT_EX_SIZE))
                {
                    fmt = new WaveFormat(44100, 16, 2);
                    Marshal.PtrToStructure(mt.pbFormat, fmt);
                }
                else
                {
                    throw new ArgumentException(string.Format("The format {0} of the output {1} is not a valid PCM format", FormatNumber, OutputNumber));
                }
            }
            finally
            {
                Marshal.FreeCoTaskMem(buffer);
            }
            return fmt;
        }
        #endregion

        #region Overrided Stream methods
        public override void Close()
        {
            if (m_Reader != null)
            {
                m_Reader.Close();
                Marshal.ReleaseComObject(m_Reader);
                m_Reader = null;
            }
            base.Close();
        }

        private NSSBuffer m_BufferReader = null;

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (m_Reader != null)
            {
                int read = 0;
                if ((m_Length > 0) && ((m_Length - m_Position) < count))
                {
                    count = (int)(m_Length - m_Position);
                }
                if (m_BufferReader != null)
                {
                    while ((m_BufferReader.Position < m_BufferReader.Length) && (read < count))
                    {
                        read += m_BufferReader.Read(buffer, offset, count);
                    }
                }
                while (read < count)
                {
                    INSSBuffer sample = null;
                    try
                    {
                        uint flags = 0;
                        ulong duration = 0;
                        ulong sampleTime = 0;
                        m_Reader.GetNextSample(m_OuputStream, out sample, out sampleTime, out duration, out flags, out m_OutputNumber, out m_OuputStream);
                    }
                    catch (COMException e)
                    {
                        if (e.ErrorCode == WM.NS_E_NO_MORE_SAMPLES)
                        { //No more samples
                            if (m_OutputFormat.wBitsPerSample == 8)
                            {
                                while (read < count)
                                {
                                    buffer[offset + read] = 0x80;
                                    read++;
                                }
                            }
                            else
                            {
                                Array.Clear(buffer, offset + read, count - read);
                                read = count;
                            }
                            break;
                        }
                        else
                        {
                            throw (e);
                        }
                    }

                    if (m_BufferReader != null)
                    {
                        m_BufferReader.Reset(sample);
                    }
                    else
                    {
                        m_BufferReader = new NSSBuffer(sample);
                    }
                    read += m_BufferReader.Read(buffer, offset + read, count - read);
                }
                if ((m_BufferReader != null) && (m_BufferReader.Position >= m_BufferReader.Length))
                {
                    m_BufferReader.Dispose();
                    m_BufferReader = null;
                }
                m_Position += read;
                return read;
            }
            throw new ObjectDisposedException(this.ToString());
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            if (CanSeek)
            {
                switch (origin)
                {
                    case SeekOrigin.Current:
                        offset += m_Position;
                        break;
                    case SeekOrigin.End:
                        offset += m_Length;
                        break;
                }
                if (offset == m_Position)
                {
                    return m_Position; // :-)
                }
                if ((offset < 0) || (offset > m_Length))
                {
                    throw new ArgumentException("Offset out of range", "offset");
                }
                if ((offset % SeekAlign) > 0)
                {
                    throw new ArgumentException(string.Format("Offset must be aligned by a value of SeekAlign ({0})", SeekAlign), "offset");
                }
                ulong sampleTime = BytePosition2SampleTime(offset);
                m_Reader.SetRange(sampleTime, 0);
                m_Position = offset;
                if (m_BufferReader != null)
                {
                    Marshal.ReleaseComObject(m_BufferReader.Buffer);
                }
                m_BufferReader = null;
                return offset;
            }
            throw new NotSupportedException();
        }

        public override void Flush()
        {
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override bool CanRead
        {
            get
            {
                if (m_Reader != null)
                {
                    return true;
                }
                throw new ObjectDisposedException(this.ToString());
            }
        }

        public override bool CanSeek
        {
            get
            {
                if (m_Reader != null)
                {
                    return m_Seekable && (m_Length > 0);
                }
                throw new ObjectDisposedException(this.ToString());
            }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override long Length
        {
            get
            {
                if (m_Reader != null)
                {
                    if (CanSeek)
                    {
                        return m_Length;
                    }
                    throw new NotSupportedException();
                }
                throw new ObjectDisposedException(this.ToString());
            }
        }

        public TimeSpan Duration
        {
            get { return TimeSpan.FromSeconds((Length * 1.0) / Format.nAvgBytesPerSec); }
        }

        public override long Position
        {
            get
            {
                if (m_Reader != null)
                {
                    return m_Position;
                }
                throw new ObjectDisposedException(this.ToString());
            }
            set
            {
                Seek(value, SeekOrigin.Begin);
            }
        }
        #endregion

        #region IDisposable Members
        private new void Dispose(bool disposing)
        {
            if (disposing)
            {
                Close();
            }
        }

        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        public Stream GetEncodedStream()
        {
            var encodedStream = new MemoryStream();
            var writer = new WmaWriter(encodedStream, Format, Profile);

            Seek(0, SeekOrigin.Begin);

            var buffer = new byte[writer.OptimalBufferSize * 50];

            int read;
            while (Position < Length && (read = Read(buffer, 0, buffer.Length)) > 0)
            {
                writer.Write(buffer, 0, read);
            }

            writer.Close();

            return encodedStream;
        }
    }
}

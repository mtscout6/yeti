using System;
using System.IO;
using System.Runtime.Remoting;

namespace yeti.wma.interfaces
{
    public interface IWmaStreamReader : IDisposable
    {
        /// <summary>
        /// Give the <see cref="WaveFormat"/> that describes the format of ouput data in each Read operation.
        /// </summary>
        WaveFormat Format { get; }

        /// <summary>
        /// IWMProfile of the input ASF file.
        /// </summary>
        IWMProfile Profile { get; }

        /// <summary>
        /// IWMHeaderInfo related to the input ASF file.
        /// </summary>
        IWMHeaderInfo HeaderInfo { get; }

        /// <summary>
        /// Recomended size of buffer in each <see cref="Read"/> operation
        /// </summary>
        int SampleSize { get; }

        /// <summary>
        /// If Seek if allowed every seek operation must be to a value multiple of SeekAlign
        /// </summary>
        long SeekAlign { get; }

        bool CanRead { get; }
        bool CanSeek { get; }
        bool CanWrite { get; }
        long Length { get; }
        TimeSpan Duration { get; }
        long Position { get; set; }
        bool CanTimeout { get; }
        int ReadTimeout { get; set; }
        int WriteTimeout { get; set; }

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
        string this[string AttrName] { get; }

        void Close();
        int Read(byte[] buffer, int offset, int count);
        void Write(byte[] buffer, int offset, int count);
        long Seek(long offset, SeekOrigin origin);
        void Flush();
        void SetLength(long value);
        IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state);
        int EndRead(IAsyncResult asyncResult);
        IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state);
        void EndWrite(IAsyncResult asyncResult);
        int ReadByte();
        void WriteByte(byte value);
        object GetLifetimeService();
        object InitializeLifetimeService();
        ObjRef CreateObjRef(Type requestedType);

        Stream GetEncodedStream();
    }
}
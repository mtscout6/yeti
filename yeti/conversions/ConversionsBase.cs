using System.IO;

namespace yeti.conversions
{
    public class ConversionsBase
    {
        protected static void WriteToStream(AudioWriter writer, Stream inputStream, byte[] buffer)
        {
            int read;
            while ((read = inputStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                writer.Write(buffer, 0, read);
                writer.Flush();
            }
        }
    }
}

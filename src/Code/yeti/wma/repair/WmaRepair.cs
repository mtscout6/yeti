using System;
using System.IO;
using yeti.wma.repair.fileObjects;

namespace yeti.wma.repair
{
    /// <summary>
    /// This class will attempt to repair a WMA file so 
    /// that it will regain seek support and be able to convert it to another format.
    /// There is NO guarantee that this will work but it will fill in some of the blanks
    /// that are left if a WMA file is not ended properly.  
    /// Example: The process crashes in the middle of recording a wma file.
    /// </summary>
    public static class WmaRepair
    {
        public static void Repair(Stream stream, bool isBroadcastStream)
        {
            var reader = new BinaryReader(stream);
            var fileLength = reader.BaseStream.Length;
            if (Helper.SeekTo(reader, ASFGuids.Header))
            {
                var headerSize = reader.ReadInt64();
                var fileProperties = new FileProperties(reader);
                var dataProperties = new DataProperties(reader);
                var dataPacketCount = (long)Math.Floor((fileLength - headerSize) * 1d / fileProperties.MaximumDataPacketSize);
                fileProperties.WriteFileSize(fileLength);
                var duration = TimeSpan.FromMinutes(fileLength / ((fileProperties.MaximumBitrate / 8d) * 60));
                fileProperties.WritePlayAndSendDuration(duration);
                fileProperties.WriteDataPacketsCount(dataPacketCount);
                fileProperties.WriteFlags(isBroadcastStream);
                dataProperties.WriteDataPacketsCount(dataPacketCount);
                dataProperties.WriteDataSize(fileLength - headerSize);
            }
        }

        public static void Repair(string file, bool isBroadcastStream)
        {
            using (var stream = new FileStream(file, FileMode.Open, FileAccess.ReadWrite))
            {
                Repair(stream, isBroadcastStream);
            }
        }
    }
}
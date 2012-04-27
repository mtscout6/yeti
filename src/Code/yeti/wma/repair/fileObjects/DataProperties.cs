using System;
using System.IO;

namespace yeti.wma.repair.fileObjects
{
    public class DataProperties
    {
         private readonly BinaryReader _reader;
        private long _objectStreamPosition;

        public DataProperties(BinaryReader reader)
        {
            _reader = reader;
            ReadValues();
        }

        private void ReadValues()
        {
            if (!Helper.SeekTo(_reader, ASFGuids.Data))
            {
                throw new InvalidOperationException("Reader is not at the data properties");
            }
            _objectStreamPosition = _reader.BaseStream.Position;
            Size = _reader.ReadInt64();
            FileId = new Guid(_reader.ReadBytes(16));
            TotalDataPackets = _reader.ReadInt64();
        }

        public void WriteDataPacketsCount(long count)
        {
            var currentPosition = _reader.BaseStream.Position;
            var stream = _reader.BaseStream;
            var writer = new BinaryWriter(_reader.BaseStream);
            stream.Position = _objectStreamPosition + 24 /*Skip the properties until DataPacketsCount*/;
            writer.Write(count);
            writer.Flush();
            stream.Position = currentPosition;
            TotalDataPackets = count;
        }

        public void WriteDataSize(long size)
        {
            var currentPosition = _reader.BaseStream.Position;
            var stream = _reader.BaseStream;
            var writer = new BinaryWriter(_reader.BaseStream);
            stream.Position = _objectStreamPosition;
            writer.Write(size);
            writer.Flush();
            stream.Position = currentPosition;
            Size = size;
        }

        public long Size { get; set; }
        public Guid FileId { get; set; }
        public long TotalDataPackets { get; set; }
    }
}
using System;
using System.IO;

namespace yeti.wma.repair.fileObjects
{
    public class FileProperties
    {
        private readonly BinaryReader _reader;
        private long _objectStreamPosition;
        private FlagEnum _flagValue;

        public FileProperties(BinaryReader reader)
        {
            _reader = reader;
            ReadValues();
        }

        private void ReadValues()
        {
            if (!Helper.SeekTo(_reader, HeaderGuids.File_Properties))
            {
                throw new InvalidOperationException("Reader is not at the file properties");
            }
            _objectStreamPosition = _reader.BaseStream.Position;
            Size = _reader.ReadInt64();
            FileId = new Guid(_reader.ReadBytes(16));
            FileSize = _reader.ReadInt64();
            CreationDate = new DateTime(_reader.ReadInt64());
            DataPacketsCount = _reader.ReadInt64();
            PlayDuration = new TimeSpan(_reader.ReadInt64());
            SendDuration = new TimeSpan(_reader.ReadInt64());
            Preroll = new TimeSpan(_reader.ReadInt64());
            var flags = _reader.ReadBytes(4);
            var f = flags[0];
            _flagValue = (FlagEnum)BitConverter.ToInt32(flags, 0);
            MinimumDataPacketSize = _reader.ReadInt32();
            MaximumDataPacketSize = _reader.ReadInt32();
            MaximumBitrate = _reader.ReadInt32();
        }

        public long Size { get; private set; }
        public Guid FileId { get; private set; }
        public long FileSize { get; private set; }
        public DateTime CreationDate { get; private set; }
        public long DataPacketsCount { get; private set; }
        public TimeSpan PlayDuration { get; private set; }
        public TimeSpan SendDuration { get; private set; }
        public TimeSpan Preroll { get; private set; }
        public bool Broadcast 
        { 
            get 
            {
                return (_flagValue & FlagEnum.Broadcast) == FlagEnum.Broadcast;
            }
        }
        public bool Seekable 
        {
            get
            {
                return (_flagValue & FlagEnum.Seekable) == FlagEnum.Seekable;
            }
        }
        public int MinimumDataPacketSize { get; private set; }
        public int MaximumDataPacketSize { get; private set; }
        public int MaximumBitrate { get; private set; }

        public void WriteFlags(bool isBroadcast)
        {
            var currentPosition = _reader.BaseStream.Position;
            var stream = _reader.BaseStream;
            var writer = new BinaryWriter(_reader.BaseStream);
            stream.Position = _objectStreamPosition + 72/*Skip the properties until Flag*/;
            var newFlag = (byte)(isBroadcast ? 9/*Broadcast-Live*/ : 2/*Seekable*/);
            writer.Write(newFlag);
            writer.Flush();
            stream.Position = currentPosition;
            _flagValue = (FlagEnum)newFlag;
        }

        public void WriteDataPacketsCount(long count)
        {
            var currentPosition = _reader.BaseStream.Position;
            var stream = _reader.BaseStream;
            var writer = new BinaryWriter(_reader.BaseStream);
            stream.Position = _objectStreamPosition + 40 /*Skip the properties until DataPacketsCount*/;
            writer.Write(count);
            writer.Flush();
            stream.Position = currentPosition;
            DataPacketsCount = count;
        }

        public void WriteFileSize(long length)
        {
            var currentPosition = _reader.BaseStream.Position;
            var stream = _reader.BaseStream;
            var writer = new BinaryWriter(_reader.BaseStream);
            stream.Position = _objectStreamPosition + 24 /*Skip the properties until FileSize*/;
            writer.Write(length);
            writer.Flush();
            stream.Position = currentPosition;
            FileSize = length;
        }

        public void WritePlayAndSendDuration(TimeSpan duration)
        {
            var currentPosition = _reader.BaseStream.Position;
            var stream = _reader.BaseStream;
            var writer = new BinaryWriter(_reader.BaseStream);
            stream.Position = _objectStreamPosition + 48 /*Skip the properties until Play/SendDuration*/;
            var sendDuration = duration.Add(Preroll);
            writer.Write(duration.Ticks);
            writer.Write(sendDuration.Ticks);
            writer.Flush();
            stream.Position = currentPosition;
            PlayDuration = duration;
            SendDuration = sendDuration;
        }

        [Flags]
        private enum FlagEnum
        {
            Broadcast = 0x1,
            Seekable = 0x2
        }
    }
}
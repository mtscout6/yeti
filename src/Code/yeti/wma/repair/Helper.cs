using System;
using System.IO;

namespace yeti.wma.repair
{
    public static class Helper
    {
        internal static bool SeekTo(BinaryReader reader, Guid location)
        {
            try
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length - 16)
                {
                    var guid = new Guid(reader.ReadBytes(16));
                    if (guid != location)
                    {
                        reader.BaseStream.Position = reader.BaseStream.Position - 15;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                reader.BaseStream.Position = reader.BaseStream.Position - 15;
            }
            return false;
        }
    }
}
using System;
using System.IO;
using yeti.mp3;
using yeti.mp3.configuration;
using yeti.wav;
using yeti.wma;

namespace yeti.conversions
{
    public class WmaConversions : ConversionsBase
    {
        public static void WmaToMp3(string wmafilePath, string outputPath)
        {
            WmaToMp3(File.OpenRead(wmafilePath), new FileStream(outputPath, FileMode.Create), null, 1);
        }

        public static void WmaToMp3(string wmafilePath, string outputPath, Mp3WriterConfig mp3Format)
        {
            WmaToMp3(File.OpenRead(wmafilePath), new FileStream(outputPath, FileMode.Create), mp3Format, 1);
        }

        public static void WmaToMp3(
            string wmafilePath, string outputPath, Mp3WriterConfig mp3Format, int bufferMultiplier)
        {
            WmaToMp3(File.OpenRead(wmafilePath),
                new FileStream(outputPath, FileMode.Create),
                mp3Format,
                bufferMultiplier);
        }

        public static void WmaToMp3(string wmafilePath, Stream outputStream)
        {
            WmaToMp3(File.OpenRead(wmafilePath), outputStream, null, 1);
        }

        public static void WmaToMp3(string wmafilePath, Stream outputStream, Mp3WriterConfig mp3Format)
        {
            WmaToMp3(File.OpenRead(wmafilePath), outputStream, mp3Format, 1);
        }

        public static void WmaToMp3(Stream wmafileStream, string outputPath)
        {
            WmaToMp3(wmafileStream, new FileStream(outputPath, FileMode.Create), null, 1);
        }

        public static void WmaToMp3(Stream wmafileStream, string outputPath, Mp3WriterConfig mp3Format)
        {
            WmaToMp3(wmafileStream, new FileStream(outputPath, FileMode.Create), mp3Format, 1);
        }

        public static void WmaToMp3(
            Stream wmafileStream, string outputPath, Mp3WriterConfig mp3Format, int bufferMultiplier)
        {
            WmaToMp3(wmafileStream, new FileStream(outputPath, FileMode.Create), mp3Format, bufferMultiplier);
        }

        public static void WmaToMp3(Stream wmafileStream, Stream outputStream)
        {
            WmaToMp3(wmafileStream, outputStream, null, 1);
        }

        public static void WmaToMp3(Stream wmafileStream, Stream outputStream, Mp3WriterConfig mp3Format)
        {
            WmaToMp3(wmafileStream, outputStream, mp3Format, 1);
        }

        public static void WmaToMp3(
            Stream wmaInputStream, Stream outputStream, Mp3WriterConfig mp3Format, int bufferMultiplier)
        {
            WmaToMp3Delegate convert = wmaStream =>
                                       {
                                           var writer = new Mp3Writer(outputStream,
                                                            mp3Format ?? new Mp3WriterConfig(wmaStream.Format, new BE_CONFIG(wmaStream.Format)));
                                           var buffer = new byte[writer.OptimalBufferSize*bufferMultiplier];
                                           WriteToFile(writer, wmaStream, buffer);
                                       };

            if (wmaInputStream is WmaStreamReader)
                convert((WmaStreamReader)wmaInputStream);
            else
            {
                using (var wmaStream = new WmaStreamReader(wmaInputStream))
                {
                    convert(wmaStream);
                }
            }
        }

        private delegate void WmaToMp3Delegate(WmaStreamReader wmaStream);

        public static void WmaToMp3(string wmafilePath, string outputPath, uint bitRate)
        {
            WmaToMp3(File.OpenRead(wmafilePath), new FileStream(outputPath, FileMode.Create), bitRate, 1);
        }

        public static void WmaToMp3(string wmafilePath, Stream outputStream, uint bitRate)
        {
            WmaToMp3(File.OpenRead(wmafilePath), outputStream, bitRate, 1);
        }

        public static void WmaToMp3(string wmafilePath, string outputPath, uint bitRate, int bufferMultiplier)
        {
            WmaToMp3(File.OpenRead(wmafilePath), new FileStream(outputPath, FileMode.Create), bitRate, 1);
        }

        public static void WmaToMp3(Stream wmafileStream, string outputPath, uint bitRate)
        {
            WmaToMp3(wmafileStream, new FileStream(outputPath, FileMode.Create), bitRate, 1);
        }

        public static void WmaToMp3(Stream wmafileStream, Stream outputStream, uint bitRate)
        {
            WmaToMp3(wmafileStream, outputStream, bitRate, 1);
        }

        public static void WmaToMp3(
            Stream wmafileStream, string outputPath, uint bitRate, int bufferMultiplier)
        {
            WmaToMp3(wmafileStream, new FileStream(outputPath, FileMode.Create), bitRate, 1);
        }

        public static void WmaToMp3(
            Stream wmaInputStream, Stream outputStream, uint bitRate, int bufferMultiplier)
        {
            using (var wmaStream = new WmaStreamReader(wmaInputStream))
            {
                var writer = new Mp3Writer(outputStream,
                    new Mp3WriterConfig(wmaStream.Format, new BE_CONFIG(wmaStream.Format, bitRate)));
                var buffer = new byte[writer.OptimalBufferSize*bufferMultiplier];
                WriteToFile(writer, wmaStream, buffer);
            }
        }

        public static void WmaToWav(string wmafilePath, string outputPath)
        {
            WmaToWav(wmafilePath, new FileStream(outputPath, FileMode.Create), null, 1);
        }

        public static void WmaToWav(string wmafilePath, string outputPath, WaveFormat waveFormat)
        {
            WmaToWav(wmafilePath, new FileStream(outputPath, FileMode.Create), waveFormat, 1);
        }

        public static void WmaToWav(
            string wmafilePath, string outputPath, WaveFormat waveFormat, int bufferMultiplier)
        {
            WmaToWav(wmafilePath, new FileStream(outputPath, FileMode.Create), waveFormat, bufferMultiplier);
        }

        public static void WmaToWav(string wmafilePath, Stream outputStream)
        {
            WmaToWav(wmafilePath, outputStream, null, 1);
        }

        public static void WmaToWav(string wmafilePath, Stream outputStream, WaveFormat waveFormat)
        {
            WmaToWav(wmafilePath, outputStream, waveFormat, 1);
        }

        public static void WmaToWav(
            string wmafilePath, Stream outputStream, WaveFormat waveFormat, int bufferMultiplier)
        {
            using (var wmaStream = new WmaStreamReader(wmafilePath))
            {
                var writer = new WaveWriter(outputStream, waveFormat ?? wmaStream.Format);
                var buffer = new byte[writer.OptimalBufferSize*bufferMultiplier];
                WriteToFile(writer, wmaStream, buffer);
            }
        }

        public static void WmaToWma(string wmafilePath, string outputPath, WmaWriterConfig wmaFormat)
        {
            WmaToWma(wmafilePath, new FileStream(outputPath, FileMode.Create), wmaFormat, 1);
        }

        public static void WmaToWma(
            string wmafilePath, string outputPath, WmaWriterConfig wmaFormat, int bufferMultiplier)
        {
            WmaToWma(wmafilePath, new FileStream(outputPath, FileMode.Create), wmaFormat, bufferMultiplier);
        }

        public static void WmaToWma(string wmafilePath, Stream outputStream, WmaWriterConfig wmaFormat)
        {
            WmaToWma(wmafilePath, outputStream, wmaFormat, 1);
        }

        public static void WmaToWma(
            string wmafilePath, Stream outputStream, WmaWriterConfig wmaFormat, int bufferMultiplier)
        {
            using (var wmaStream = new WmaStreamReader(wmafilePath))
            {
                var writer = new WmaWriter(outputStream, wmaFormat);
                var buffer = new byte[writer.OptimalBufferSize*bufferMultiplier];
                WriteToFile(writer, wmaStream, buffer);
            }
        }
    }
}
using System;
using System.IO;

namespace yeti.wma
{
    public static class WmaFunctions
    {
        /// <summary>
        /// Combines files into one in the order they are passed in..
        /// </summary>
        /// <param name="filepaths">The file paths to combine</param>
        /// <param name="outputFile">The file path to save the combined file to.</param>
        public static void Combine(string outputFile, params string[] filepaths)
        {
            Combine(outputFile, 1, filepaths);
        }

        /// <summary>
        /// Combines files into one in the order they are passed in..
        /// </summary>
        /// <param name="bufferMultiplier">The multiplier to use against the OptimalBufferSize of the file for the read buffer, sometimes a larger than optimal buffer size is better.</param>
        /// <param name="filepaths">The file paths to combine</param>
        /// <param name="outputFile">The file path to save the combined file to.</param>
        public static void Combine(string outputFile, int bufferMultiplier, params string[] filepaths)
        {
            Combine(new FileStream(outputFile, FileMode.Create), bufferMultiplier, filepaths);
        }

        /// <summary>
        /// Combines files into one in the order they are passed in..
        /// </summary>
        /// <param name="inputStreams">The streams to combine</param>
        /// <param name="outputFile">The file path to save the combined file to.</param>
        public static void Combine(string outputFile, params Stream[] inputStreams)
        {
            Combine(new FileStream(outputFile, FileMode.Create), 1, inputStreams);
        }

        /// <summary>
        /// Combines files into one in the order they are passed in..
        /// </summary>
        /// <param name="bufferMultiplier">The multiplier to use against the OptimalBufferSize of the file for the read buffer, sometimes a larger than optimal buffer size is better.</param>
        /// <param name="inputStreams">The streams to combine</param>
        /// <param name="outputFile">The file path to save the combined file to.</param>
        public static void Combine(string outputFile, int bufferMultiplier, params Stream[] inputStreams)
        {
            Combine(new FileStream(outputFile, FileMode.Create), bufferMultiplier, inputStreams);
        }

        /// <summary>
        /// Combines files into one in the order they are passed in..
        /// </summary>
        /// <param name="filepaths">The file paths to combine</param>
        /// <param name="outputStream">The stream to save the combined file to.</param>
        public static void Combine(Stream outputStream, params string[] filepaths)
        {
            Combine(outputStream, 1, filepaths);
        }

        /// <summary>
        /// Combines files into one in the order they are passed in..
        /// </summary>
        /// <param name="bufferMultiplier">The multiplier to use against the OptimalBufferSize of the file for the read buffer, sometimes a larger than optimal buffer size is better.</param>
        /// <param name="filepaths">The file paths to combine</param>
        /// <param name="outputStream">The stream to save the combined file to.</param>
        public static void Combine(Stream outputStream, int bufferMultiplier, params string[] filepaths)
        {
            var inputStreams = new Stream[filepaths.Length];

            for (var i = 0; i < filepaths.Length; i++)
            {
                inputStreams[i] = new WmaStreamReader(filepaths[i]);
            }

            Combine(outputStream, bufferMultiplier, inputStreams);
        }

        /// <summary>
        /// Combines files into one in the order they are passed in..
        /// </summary>
        /// <param name="inputStreams">The streams to combine</param>
        /// <param name="outputStream">The stream to save the combined file to.</param>
        public static void Combine(Stream outputStream, params Stream[] inputStreams)
        {
            Combine(outputStream, 1, inputStreams);
        }

        /// <summary>
        /// Combines files into one in the order they are passed in..
        /// </summary>
        /// <param name="bufferMultiplier">The multiplier to use against the OptimalBufferSize of the file for the read buffer, sometimes a larger than optimal buffer size is better.</param>
        /// <param name="inputStreams">The streams to combine</param>
        /// <param name="outputStream">The stream to save the combined file to.</param>
        public static void Combine(Stream outputStream, int bufferMultiplier, params Stream[] inputStreams)
        {
            if (inputStreams.Length <= 0)
                return;

            var wmaInput = inputStreams[0] is WmaStreamReader
                ? (WmaStreamReader)inputStreams[0]
                : new WmaStreamReader(inputStreams[0]);

            var wmaOutput = new WmaWriter(outputStream,
                                          wmaInput.Format,
                                          wmaInput.Profile);

            var buffer = new byte[wmaOutput.OptimalBufferSize * bufferMultiplier];

            try
            {
                WriteFile(wmaOutput, wmaInput, buffer);
                for (var i = 1; i < inputStreams.Length; i++)
                {
                    wmaInput = inputStreams[i] is WmaStreamReader
                        ? (WmaStreamReader)inputStreams[i]
                        : new WmaStreamReader(inputStreams[i]);
                    WriteFile(wmaOutput, wmaInput, buffer);
                }
            }
            finally
            {
                wmaOutput.Close();
            }
        }

        /// <summary>
        /// Cuts out a smaller portion of a Wma file.
        /// </summary>
        /// <param name="inputFile">The input file path.</param>
        /// <param name="outputFile">The output file path.</param>
        /// <param name="startTime">The time that the split from the source file should start.</param>
        /// <param name="endTime">The time that the split from the source file should end.</param>
        public static void Split(string inputFile, string outputFile, TimeSpan startTime, TimeSpan endTime)
        {
            Split(inputFile, outputFile, startTime, endTime, 1);
        }

        /// <summary>
        /// Cuts out a smaller portion of a Wma file.
        /// </summary>
        /// <param name="inputFile">The input file path.</param>
        /// <param name="outputFile">The output file path.</param>
        /// <param name="startTime">The time that the split from the source file should start.</param>
        /// <param name="endTime">The time that the split from the source file should end.</param>
        /// <param name="bufferMultiplier">The multiplier to use against the OptimalBufferSize of the file for the read buffer, sometimes a larger than optimal buffer size is better.</param>
        public static void Split(string inputFile, string outputFile, TimeSpan startTime, TimeSpan endTime, int bufferMultiplier)
        {
            Split(inputFile, new FileStream(outputFile, FileMode.Create), startTime, endTime, bufferMultiplier);
        }

        /// <summary>
        /// Cuts out a smaller portion of a Wma file.
        /// </summary>
        /// <param name="inputFile">The input file path.</param>
        /// <param name="outputStream">The stream to write the split portion of the file to.</param>
        /// <param name="startTime">The time that the split from the source file should start.</param>
        /// <param name="endTime">The time that the split from the source file should end.</param>
        public static void Split(string inputFile, Stream outputStream, TimeSpan startTime, TimeSpan endTime)
        {
            Split(inputFile, outputStream, startTime, endTime, 1);
        }

        /// <summary>
        /// Cuts out a smaller portion of a Wma file.
        /// </summary>
        /// <param name="inputFile">The input file path.</param>
        /// <param name="outputStream">The stream to write the split portion of the file to.</param>
        /// <param name="startTime">The time that the split from the source file should start.</param>
        /// <param name="endTime">The time that the split from the source file should end.</param>
        /// <param name="bufferMultiplier">The multiplier to use against the OptimalBufferSize of the file for the read buffer, sometimes a larger than optimal buffer size is better.</param>
        public static void Split(string inputFile, Stream outputStream, TimeSpan startTime, TimeSpan endTime, int bufferMultiplier)
        {
            Split(new FileStream(inputFile, FileMode.Create), outputStream, startTime, endTime, bufferMultiplier);
        }

        /// <summary>
        /// Cuts out a smaller portion of a Wma file.
        /// </summary>
        /// <param name="inputStream">The input stream.</param>
        /// <param name="outputFile">the output file path.</param>
        /// <param name="startTime">The time that the split from the source file should start.</param>
        /// <param name="endTime">The time that the split from the source file should end.</param>
        public static void Split(Stream inputStream, string outputFile, TimeSpan startTime, TimeSpan endTime)
        {
            Split(inputStream, outputFile, startTime, endTime, 1);
        }

        /// <summary>
        /// Cuts out a smaller portion of a Wma file.
        /// </summary>
        /// <param name="inputStream">The input stream.</param>
        /// <param name="outputFile">The output file path.</param>
        /// <param name="startTime">The time that the split from the source file should start.</param>
        /// <param name="endTime">The time that the split from the source file should end.</param>
        /// <param name="bufferMultiplier">The multiplier to use against the OptimalBufferSize of the file for the read buffer, sometimes a larger than optimal buffer size is better.</param>
        public static void Split(Stream inputStream, string outputFile, TimeSpan startTime, TimeSpan endTime, int bufferMultiplier)
        {
            Split(inputStream, new FileStream(outputFile, FileMode.Create), startTime, endTime, bufferMultiplier);
        }

        /// <summary>
        /// Cuts out a smaller portion of a Wma file.
        /// </summary>
        /// <param name="inputStream">The input stream.</param>
        /// <param name="outputStream">The stream to write the split portion of the file to.</param>
        /// <param name="startTime">The time that the split from the source file should start.</param>
        /// <param name="endTime">The time that the split from the source file should end.</param>
        public static void Split(Stream inputStream, Stream outputStream, TimeSpan startTime, TimeSpan endTime)
        {
            Split(inputStream, outputStream, startTime, endTime, 1);
        }

        /// <summary>
        /// Cuts out a smaller portion of a Wma file.
        /// </summary>
        /// <param name="inputStream">The input stream.</param>
        /// <param name="outputStream">The stream to write the split portion to.</param>
        /// <param name="startTime">The time that the split from the source stream should start.</param>
        /// <param name="endTime">The time that the split from the source stream should end.</param>
        /// <param name="bufferMultiplier">The multiplier to use against the OptimalBufferSize of the file for the read buffer, sometimes a larger than optimal buffer size is better.</param>
        public static void Split(Stream inputStream, Stream outputStream, TimeSpan startTime, TimeSpan endTime, int bufferMultiplier)
        {
            var wmaInput = inputStream is WmaStreamReader ? (WmaStreamReader) inputStream : new WmaStreamReader(inputStream);
            var wmaOutput = new WmaWriter(outputStream, wmaInput.Format, wmaInput.Profile);
            int bytesPerSec = wmaInput.Format.nAvgBytesPerSec;
            var stopPosition = (long)(bytesPerSec*endTime.TotalSeconds);
            var buffer = new byte[wmaOutput.OptimalBufferSize * bufferMultiplier];

            var offset = (long) (bytesPerSec * startTime.TotalSeconds);
            offset = offset - (offset % wmaInput.SeekAlign);

            wmaInput.Seek(offset, SeekOrigin.Begin);
            try
            {
                WriteFile(wmaOutput, wmaInput, buffer, stopPosition);
            }
            finally
            {
                wmaOutput.Close();
            }
        }

        private static void WriteFile(WmaWriter wmaOutput, WmaStreamReader wmaInput, byte[] buffer)
        {
            WriteFile(wmaOutput, wmaInput, buffer, -1);
        }

        private static void WriteFile(WmaWriter wmaOutput, WmaStreamReader wmaInput, byte[] buffer, long stopPosition)
        {
            if (stopPosition == -1)
            {
                stopPosition = wmaInput.Length + 1;
            }
            int read;
            //Read the file until we hit our stop position, or the end of the file.
            while (wmaInput.Position < stopPosition && (read = wmaInput.Read(buffer, 0, buffer.Length)) > 0)
            {
                wmaOutput.Write(buffer, 0, read);
            }
        }
    }
}
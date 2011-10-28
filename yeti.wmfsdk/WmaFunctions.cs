using System;
using System.IO;

namespace yeti.wma
{
    public static class WmaFunctions
    {
        /// <summary>
        /// Cuts out a smaller portion of a Wma file.
        /// </summary>
        /// <param name="inputFile">The input file path.</param>
        /// <param name="outputFile">The output file path.</param>
        /// <param name="startTime">The time that the split from the source file should start.</param>
        /// <param name="endTime">The time that the split from the source file shound end.</param>
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
        /// <param name="endTime">The time that the split from the source file shound end.</param>
        /// <param name="bufferMultiplier">The multipler to use against the OptimalBufferSize of the file for the read buffer, sometimes a larger than optimal buffer size is better.</param>
        public static void Split(string inputFile, string outputFile, TimeSpan startTime, TimeSpan endTime, int bufferMultiplier)
        {
            var wmaInput = new WmaStream(inputFile);
            var wmaOutput = new WmaWriter(new FileStream(outputFile, FileMode.Create), wmaInput.Format, wmaInput.Profile);
            var bytesPerSec = wmaInput.Format.nAvgBytesPerSec;
            var stopPosition = (long)(bytesPerSec * endTime.TotalSeconds);
            var buffer = new byte[wmaOutput.OptimalBufferSize * bufferMultiplier];

            wmaInput.Seek((long)(bytesPerSec * startTime.TotalSeconds), SeekOrigin.Begin);
            try
            {
                WriteFile(wmaOutput, wmaInput, buffer, stopPosition);
            }
            finally
            {
                wmaOutput.Close();
            }
        }

        /// <summary>
        /// Combines files into one in the order they are passed in..
        /// </summary>
        /// <param name="filepaths">The filepaths to combine</param>
        /// <param name="outputFile">The filepath to save the combined file to.</param>
        public static void Combine(string outputFile, params string[] filepaths)
        {
            Combine(outputFile, 1, filepaths);
        }

        /// <summary>
        /// Combines files into one in the order they are passed in..
        /// </summary>
        /// <param name="bufferMultiplier">The multipler to use against the OptimalBufferSize of the file for the read buffer, sometimes a larger than optimal buffer size is better.</param>
        /// <param name="filepaths">The filepaths to combine</param>
        /// <param name="outputFile">The filepath to save the combined file to.</param>
        public static void Combine(string outputFile, int bufferMultiplier, params string[] filepaths)
        {
            if (filepaths.Length > 0)
            {
                var wmaInput = new WmaStream(filepaths[0]);
                var wmaOutput = new WmaWriter(new FileStream(outputFile, FileMode.Create),
                    wmaInput.Format,
                    wmaInput.Profile);
                var buffer = new byte[wmaOutput.OptimalBufferSize*bufferMultiplier];

                try
                {
                    WriteFile(wmaOutput, wmaInput, buffer);
                    for (int i = 1; i < filepaths.Length; i++)
                    {
                        WriteFile(wmaOutput, new WmaStream(filepaths[i]), buffer);
                    }
                }
                finally
                {
                    wmaOutput.Close();
                }
            }
        }

        private static void WriteFile(WmaWriter wmaOutput, WmaStream wmaInput, byte[] buffer)
        {
            WriteFile(wmaOutput, wmaInput, buffer, -1);
        }

        private static void WriteFile(WmaWriter wmaOutput, WmaStream wmaInput, byte[] buffer, long stopPosition)
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
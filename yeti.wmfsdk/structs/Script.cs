namespace yeti.wma.structs
{
    /// <summary>
    /// Represent a script in ASF streams
    /// </summary>
    public struct Script
    {
        /// <summary>
        /// Type of the script. Its length must be less than 1024 characters.
        /// </summary>
        public string Type;
        /// <summary>
        /// The script command. 
        /// </summary>
        public string Command;
        /// <summary>
        /// Script time in 100-nanosecond units
        /// </summary>
        public ulong Time;

        /// <summary>
        /// Scrip constructor
        /// </summary>
        /// <param name="type">Script type</param>
        /// <param name="command">Scrip Command</param>
        /// <param name="time">Time in 100-nanosecond units</param>
        public Script(string type, string command, ulong time)
        {
            this.Type = type;
            this.Command = command;
            this.Time = time;
        }
    }
}
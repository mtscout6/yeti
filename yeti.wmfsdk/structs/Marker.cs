namespace yeti.wma.structs
{
    /// <summary>
    /// Represent a marker in ASF streams
    /// </summary>
    public struct Marker
    {
        /// <summary>
        /// Marker name
        /// </summary>
        public string Name;
        /// <summary>
        /// Marker time in 100-nanosecond units
        /// </summary>
        public ulong Time;
        /// <summary>
        /// Marker constructor
        /// </summary>
        /// <param name="name">Name of the marker</param>
        /// <param name="time">Time in 100-nanosecond units</param>
        public Marker(string name, ulong time)
        {
            this.Name = name;
            this.Time = time;
        }
    }
}
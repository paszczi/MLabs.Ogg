namespace Mlabs.Ogg.Container
{
    /// <summary>
    /// Packet
    /// </summary>o
    public class Packet
    {
        public Packet(long fileOffset, long size, int firstPage, int numberOfSegments)
        {
            FileOffset = fileOffset;
            Size = size;
            FirstPage = firstPage;
            NumberOfSegments = numberOfSegments;
        }

        /// <summary>
        /// Gets the ffset from the beginning of the file.
        /// </summary>
        public long FileOffset { get; private set; }


        /// <summary>
        /// Gets the size of the packet
        /// </summary>
        public long Size { get; private set; }


        /// <summary>
        /// Index of the page where packet was started.
        /// </summary>
        public int FirstPage { get; private set; }


        /// <summary>
        /// Number of segments.
        /// </summary>
        public int NumberOfSegments { get; private set; }


        public override string ToString()
        {
            return string.Format("FileOffset: {0}, Size: {1}, FirstPage: {2}, NumberOfSegments: {3}", FileOffset, Size, FirstPage, NumberOfSegments);
        }
    }
}
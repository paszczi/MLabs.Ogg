using System.Collections.Generic;
using Mlabs.Ogg.Container;

namespace Mlabs.Ogg.Streams.Vorbis
{
    /// <summary>
    /// Vorbis audio stream
    /// </summary>
    public class VorbisStream : MediaStream
    {
        public VorbisStream(IEnumerable<Page> pages) : base(pages)
        {
        }


        public override StreamType StreamType
        {
            get { return StreamType.Audio; }
        }


        /// <summary>
        /// Gets the number of audio channels.
        /// </summary>
        public byte AudioChannels { get; internal set; }


        /// <summary>
        /// Get the blocksize 0 value.
        /// </summary>
        public uint BlockSize0 { get; internal set; }


        /// <summary>
        /// Gets the blocksize 1 value.
        /// </summary>
        public uint BlockSize1 { get; internal set; }


        /// <summary>
        /// Gets the framing flag.
        /// </summary>
        public byte FramingFlag { get; internal set; }


        public override string ToString()
        {
            return string.Format("{0}, AudioChannels: {1}, BlockSize0: {2}, BlockSize1: {3}, FramingFlag: {4}", base.ToString(), AudioChannels, BlockSize0, BlockSize1, FramingFlag);
        }
    }
}
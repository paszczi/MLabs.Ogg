using System;
using System.Collections.Generic;
using Mlabs.Ogg.Container;

namespace Mlabs.Ogg.Streams
{
    /// <summary>
    /// Media stream
    /// </summary>
    public abstract class MediaStream : OggStream
    {
        protected MediaStream(IEnumerable<Page> pages) : base(pages)
        {
        }


        /// <summary>
        /// Gets the sample rate.
        /// </summary>
        public uint SampleRate { get; internal set; }


        /// <summary>
        /// Get the maximum bitrate.
        /// </summary>
        public int MaxBitrate { get; internal set; }


        /// <summary>
        /// Gets the minimum bitrate.
        /// </summary>
        public int MinBitrate { get; internal set; }


        /// <summary>
        /// Gets the nominal bitrate.
        /// </summary>
        public int NominalBitrate { get; internal set; }


        /// <summary>
        /// Gets the stream duration.
        /// </summary>
        public TimeSpan Duration { get; internal set; }


        public override string ToString()
        {
            return string.Format("{0}, SampleRate: {1}, MaxBitrate: {2}, MinBitrate: {3}, NominalBitrate: {4}, Duration: {5}", base.ToString(), SampleRate, MaxBitrate, MinBitrate, NominalBitrate, Duration);
        }
    }
}
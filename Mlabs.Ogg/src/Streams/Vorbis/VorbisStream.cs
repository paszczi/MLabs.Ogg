using System;
using System.Collections.Generic;
using Mlabs.Ogg.Container;

namespace Mlabs.Ogg.Streams.Vorbis
{
    /// <summary>
    /// Vorbis audio stream
    /// </summary>
    public class VorbisStream : OggStream
    {
        public VorbisStream(IEnumerable<Page> pages) : base(pages)
        {
        }


        public override StreamType StreamType
        {
            get { return StreamType.Audio; }
        }


        public override TimeSpan Duration
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the number of audio channels.
        /// </summary>
        public byte AudioChannels { get; internal set; }


        /// <summary>
        /// Gets the audio sample rate.
        /// </summary>
        public uint AudioSampleRate { get; internal set; }


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
            return string.Format("AudioChannels: {0}, AudioSampleRate: {1}, MaxBitrate: {2}, MinBitrate: {3}, NominalBitrate: {4}, BlockSize0: {5}, BlockSize1: {6}, FramingFlag: {7}", AudioChannels, AudioSampleRate, MaxBitrate, MinBitrate, NominalBitrate, BlockSize0, BlockSize1, FramingFlag);
        }
    }
}
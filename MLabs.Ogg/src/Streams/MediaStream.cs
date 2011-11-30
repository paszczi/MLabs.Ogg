//
// Copyright (c) 2011 Maciej Paszta
// 
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.Collections.Generic;
using MLabs.Ogg.Container;

namespace MLabs.Ogg.Streams
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
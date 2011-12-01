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

using System.Collections.Generic;
using MLabs.Ogg.Container;

namespace MLabs.Ogg.Streams.Vorbis
{
    /// <summary>
    /// Vorbis audio stream
    /// </summary>
    public class VorbisStream : MediaStream
    {
        public VorbisStream (IEnumerable<Page> pages, IEnumerable<Packet> packets) : base (pages, packets)
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


        /// <summary>
        /// Vorbis stream version.
        /// </summary>
        public byte Version { get; internal set; }


        /// <summary>
        /// Vorbis comments.
        /// </summary>
        public VorbisComments Comments { get; internal set; }


        public override string ToString()
        {
            return string.Format("{0}, AudioChannels: {1}, BlockSize0: {2}, BlockSize1: {3}, FramingFlag: {4}", base.ToString(), AudioChannels, BlockSize0, BlockSize1, FramingFlag);
        }
    }
}
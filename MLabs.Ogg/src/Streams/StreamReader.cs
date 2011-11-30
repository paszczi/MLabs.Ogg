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
using System.IO;
using MLabs.Ogg.Container;
using MLabs.Ogg.Streams.Unknown;
using MLabs.Ogg.Streams.Vorbis;

namespace MLabs.Ogg.Streams
{
    /// <summary>
    /// Reads stream from pages and decode it to appropriate <see cref="OggStream"/> instances.
    /// </summary>
    public class StreamReader
    {
        private readonly IList<StreamDecoder> m_decoders;


        public StreamReader(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException("stream");
            m_decoders = new List<StreamDecoder>
                             {
                                 new VorbisStreamDecoder(stream),
                                 new UnknownStreamDecoder(stream),
                             };
        }


        public OggStream DecodeStream(IList<Page> pages, IList<Packet> packets)
        {
            if (pages == null) throw new ArgumentNullException("pages");
            foreach (var streamDecoder in m_decoders)
            {
                OggStream stream;
                if (streamDecoder.TryDecode(pages, packets, out stream))
                    return stream;
            }

            //we should never reach this code since the last of the decoders
            //can always decode pages
            return null;
        }
    }
}
﻿//
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
using System.IO;
using MLabs.Ogg.Container;

namespace MLabs.Ogg.Streams
{
    public abstract class StreamDecoder
    {
        private readonly Stream m_stream;

        protected StreamDecoder(Stream stream)
        {
            m_stream = stream;
        }


        /// <summary>
        /// Tries to decode the stream.
        /// </summary>
        /// <param name="pages">The pages.</param>
        /// <param name="packets">The packets.</param>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        public abstract bool TryDecode(IList<Page> pages, IList<Packet> packets, out OggStream stream);


        protected Stream FileStream
        {
            get { return m_stream; }
        }


        protected byte[] Read(long fileOffset, int size)
        {
            m_stream.Seek(fileOffset, SeekOrigin.Begin);
            return ReadNoSeek(size);
        }


        protected byte[] ReadNoSeek(int size)
        {
            byte[] buffer = new byte[size];
            if (m_stream.Read(buffer, 0, size) != size)
                throw new IOException("Unable to read " + size + " bytes from the stream");
            return buffer;
        }
    }
}
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MLabs.Ogg.Container;
using MLabs.Ogg.Streams;
using StreamReader = MLabs.Ogg.Streams.StreamReader;

namespace MLabs.Ogg
{
    public class OggReader
    {
        private readonly Stream m_stream;
        private readonly bool m_owns;


        public OggReader (string fileName)
        {
            if (fileName == null) throw new ArgumentNullException ("fileName");
            m_stream = new FileStream (fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            m_owns = true;
        }


        public OggReader (Stream stream)
        {
            if (stream == null) throw new ArgumentNullException ("stream");
            if (!stream.CanSeek) throw new ArgumentException ("Stream must be seekable", "stream");
            if (!stream.CanRead) throw new ArgumentException ("Stream must be readable", "stream");

            m_stream = stream;
            m_owns = false;
        }


        public IOggData Read ()
        {
            long originalOffset = m_stream.Position;

            var p = new PageReader ();
            var sr = new StreamReader (m_stream);
            var packetReader = new PacketReader ();
            //read pages and break them down to streams
            var pagesByStreamId = p.ReadPages (m_stream).GroupBy (e => e.StreamSerialNumber);
            IList<OggStream> streams = new List<OggStream> ();
            foreach (var pages in pagesByStreamId)
            {
                var materializedPages = pages.ToList ();
                var packets = packetReader.ReadPackets (materializedPages).ToList ();
                streams.Add (sr.DecodeStream (materializedPages, packets));
            }

            if (m_owns)
            {
                m_stream.Dispose ();
            }
            else
            {
                //if we didn't create stream rewind it, so that user won't get any surprise :)
                m_stream.Seek (originalOffset, SeekOrigin.Begin);
            }
            return new OggData (streams);
        }
    }
}
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
using System.Linq;
using System.Text;

namespace MLabs.Ogg.Container
{
    /// <summary>
    /// Reads pages from the Ogg file stream.
    /// </summary>
    internal class PageReader
    {
        private readonly byte[] m_headerBuffer = new byte[HeaderInfo.HeaderSize];
        private long m_offset;

        public IEnumerable<Page> ReadPages(Stream stream)
        {
            m_offset = 0;
            if (stream == null) throw new ArgumentNullException("stream");

            while (true)
            {
                var p = ReadPage(stream);
                if (p == null)
                    break;
                p.Segments = ReadSegments(stream, p);
                yield return p;
            }
            yield break;
        }

        private IList<Segment> ReadSegments(Stream stream, Page page)
        {
            IList<Segment> result = new List<Segment>(page.PageSegments);
            int offsetFromSegmentsTable = 0;
            for (int i = 0; i < page.PageSegments; i++)
            {
                int read = stream.ReadByte();
                if (read == -1)
                    throw new PrematureEndOfFileException("Could not read sufficient number of segment sizes");
                var s = new Segment();
                s.Size = read;
                //we must include the size of segments table
                s.FileOffset = m_offset + offsetFromSegmentsTable + page.PageSegments;
                offsetFromSegmentsTable += read;

                result.Add(s);
            }

            if (page.PageSegments > 0)
            {
                var last = result.Last();
                m_offset = last.FileOffset + last.Size;
                //offset from segments' table contains the first packet of net page 
                stream.Seek(offsetFromSegmentsTable, SeekOrigin.Current);
            }

            return result;
        }

        private Page ReadPage(Stream stream)
        {
            var readBytes = stream.Read(m_headerBuffer, 0, HeaderInfo.HeaderSize);

            //EOF
            if (readBytes == 0)
                return null;

            if (readBytes != HeaderInfo.HeaderSize)
                throw new PrematureEndOfFileException();

            var p = new Page();
            p.MagicString = Encoding.ASCII.GetString(m_headerBuffer, HeaderInfo.MagicPattern, HeaderInfo.MagicPatternSize);
            if (p.MagicString != HeaderInfo.MagicPatternString)
                throw new InvalidMagicNumberException(p.MagicString);

            p.PageType = (PageType) m_headerBuffer[HeaderInfo.HeaderType];
            p.Version = m_headerBuffer[HeaderInfo.StreamStructureVersion];
            p.GranulePosition = BitConverter.ToUInt64(m_headerBuffer, HeaderInfo.GranulePosition);
            p.StreamSerialNumber = BitConverter.ToUInt32(m_headerBuffer, HeaderInfo.StreamSerialNumber);
            p.PageNumber = BitConverter.ToUInt32(m_headerBuffer, HeaderInfo.PageNumber);
            p.Checksum = BitConverter.ToUInt32(m_headerBuffer, HeaderInfo.PageChecksum);
            p.PageSegments = m_headerBuffer[HeaderInfo.PageSegments];

            m_offset += HeaderInfo.HeaderSize;
            return p;
        }
    }
}
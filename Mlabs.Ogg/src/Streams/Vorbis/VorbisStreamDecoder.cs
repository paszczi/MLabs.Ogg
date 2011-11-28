using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mlabs.Ogg.Metadata;

namespace Mlabs.Ogg.Streams.Vorbis
{
    public class VorbisStreamDecoder : StreamDecoder
    {
        public VorbisStreamDecoder(Stream stream) : base(stream)
        {
        }

        public override bool CanDecode(IEnumerable<Page> pages)
        {
            var firstPage = pages.FirstOrDefault(p => p.PageType == PageType.BeginningOfStream);
            if (firstPage == null)
                return false;

            byte[] header = Read(firstPage.Segments.First().FileOffset, GetHeaderSize(firstPage));
            return false;
        }

        private int GetHeaderSize (Page p)
        {
            int size = 0;
            foreach (var segment in p.Segments)
            {
                size += segment.Size;
                if (size < 255)
                    break;
            }
            return size;
        }

        public override OggStream Decode(IEnumerable<Page> pages)
        {
            throw new NotImplementedException();
        }
    }
}
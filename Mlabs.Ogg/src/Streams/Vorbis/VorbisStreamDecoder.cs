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
            var firstPage = pages.FirstOrDefault(p => p.HeaderType == HeaderType.BeginningOfStream);
            if (firstPage == null)
                return false;
            return false;
        }

        public override OggStream Decode(IEnumerable<Page> pages)
        {
            throw new NotImplementedException();
        }
    }
}
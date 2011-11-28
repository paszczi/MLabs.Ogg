using System.Collections.Generic;
using System.IO;
using Mlabs.Ogg.Metadata;

namespace Mlabs.Ogg.Streams.Unknown
{
    /// <summary>
    /// Decodes unknown stream
    /// </summary>
    public class UnknownStreamDecoder : StreamDecoder
    {
        public UnknownStreamDecoder(Stream stream) : base(stream)
        {
        }

        public override bool CanDecode(IEnumerable<Page> pages)
        {
            return true;
        }

        public override OggStream Decode(IEnumerable<Page> pages)
        {
            return new UnknownStream(pages);
        }
    }
}
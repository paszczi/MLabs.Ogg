using System.Collections.Generic;
using Mlabs.Ogg.Metadata;

namespace Mlabs.Ogg.Streams.Unknown
{
    /// <summary>
    /// Decodes unknown stream
    /// </summary>
    public class UnknownStreamDecoder : StreamDecoder
    {
        public override bool CanDecoded(IEnumerable<Page> pages)
        {
            return true;
        }

        public override OggStream Decode(IEnumerable<Page> pages)
        {
            return new UnknownStream(pages);
        }
    }
}
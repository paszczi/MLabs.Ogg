using System.Collections.Generic;
using System.IO;
using Mlabs.Ogg.Container;

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


        public override bool TryDecode(IList<Page> pages, IList<Packet> packets, out OggStream stream)
        {
            stream = new UnknownStream(pages);
            return true;
        }
    }
}
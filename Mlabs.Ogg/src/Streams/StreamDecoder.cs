using System.Collections.Generic;
using Mlabs.Ogg.Metadata;

namespace Mlabs.Ogg.Streams
{
    public abstract class StreamDecoder
    {
        /// <summary>
        /// Checks whether the specific decoder can decode this stream.
        /// </summary>
        /// <param name="pages">The pages.</param>
        /// <returns></returns>
        public abstract bool CanDecoded(IEnumerable<Page> pages);


        /// <summary>
        /// Decodes given stream info <see cref="OggStream"/>.
        /// </summary>
        /// <param name="pages">The pages.</param>
        /// <returns></returns>
        public abstract OggStream Decode(IEnumerable<Page> pages);

    }
}
using System;
using System.Collections.Generic;
using Mlabs.Ogg.Metadata;
using Mlabs.Ogg.Streams.Unknown;

namespace Mlabs.Ogg.Streams
{
    /// <summary>
    /// Reads stream from pages and decode it to appropriate <see cref="OggStream"/> instances.
    /// </summary>
    public class StreamReader
    {
        private static readonly IList<StreamDecoder> s_decoders = new List<StreamDecoder>
                                                                      {
                                                                          new UnknownStreamDecoder(),
                                                                      };

        public OggStream DecodeStream(IEnumerable<Page> pages)
        {
            if (pages == null) throw new ArgumentNullException("pages");
            foreach (var streamDecoder in s_decoders)
            {
                if (streamDecoder.CanDecoded(pages))
                    return streamDecoder.Decode(pages);
            }

            //we should never reach this code since the last of the decoders
            //can always decode pages
            return null;
        }
    }
}
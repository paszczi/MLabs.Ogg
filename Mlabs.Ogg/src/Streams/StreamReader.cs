using System;
using System.Collections.Generic;
using System.IO;
using Mlabs.Ogg.Container;
using Mlabs.Ogg.Streams.Unknown;
using Mlabs.Ogg.Streams.Vorbis;

namespace Mlabs.Ogg.Streams
{
    /// <summary>
    /// Reads stream from pages and decode it to appropriate <see cref="OggStream"/> instances.
    /// </summary>
    public class StreamReader
    {
        private readonly IList<StreamDecoder> m_decoders;


        public StreamReader(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException("stream");
            m_decoders = new List<StreamDecoder>
                             {
                                 new VorbisStreamDecoder(stream),
                                 new UnknownStreamDecoder(stream),
                             };
        }


        public OggStream DecodeStream(IList<Page> pages, IList<Packet> packets)
        {
            if (pages == null) throw new ArgumentNullException("pages");
            foreach (var streamDecoder in m_decoders)
            {
                OggStream stream;
                if (streamDecoder.TryDecode(pages, packets, out stream))
                    return stream;
            }

            //we should never reach this code since the last of the decoders
            //can always decode pages
            return null;
        }
    }
}
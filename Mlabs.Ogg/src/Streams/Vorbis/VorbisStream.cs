using System;
using System.Collections.Generic;
using Mlabs.Ogg.Metadata;

namespace Mlabs.Ogg.Streams.Vorbis
{
    /// <summary>
    /// Vorbis audio stream
    /// </summary>
    public class VorbisStream : OggStream
    {
        public VorbisStream(IEnumerable<Page> pages) : base(pages)
        {
        }


        public override StreamType StreamType
        {
            get { return StreamType.Audio; }
        }


        public override TimeSpan Duration
        {
            get { throw new NotImplementedException(); }
        }
    }
}
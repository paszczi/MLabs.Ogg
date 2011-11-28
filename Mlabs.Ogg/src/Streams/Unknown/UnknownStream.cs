using System;
using System.Collections.Generic;
using Mlabs.Ogg.Container;

namespace Mlabs.Ogg.Streams.Unknown
{
    /// <summary>
    /// Stream of unknown stream
    /// </summary>
    public class UnknownStream : OggStream
    {
        public UnknownStream(IEnumerable<Page> pages) : base(pages)
        {
        }


        public override StreamType StreamType
        {
            get { return StreamType.Unknown; }
        }


        public override TimeSpan Duration
        {
            get { return TimeSpan.FromHours(0); }
        }
    }
}
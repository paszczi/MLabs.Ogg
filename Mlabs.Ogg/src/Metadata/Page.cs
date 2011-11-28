﻿using System.Collections.Generic;

namespace Mlabs.Ogg.Metadata
{
    public class Page
    {
        public Page()
        {
            Segments = new List<Segment>();
        }

        public string MagicString { get; set; }

        public HeaderType HeaderType { get; set; }

        public ulong GranulePosition { get; set; }

        public byte Version { get; set; }

        public uint StreamSerialNumber { get; set; }

        public uint PageNumber { get; set; }

        public uint Checksum { get; set; }

        public byte PageSegments { get; set; }

        public IList<Segment> Segments { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace Mlabs.Ogg.Container
{
    /// <summary>
    /// Reads the packets out of the pages.
    /// </summary>
    internal class PacketReader
    {
        private const byte ExtendedSegmentSize = 255;

        public IEnumerable<Packet> ReadPackets(IEnumerable<Page> pages)
        {
            if (pages == null) throw new ArgumentNullException("pages");

            long fileOffset = 0;
            int size = 0;
            int firstPage = 0;
            int pageIndex = 0;
            int numberOfSegments = 0;
            bool hasPacket = false;

            foreach (var page in pages)
            {
                //if the previous page had unfinished packet, this page must be marked as continuation
                if (hasPacket && page.PageType != PageType.Continuation)
                        throw new InvalidStreamException("Packet didn't finish on previous page, but next page is not marked as Continuation");
                
                pageIndex++;
                foreach (var segment in page.Segments)
                {
                    //start new packet
                    if (!hasPacket)
                    {
                        fileOffset = segment.FileOffset;
                        firstPage = pageIndex;
                        hasPacket = true;
                    }
                    numberOfSegments++;
                    size += segment.Size;

                    //packet spans across several segments
                    if (segment.Size == ExtendedSegmentSize) continue;

                    Packet p = new Packet(fileOffset, size, firstPage, numberOfSegments);
                    yield return p;

                    //reset values
                    size = 0;
                    fileOffset = 0;
                    hasPacket = false;
                    numberOfSegments = 0;
                }
            }
            yield break;
        }
    }
}
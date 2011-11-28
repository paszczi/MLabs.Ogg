using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Mlabs.Ogg.Metadata;

namespace Mlabs.Ogg.Streams.Vorbis
{
    public class VorbisStreamDecoder : StreamDecoder
    {
        public VorbisStreamDecoder(Stream stream) : base(stream)
        {
        }

        public override bool CanDecode(IEnumerable<Page> pages)
        {
            var firstPage = pages.FirstOrDefault(p => p.PageType == PageType.BeginningOfStream);
            if (firstPage == null)
                return false;

            byte[] header = Read(firstPage.Segments.First().FileOffset, GetHeaderSize(firstPage));
            if (!IsProperSize(header))
                return false;
            if (!IsProperHeaderType(header))
                return false;
            if (!HasMagicSeq(header))
                return false;
            if (!HasProperVersion(header))
                return false;
            return true;
        }

        private bool HasProperVersion(byte[] header)
        {
            uint version = BitConverter.ToUInt32(header, VorbisHeaderInfo.VersionIndex);
            return version == VorbisHeaderInfo.VorbisVersion;
        }

        private bool IsProperSize(byte[] header)
        {
            return header.Length == VorbisHeaderInfo.IdentificatationHeaderSize;
        }

        private bool IsProperHeaderType(byte[] header)
        {
            //header must be the identification header
            return header[VorbisHeaderInfo.HeaderTypeIndex] == VorbisHeaderInfo.IdentificartionHeader;
        }

        private bool HasMagicSeq(byte[] header)
        {
            string magicSeq = Encoding.ASCII.GetString(header, VorbisHeaderInfo.MagicSeqIndex, VorbisHeaderInfo.MagicSeqSize);
            if (magicSeq != VorbisHeaderInfo.MagicSeq)
                return false;
            return true;
        }

        private int GetHeaderSize(Page p)
        {
            int size = 0;
            foreach (var segment in p.Segments)
            {
                size += segment.Size;
                if (size < 255)
                    break;
            }
            return size;
        }

        public override OggStream Decode(IEnumerable<Page> pages)
        {
            throw new NotImplementedException();
        }
    }
}
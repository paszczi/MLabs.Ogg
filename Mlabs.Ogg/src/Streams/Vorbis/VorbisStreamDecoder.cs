using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Mlabs.Ogg.Container;

namespace Mlabs.Ogg.Streams.Vorbis
{
    public class VorbisStreamDecoder : StreamDecoder
    {
        public VorbisStreamDecoder(Stream stream) : base(stream)
        {
        }


        public override bool TryDecode(IList<Page> pages, IList<Packet> packets, out OggStream stream)
        {
            stream = null;
            var firstPage = pages.FirstOrDefault(p => p.PageType == PageType.BeginningOfStream);
            if (firstPage == null)
                return false;

            byte[] header = Read(firstPage.Segments.First().FileOffset, GetHeaderSize(firstPage));
            if (!IsVorbisStream(header))
                return false;

            stream = Decode(pages, header);
            return true;
        }


        private bool IsVorbisStream(byte[] header)
        {
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


        private OggStream Decode(IEnumerable<Page> pages, byte[] identificationHeader)
        {
            byte audioChannels = identificationHeader[VorbisHeaderInfo.AudioChannelsIndex];
            uint audioSampleRate = BitConverter.ToUInt32(identificationHeader, VorbisHeaderInfo.AudioSampleRateIndex);
            int maxBitrate = BitConverter.ToInt32(identificationHeader, VorbisHeaderInfo.MaximumBitrateIndex);
            int nominalBitrate = BitConverter.ToInt32(identificationHeader, VorbisHeaderInfo.NominalBitrateIndex);
            int minBitrate = BitConverter.ToInt32(identificationHeader, VorbisHeaderInfo.MinimumBitrateIndex);
            uint blockSize0 = (uint) Math.Pow(2, (identificationHeader[VorbisHeaderInfo.BlockSizeIndex] & VorbisHeaderInfo.BlockSize0Mask));
            uint blockSize1 = (uint) Math.Pow(2, identificationHeader[VorbisHeaderInfo.BlockSizeIndex] >> 4);
            byte framingFlag = identificationHeader[VorbisHeaderInfo.FramingFlagIndex];

            VorbisStream vorbis = new VorbisStream(pages)
                                      {
                                          AudioChannels = audioChannels,
                                          SampleRate = audioSampleRate,
                                          BlockSize0 = blockSize0,
                                          BlockSize1 = blockSize1,
                                          FramingFlag = framingFlag,
                                          MaxBitrate = maxBitrate,
                                          MinBitrate = minBitrate,
                                          NominalBitrate = nominalBitrate,
                                      };

            vorbis.Duration = GetDuration(pages, audioSampleRate);
            return vorbis;
        }


        private TimeSpan GetDuration(IEnumerable<Page> pages, uint audioSampleRate)
        {
            var first = pages.FirstOrDefault(p => p.PageType == PageType.BeginningOfStream);
            var last = pages.LastOrDefault(p => p.PageType == PageType.EndOfStream);

            ulong granuleDelta = last.GranulePosition - first.GranulePosition;
            double seconds = (double) granuleDelta/audioSampleRate;
            return TimeSpan.FromSeconds(seconds);
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
    }
}
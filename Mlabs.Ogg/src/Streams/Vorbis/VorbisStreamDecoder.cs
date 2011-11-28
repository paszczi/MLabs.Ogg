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

        public override OggStream Decode(IEnumerable<Page> pages)
        {
            var firstPage = pages.First(p => p.PageType == PageType.BeginningOfStream);
            byte[] header = Read(firstPage.Segments.First().FileOffset, GetHeaderSize(firstPage));

            byte audioChannels = header[VorbisHeaderInfo.AudioChannelsIndex];
            uint audioSampleRate = BitConverter.ToUInt32(header, VorbisHeaderInfo.AudioSampleRateIndex);
            int maxBitrate = BitConverter.ToInt32(header, VorbisHeaderInfo.MaximumBitrateIndex);
            int nominalBitrate = BitConverter.ToInt32(header, VorbisHeaderInfo.NominalBitrateIndex);
            int minBitrate = BitConverter.ToInt32(header, VorbisHeaderInfo.MinimumBitrateIndex);
            uint blockSize0 = (uint) Math.Pow(2, (header[VorbisHeaderInfo.BlockSizeIndex] & VorbisHeaderInfo.BlockSize0Mask));
            uint blockSize1 = (uint) Math.Pow(2, header[VorbisHeaderInfo.BlockSizeIndex] >> 4);
            byte framingFlag = header[VorbisHeaderInfo.FramingFlagIndex];

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
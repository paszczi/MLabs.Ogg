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
            //we need a minimum of free packets for id, comment and setup header
            if (packets.Count < 3)
                return false;

            if (!IsVorbisStream(packets[0], packets[1], packets[2]))
                return false;

            //stream = Decode(pages, header);
            return false;
        }

        private bool IsVorbisStream(Packet idHeader, Packet commentHeader, Packet setupHeader)
        {
            if (idHeader.Size < VorbisHeaderInfo.PacketHeaderSize)
                return false;
            if (commentHeader.Size < VorbisHeaderInfo.PacketHeaderSize)
                return false;
            if (setupHeader.Size < VorbisHeaderInfo.PacketHeaderSize)
                return false;

            return IsCorrectHeader(idHeader, VorbisHeaderInfo.IdHeaderType) &&
                   IsCorrectHeader(commentHeader, VorbisHeaderInfo.CommentHeaderType) &&
                   IsCorrectHeader(setupHeader, VorbisHeaderInfo.SetupHeaderType);
        }


        private bool IsCorrectHeader (Packet headerPacket, byte headerType)
        {
            byte[] header = Read(headerPacket.FileOffset, VorbisHeaderInfo.PacketHeaderSize);
            if (header[VorbisHeaderInfo.HeaderTypeIndex] != headerType)
                return false;
            string magicSeq = Encoding.ASCII.GetString(header, VorbisHeaderInfo.MagicSeqIndex, VorbisHeaderInfo.MagicSeqSize);
            if (magicSeq != VorbisHeaderInfo.MagicSeq)
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
    }
}
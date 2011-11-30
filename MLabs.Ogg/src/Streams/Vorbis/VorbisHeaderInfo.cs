//
// Copyright (c) 2011 Maciej Paszta
// 
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

namespace MLabs.Ogg.Streams.Vorbis
{
    /// <summary>
    /// Information needed to read vorbis header
    /// </summary>
    internal class VorbisHeaderInfo
    {
        
        public const int HeaderTypeIndex = 0;
        public const int IdHeaderSize = 30;
        public const byte IdHeaderType = 1;

        //for now only packet_type + macgic seq
        public const byte CommentHeaderType = 3;
        public const byte VendorLengthIndex = 7;

        public const int SetupHeaderSize = -1;
        public const byte SetupHeaderType = 5;

        public const int PacketHeaderSize = 7;

        public const int MagicSeqIndex = 1;
        public const int MagicSeqSize = 6;
        public const string MagicSeq = "vorbis";

        public const int VersionIndex = 7;

        public const int AudioChannelsIndex = 11;

        public const int AudioSampleRateIndex = 12;

        public const int MaximumBitrateIndex = 16;

        public const int NominalBitrateIndex = 20;

        public const int MinimumBitrateIndex = 24;

        public const int BlockSizeIndex = 28;

        public const int FramingFlagIndex = 29;

        public const int BlockSize0Mask = 0x0f;
    }
}
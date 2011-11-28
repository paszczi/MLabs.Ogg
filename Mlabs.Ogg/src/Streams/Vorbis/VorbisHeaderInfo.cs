namespace Mlabs.Ogg.Streams.Vorbis
{
    /// <summary>
    /// Information needed to read vorbis header
    /// </summary>
    internal class VorbisHeaderInfo
    {
        public const int HeaderTypeIndex = 0;
        public const int IdentificatationHeaderSize = 30;
        public const int IdentificartionHeader = 1;

        public const int MagicSeqIndex = 1;
        public const int MagicSeqSize = 6;
        public const string MagicSeq = "vorbis";

        public const int VersionIndex = 7;
        public const uint VorbisVersion = 0;

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
namespace Mlabs.Ogg.Streams.Vorbis
{
    /// <summary>
    /// Information needed to read vorbis header
    /// </summary>
    public class VorbisHeaderInfo
    {
        public const int HeaderTypeIndex = 0;
        public const int IdentificatationHeaderSize = 30;
        public const int IdentificartionHeader = 1;
        public const int MagicSeqIndex = 1;
        public const int MagicSeqSize = 6;
        public const string MagicSeq = "vorbis";
        public const int VersionIndex = 7;
        public const uint VorbisVersion = 0;
    }
}
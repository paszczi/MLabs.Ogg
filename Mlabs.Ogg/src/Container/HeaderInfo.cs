namespace Mlabs.Ogg.Container
{
    public static class HeaderInfo
    {
        public const int HeaderSize = 27;
        public const int MagicPattern = 0;
        public const int MagicPatternSize = 4;
        public const int StreamStructureVersion = 4;
        public const int HeaderType = 5;
        public const int GranulePosition = 6;
        public const int StreamSerialNumber = 14;
        public const int PageNumber = 18;
        public const int PageChecksum = 22;
        public const int PageSegments = 26;

        public const string MagicPatternString = "OggS";
    }
}
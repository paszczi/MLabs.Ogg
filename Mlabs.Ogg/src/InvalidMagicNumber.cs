using System;
using Mlabs.Ogg.Container;

namespace Mlabs.Ogg
{
    [Serializable]
    public class InvalidMagicNumberException : Exception
    {
        public InvalidMagicNumberException(string magicPattern)
            : base(string.Format("Expected magic pattern {0} but got {1}", HeaderInfo.MagicPatternString, magicPattern))
        {
        }
    }
}
using System;
using System.Runtime.Serialization;

namespace Mlabs.Ogg
{
    [Serializable]
    public class PrematureEndOfFileException : Exception
    {
        public PrematureEndOfFileException()
        {
        }

        public PrematureEndOfFileException(string message) : base(message)
        {
        }

        public PrematureEndOfFileException(string message, Exception inner) : base(message, inner)
        {
        }

        protected PrematureEndOfFileException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
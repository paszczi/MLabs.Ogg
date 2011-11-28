using System;
using System.Runtime.Serialization;

namespace Mlabs.Ogg
{
    [Serializable]
    public class InvalidStreamException : Exception
    {
        public InvalidStreamException() : base ("Provided stream is not a valid Ogg stream")
        {
        }

        public InvalidStreamException(string message) : base(message)
        {
        }

        public InvalidStreamException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InvalidStreamException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
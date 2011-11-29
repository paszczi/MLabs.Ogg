using System.Collections.Generic;
using System.IO;
using Mlabs.Ogg.Container;

namespace Mlabs.Ogg.Streams
{
    public abstract class StreamDecoder
    {
        private readonly Stream m_stream;

        protected StreamDecoder(Stream stream)
        {
            m_stream = stream;
        }


        /// <summary>
        /// Tries to decode the stream.
        /// </summary>
        /// <param name="pages">The pages.</param>
        /// <param name="packets">The packets.</param>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        public abstract bool TryDecode(IList<Page> pages, IList<Packet> packets, out OggStream stream);


        protected byte[] Read(long fileOffset, int size)
        {
            byte[] buffer = new byte[size];
            m_stream.Seek(fileOffset, SeekOrigin.Begin);
            if (m_stream.Read(buffer, 0, size) != size)
                throw new IOException("Unable to read " + size + " bytes from the stream");
            return buffer;
        }
    }
}
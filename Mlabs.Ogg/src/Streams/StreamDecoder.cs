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
        /// Checks whether the specific decoder can decode this stream.
        /// </summary>
        /// <param name="pages">The pages.</param>
        /// <returns></returns>
        public abstract bool CanDecode(IEnumerable<Page> pages);


        /// <summary>
        /// Decodes given stream info <see cref="OggStream"/>.
        /// </summary>
        /// <param name="pages">The pages.</param>
        /// <returns></returns>
        public abstract OggStream Decode(IEnumerable<Page> pages);


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
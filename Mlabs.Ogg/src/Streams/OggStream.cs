using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Mlabs.Ogg.Container;

namespace Mlabs.Ogg.Streams
{
    /// <summary>
    /// Generic Ogg stream
    /// </summary>
    public abstract class OggStream : IEnumerable<Page>
    {
        private readonly IEnumerable<Page> m_pages;

        protected OggStream(IEnumerable<Page> pages)
        {
            m_pages = new ReadOnlyCollection<Page>(pages.ToList());
            SerialNumber = m_pages.First().StreamSerialNumber;
        }


        /// <summary>
        /// Gets the stream serial number.
        /// </summary>
        public virtual uint SerialNumber { get; protected set; }


        /// <summary>
        /// Gets the stream type.
        /// </summary>
        public abstract StreamType StreamType { get; }


        public IEnumerator<Page> GetEnumerator()
        {
            return m_pages.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return string.Format("SerialNumber: {0}, StreamType: {1}", SerialNumber, StreamType);
        }
    }
}
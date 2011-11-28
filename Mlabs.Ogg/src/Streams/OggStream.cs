using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Mlabs.Ogg.Container;

namespace Mlabs.Ogg.Streams
{
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


        /// <summary>
        /// Gets the stream duration.
        /// </summary>
        public abstract TimeSpan Duration { get; }


        public IEnumerator<Page> GetEnumerator()
        {
            return m_pages.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
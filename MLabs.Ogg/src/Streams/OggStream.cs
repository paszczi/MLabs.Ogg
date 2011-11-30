//
// Copyright (c) 2011 Maciej Paszta
// 
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MLabs.Ogg.Container;

namespace MLabs.Ogg.Streams
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
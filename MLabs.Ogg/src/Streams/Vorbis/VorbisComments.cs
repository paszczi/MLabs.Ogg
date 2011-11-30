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

namespace Mlabs.Ogg.Streams.Vorbis
{
    /// <summary>
    /// Contains comments as specified by Vorbis documentation.
    /// </summary>
    public class VorbisComments : IEnumerable<KeyValuePair<string, string>>
    {
        private readonly Dictionary<string, string> m_userComments = new Dictionary<string, string>();

        public VorbisComments(string vendor, IEnumerable<string> userComments)
        {
            Vendor = vendor;
            foreach (var userComment in userComments)
            {
                string[] parts = userComment.Split('=');
                if (parts.Length != 2)
                    continue;
                m_userComments.Add(parts[0].ToUpper(), parts[1]);
            }
            foreach (var userComment in m_userComments)
            {
            }
        }


        /// <summary>
        /// Gets the vendor string
        /// </summary>
        public string Vendor { get; private set; }


        /// <summary>
        /// Gets the user comment specified by key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string this[string key]
        {
            get { return m_userComments[key.ToUpper()]; }
        }


        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return m_userComments.GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
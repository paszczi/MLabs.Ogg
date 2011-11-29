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
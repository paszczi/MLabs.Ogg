using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mlabs.Ogg.Metadata;

namespace Mlabs.Ogg
{
    public class OggReader
    {
        private readonly Stream m_fileStream;
        private readonly bool m_owns;

        public OggReader(string fileName)
        {
            if (fileName == null) throw new ArgumentNullException("fileName");
            m_fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            m_owns = true;
        }


        public OggReader(Stream fileStream)
        {
            if (fileStream == null) throw new ArgumentNullException("fileStream");
            m_fileStream = fileStream;
            m_owns = false;
        }


        public IStreamInfo Read()
        {
            var p = new PageReader();
            var pages = p.ReadPages(m_fileStream).ToList();
            if (m_owns)
                m_fileStream.Dispose();
            return null;
        }
    }
}
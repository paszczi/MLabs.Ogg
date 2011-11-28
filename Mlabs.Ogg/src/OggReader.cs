using System;
using System.IO;
using System.Linq;

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


        public IOggInfo Read()
        {
            long originalOffset = m_fileStream.Position;

            var p = new PageReader();
            //read pages and break them down to streams
            var pages = p.ReadPages(m_fileStream).GroupBy(e => e.StreamSerialNumber);
            

            if (m_owns)
            {
                m_fileStream.Dispose();
            }
            else
            {
                //if we didn't create stream rewind it, so that user won't get any surprise :)
                m_fileStream.Seek(originalOffset, SeekOrigin.Begin);
            }
            return null;
        }
    }
}
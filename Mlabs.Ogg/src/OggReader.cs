using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mlabs.Ogg.Container;
using Mlabs.Ogg.Streams;
using StreamReader = Mlabs.Ogg.Streams.StreamReader;

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
            if (!fileStream.CanSeek) throw new ArgumentException("Stream must be seekable", "fileStream");
            if (!fileStream.CanRead) throw new ArgumentException("Stream must be readable", "fileStream");

            m_fileStream = fileStream;
            m_owns = false;
        }


        public IOggInfo Read()
        {
            long originalOffset = m_fileStream.Position;

            var p = new PageReader();
            var sr = new StreamReader(m_fileStream);
            var packetReader = new PacketReader();
            //read pages and break them down to streams
            var pagesByStreamId = p.ReadPages(m_fileStream).GroupBy(e => e.StreamSerialNumber);
            IList<OggStream> streams = new List<OggStream>();
            foreach (var pages in pagesByStreamId)
            {
                var materializedPages = pages.ToList();
                var packets = packetReader.ReadPackets(materializedPages).ToList();
                streams.Add(sr.DecodeStream(materializedPages, packets));
            }

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
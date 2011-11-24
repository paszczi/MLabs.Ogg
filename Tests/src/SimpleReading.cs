using Mlabs.Ogg;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class SimpleReading
    {
        [Test]
        public void Read()
        {
            var r = new OggReader("Lumme-Badloop.ogg");
            r.Read();
        }
    }
}
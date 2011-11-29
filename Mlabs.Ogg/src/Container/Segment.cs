namespace Mlabs.Ogg.Container
{
    public class Segment
    {
        public int Size { get; set; }
        public long FileOffset { get; set; }

        public override string ToString()
        {
            return string.Format("Size: {0}, FileOffset: {1}", Size, FileOffset);
        }
    }
}
namespace Mlabs.Ogg.Metadata
{
    public enum HeaderType : byte
    {
        Continuation = 0x01,

        BeginningOfStream = 0x02,

        EndOfStream = 0x04,
    }
}
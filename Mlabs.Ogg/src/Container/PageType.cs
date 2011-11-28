namespace Mlabs.Ogg.Container
{
    public enum PageType : byte
    {
        /// <summary>
        /// Page is normal page
        /// </summary>
        Normal = 0x00,


        /// <summary>
        /// Page is a continuation of a previous page
        /// </summary>
        Continuation = 0x01,


        /// <summary>
        /// Page is the beginning of a stream
        /// </summary>
        BeginningOfStream = 0x02,


        /// <summary>
        /// Page is the end of the stream
        /// </summary>
        EndOfStream = 0x04,
    }
}
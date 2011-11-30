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

namespace Mlabs.Ogg.Container
{
    /// <summary>
    /// Packet
    /// </summary>o
    public class Packet
    {
        public Packet(long fileOffset, int size, int firstPage, int numberOfSegments)
        {
            FileOffset = fileOffset;
            Size = size;
            FirstPage = firstPage;
            NumberOfSegments = numberOfSegments;
        }

        /// <summary>
        /// Gets the ffset from the beginning of the file.
        /// </summary>
        public long FileOffset { get; private set; }


        /// <summary>
        /// Gets the size of the packet
        /// </summary>
        public int Size { get; private set; }


        /// <summary>
        /// Index of the page where packet was started.
        /// </summary>
        public int FirstPage { get; private set; }


        /// <summary>
        /// Number of segments.
        /// </summary>
        public int NumberOfSegments { get; private set; }


        public override string ToString()
        {
            return string.Format("FileOffset: {0}, Size: {1}, FirstPage: {2}, NumberOfSegments: {3}", FileOffset, Size, FirstPage, NumberOfSegments);
        }
    }
}
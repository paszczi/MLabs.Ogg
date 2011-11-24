using System;
using System.Runtime.InteropServices;

namespace Mlabs.Ogg
{
    public static class Converters
    {
        public unsafe static T ToStruct<T>(this byte[] source)
            where T : struct
        {
            T result;
            unsafe
            {
                fixed (byte* pointer = source)
                {
                    var sourcePointer = (IntPtr) pointer;
                    result = (T) Marshal.PtrToStructure(sourcePointer, typeof(T));
                }
            }
            return result;
        }
    }
}
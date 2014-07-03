using System;
using System.Text;

namespace FSerialize
{
    namespace Utilities
    {
        public class StringCompressor
        {
            public static Byte[] CompressString(string value)
            {
                if (value != "")
                    return CLZF.Compress(Encoding.UTF8.GetBytes(value));
                else
                    return new byte[] { };
            }

            public static string DecompressString(Byte[] value)
            {
                if (value.Length > 0)
                    return Encoding.UTF8.GetString(CLZF.Decompress(value));
                else
                    return "";
            }
        }
    }
}

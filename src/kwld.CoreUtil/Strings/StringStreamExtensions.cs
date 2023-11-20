using System.IO;
using System.Text;

namespace kwld.CoreUtil.Strings
{
    /// <summary>
    /// Extensions to work with string's and streams.
    /// </summary>
    public static class StringStreamExtensions
    {
        private static readonly Encoding ASCIIEncodeWithStripper =
            Encoding.GetEncoding(Encoding.ASCII.EncodingName,
                new EncoderReplacementFallback(string.Empty),
                new DecoderReplacementFallback());

        /// <summary>
        /// Convert text to a UTF8 stream of bytes
        /// </summary>
        public static Stream AsUTF8Stream(this string text)
            => new MemoryStream(Encoding.UTF8.GetBytes(text));

        /// <summary>
        /// Convert text to a ASCII stream of bytes.
        /// </summary>
        public static Stream AsASCIIStream(this string text)
        {
            return new MemoryStream(ASCIIEncodeWithStripper.GetBytes(text));
        }
    }
}
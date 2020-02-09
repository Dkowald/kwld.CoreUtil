using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;

namespace kwd.CoreUtil.Streams
{
    /// <summary>
    /// Sugar for <see cref="TextReader"/>
    /// </summary>
     public static class TextReaderExtensions
    {
        /// <summary>
        /// Read lines from a stream using <see cref="TextReader.ReadLine"/>.
        /// </summary>
        public static IEnumerable<string> ReadLines(this TextReader rd)
        {
            var line = rd.ReadLine();
            while (line != null)
            {
                yield return line;
                line = rd.ReadLine();
            }
        }

        /// <summary>
        /// Read lines from a stream using <see cref="TextReader.ReadLineAsync"/>.
        /// </summary>
        public static async IAsyncEnumerable<string> 
            ReadLinesAsync(this TextReader rd, 
                [EnumeratorCancellation]CancellationToken cancel = default)
        {
            var line = await rd.ReadLineAsync();
            while (line != null)
            {
                yield return line;
                cancel.ThrowIfCancellationRequested();
                line = await rd.ReadLineAsync();
            }
        }
    }
}
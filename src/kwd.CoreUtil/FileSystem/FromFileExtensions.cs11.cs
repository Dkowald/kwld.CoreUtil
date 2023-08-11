#if NET7_0_OR_GREATER
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Text;
using System.Threading;

namespace kwd.CoreUtil.FileSystem
{
    public static partial class FromFileExtensions
    {
        /// <inheritdoc cref="File.ReadLinesAsync(string,Encoding,CancellationToken)"/>
        public static IAsyncEnumerable<string> ReadLinesAsync(this FileInfo fileInfo, Encoding encoding,
            CancellationToken cancel = default) =>
            File.ReadLinesAsync(fileInfo.FullName, encoding, cancel);

        /// <inheritdoc cref="File.ReadLinesAsync(string,Encoding,CancellationToken)"/>
        public static IAsyncEnumerable<string> ReadLinesAsync(this IFileInfo fileInfo, Encoding encoding,
            CancellationToken cancel = default) =>
            fileInfo.FileSystem.File.ReadLinesAsync(fileInfo.FullName, encoding, cancel);

        /// <inheritdoc cref="File.ReadLinesAsync(string,CancellationToken)"/>
        public static IAsyncEnumerable<string> ReadLinesAsync(this FileInfo fileInfo, CancellationToken cancel = default) =>
            File.ReadLinesAsync(fileInfo.FullName, cancel);

        /// <inheritdoc cref="File.ReadLinesAsync(string,CancellationToken)"/>
        public static IAsyncEnumerable<string> ReadLinesAsync(this IFileInfo fileInfo, CancellationToken cancel = default) =>
            fileInfo.FileSystem.File.ReadLinesAsync(fileInfo.FullName, cancel);
    }
}
#endif

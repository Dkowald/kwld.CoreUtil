#if NET6_0_OR_GREATER

using System.IO;
using System.IO.Abstractions;
using Microsoft.Win32.SafeHandles;

namespace kwld.CoreUtil.FileSystem
{
    /// <summary>
    /// Extensions for <see cref="FileInfo"/> to include missing function from <see cref="File"/>.
    /// </summary>
    public static partial class FromFileExtensions
    {
        /// <inheritdoc cref="File.OpenHandle"/>
        public static SafeFileHandle OpenHandle(this FileInfo fileInfo,
            FileMode mode = FileMode.Open,
            FileAccess access = FileAccess.Read,
            FileShare share = FileShare.Read,
            FileOptions options = FileOptions.None,
            long preAllocationSize = 0)
            => File.OpenHandle(fileInfo.FullName, mode, access, share, options, preAllocationSize);

        /// <inheritdoc cref="File.OpenHandle"/>
        public static SafeFileHandle OpenHandle(this IFileInfo fileInfo,
            FileMode mode = FileMode.Open,
            FileAccess access = FileAccess.Read,
            FileShare share = FileShare.Read,
            FileOptions options = FileOptions.None,
            long preAllocationSize = 0)
            => File.OpenHandle(fileInfo.FullName, mode, access, share, options, preAllocationSize);
    }
}
#endif
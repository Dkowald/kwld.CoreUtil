#if NET7_0_OR_GREATER
using System.IO;
using System.IO.Abstractions;

namespace kwd.CoreUtil.FileSystem
{
    public static partial class FromDirectoryExtensions
    {
        /// <inheritdoc cref="Directory.CreateTempSubdirectory"/>
        public static DirectoryInfo CreateTempSubdirectory(this DirectoryInfo path, string? prefix = null) =>
            Directory.CreateTempSubdirectory(prefix);

        /// <inheritdoc cref="Directory.CreateTempSubdirectory"/>
        public static IDirectoryInfo CreateTempSubdirectory(this IDirectoryInfo path, string? prefix = null) =>
            path.FileSystem.DirectoryInfo.Wrap(Directory.CreateTempSubdirectory(prefix));
    }
}

#endif
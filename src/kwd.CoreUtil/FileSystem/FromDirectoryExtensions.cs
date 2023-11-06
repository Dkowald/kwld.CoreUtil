using System.IO;
using System.IO.Abstractions;

namespace kwd.CoreUtil.FileSystem
{
    /// <summary>
    /// Extensions for <see cref="DirectoryInfo"/> to include missing functions from <see cref="Directory"/>.
    /// </summary>
    public static partial class FromDirectoryExtensions
    {
        /// <summary>
        /// See <see cref="Directory.SetCurrentDirectory(string)"/> <br />
        /// Sets the application&amp;#39;s current working directory to the specified directory.
        /// </summary>
        public static void SetCurrentDirectory(this DirectoryInfo path) =>
            Directory.SetCurrentDirectory(path.FullName);

        /// <inheritdoc cref="SetCurrentDirectory(DirectoryInfo)"/>
        public static void SetCurrentDirectory(this IDirectoryInfo path)
            => path.FileSystem.Directory.SetCurrentDirectory(path.FullName);
    }
}

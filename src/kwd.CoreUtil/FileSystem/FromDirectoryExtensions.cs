using System;
using System.IO;
using System.IO.Abstractions;
using System.Linq;

namespace kwd.CoreUtil.FileSystem
{
    /// <summary>
    /// Extensions for <see cref="DirectoryInfo"/> to include missing functions from <see cref="Directory"/>.
    /// </summary>
    public static class FromDirectoryExtensions
    {
        /// <summary>Moves a <see cref="T:System.IO.DirectoryInfo"></see> instance and its contents to a new path.</summary>
        public static void MoveTo(this DirectoryInfo src, DirectoryInfo destDir) =>
            src.MoveTo(destDir.FullName);

        /// <summary>Moves a <see cref="T:IDirectoryInfo"></see> instance and its contents to a new path.</summary>
        public static void MoveTo(this IDirectoryInfo src, IDirectoryInfo destDir) =>
            src.MoveTo(destDir.FullName);

        /// <summary>
        /// See <see cref="Directory.SetCurrentDirectory(string)"/> <br />
        /// Sets the application&amp;#39;s current working directory to the specified directory.
        /// </summary>
        public static void SetCurrentDirectory(this DirectoryInfo path) =>
            Directory.SetCurrentDirectory(path.FullName);

        /// <inheritdoc cref="SetCurrentDirectory(DirectoryInfo)"/>
        public static void SetCurrentDirectory(this IDirectoryInfo path)
        {
            path.FileSystem.Directory.SetCurrentDirectory(path.FullName);
        }

        /// <summary>
        /// Get a child path (without create)
        /// </summary>
        /// <remarks>
        /// Will be removed after version 1.3
        /// </remarks>
        [Obsolete("Use HelperExtensions.GetFolder")]
        public static DirectoryInfo Get(this DirectoryInfo dir, params string[] path) =>
            new DirectoryInfo(Path.Combine(
                new[]{dir.FullName}
                    .Union(path).ToArray()));
    }
}

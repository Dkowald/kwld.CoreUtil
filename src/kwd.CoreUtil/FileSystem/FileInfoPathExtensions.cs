using System;
using System.IO;

namespace kwd.CoreUtil.FileSystem
{
    /// <summary>
    /// Extensions for <see cref="FileInfo"/> to include missing functions from <see cref="Path"/>.
    /// </summary>
    public static class FileInfoPathExtensions
    {
        /// <summary>
        /// See <see cref="Path.ChangeExtension(string, string)"/> <br />
        /// Changes the extension of a path string.
        /// </summary>
        public static FileInfo ChangeExtension(this FileInfo item, string extension) =>
            new FileInfo(Path.ChangeExtension(item?.FullName, extension));

        /// <summary>
        /// See <see cref="Path.GetExtension(string)"/>
        /// Returns the extension of the specified path string.
        /// </summary>
        public static string? GetExtension(this FileInfo item) =>
            Path.GetExtension(item?.FullName);

        /// <summary>
        /// See <see cref="Path.GetFileNameWithoutExtension(string)"/>
        /// Returns the file name of the specified path string without the extension.
        /// </summary>
        public static string? GetFileNameWithoutExtension(this FileInfo item) =>
            Path.GetFileNameWithoutExtension(item?.FullName);

        /// <summary>
        /// See <see cref="Path.GetPathRoot(string)"/> <br />
        /// Gets the root directory information of the specified path.
        /// </summary>
        public static DirectoryInfo GetPathRoot(this FileInfo item) =>
            new DirectoryInfo(Path.GetPathRoot(item?.FullName));

        /// <summary>
        /// See <see cref="Path.HasExtension(string)"/> <br />
        /// Determines whether a path includes a file name extension.
        /// </summary>
        public static bool HasExtension(this FileInfo item) =>
            Path.HasExtension(item?.FullName);

        /// <summary>
        /// See <see cref="Path.GetRelativePath(string, string)"/>
        /// </summary>
        public static string GetRelativePath(this FileInfo item, string relativeTo) =>
            Path.GetRelativePath(relativeTo ?? "", 
                item?.FullName ?? throw new ArgumentNullException(nameof(relativeTo)));

        /// <summary>
        /// See <see cref="Path.GetRelativePath(string, string)"/>
        /// </summary>
        public static string GetRelativePath(this DirectoryInfo item, string relativeTo) =>
            Path.GetRelativePath(relativeTo ?? "", 
                item?.FullName ?? throw new ArgumentNullException(nameof(relativeTo)));

        /// <summary>
        /// See <see cref="Path.GetRelativePath(string, string)"/>
        /// </summary>
        public static string GetRelativePath(this FileInfo item, DirectoryInfo relativeTo) =>
            Path.GetRelativePath(relativeTo?.FullName ?? throw new ArgumentNullException(nameof(relativeTo)), 
                item?.FullName ?? throw new ArgumentNullException(nameof(item)));

        /// <summary>
        /// See <see cref="Path.GetRelativePath(string, string)"/>
        /// </summary>
        public static string GetRelativePath(this DirectoryInfo item, DirectoryInfo relativeTo) =>
            Path.GetRelativePath(relativeTo?.FullName ?? throw new ArgumentNullException(nameof(relativeTo)), 
                item?.FullName ?? throw new ArgumentNullException(nameof(item)));
    }
}
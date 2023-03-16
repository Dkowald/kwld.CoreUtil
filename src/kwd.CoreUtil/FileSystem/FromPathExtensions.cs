using System.IO;
using System.IO.Abstractions;

namespace kwd.CoreUtil.FileSystem
{
    /// <summary>
    /// Extensions for <see cref="FileInfo"/> to include missing functions from <see cref="Path"/>.
    /// </summary>
    public static class FromPathExtensions
    {
        /// <summary>
        /// See <see cref="Path.ChangeExtension(string, string)"/> <br />
        /// Changes the extension of a path string.
        /// </summary>
        public static FileInfo ChangeExtension(this FileInfo item, string extension) =>
            new FileInfo(Path.ChangeExtension(item.FullName, extension));

        /// <inheritdoc cref="ChangeExtension(FileInfo,string)"/>
        public static IFileInfo ChangeExtension(this IFileInfo item, string extension) =>
            item.FileSystem.FileInfo.New(
                item.FileSystem.Path.ChangeExtension(item.FullName, extension));
        
        /// <summary>
        /// See <see cref="Path.GetExtension(string)"/>
        /// Returns the extension of the specified path string.
        /// </summary>
        public static string GetExtension(this FileInfo item) =>
            Path.GetExtension(item.FullName);

        /// <inheritdoc cref="GetExtension(FileInfo)"/>
        public static string GetExtension(this IFileInfo item) =>
            item.FileSystem.Path.GetExtension(item.FullName);

        /// <summary>
        /// See <see cref="Path.GetFileNameWithoutExtension(string)"/>
        /// Returns the file name of the specified path string without the extension.
        /// </summary>
        public static string GetFileNameWithoutExtension(this FileInfo item) =>
            Path.GetFileNameWithoutExtension(item.FullName);

        /// <inheritdoc cref="GetFileNameWithoutExtension(FileInfo)"/>
        public static string GetFileNameWithoutExtension(this IFileInfo item) =>
            item.FileSystem.Path.GetFileNameWithoutExtension(item.FullName);
        
        /// <summary>
        /// See <see cref="Path.HasExtension(string)"/> <br />
        /// Determines whether a path includes a file name extension.
        /// </summary>
        public static bool HasExtension(this FileInfo item) =>
            Path.HasExtension(item.FullName);

        /// <inheritdoc cref="HasExtension(FileInfo)"/>
        public static bool HasExtension(this IFileInfo item) =>
            item.FileSystem.Path.HasExtension(item.FullName);

        /// <summary>
        /// Return the relative path to <paramref name="item"/>
        /// based on the <paramref name="relativeTo"/> root path, or
        /// <paramref name="item"/>'s FullName if not relative.
        /// <br/>See also: <seealso cref="Path.GetRelativePath(string, string)"/>
        /// </summary>
        public static string GetRelativePath(this FileInfo item, string relativeTo) =>
            Path.GetRelativePath(relativeTo, item.FullName);

        /// <inheritdoc cref="GetRelativePath(FileInfo,string)"/>
        public static string GetRelativePath(this IFileInfo item, string relativeTo) =>
            item.FileSystem.Path.GetRelativePath(relativeTo, item.FullName);

        /// <summary>
        /// Return the relative path to <paramref name="item"/>
        /// based on the <paramref name="relativeTo"/> root path, or
        /// <paramref name="item"/>'s FullName if not relative.
        /// <br/>See also: <seealso cref="Path.GetRelativePath(string, string)"/>
        /// </summary>
        public static string GetRelativePath(this DirectoryInfo item, string relativeTo) =>
            Path.GetRelativePath(relativeTo, item.FullName);

        /// <inheritdoc cref="GetRelativePath(DirectoryInfo,string)"/>
        public static string GetRelativePath(this IDirectoryInfo item, string relativeTo) =>
            item.FileSystem.Path.GetRelativePath(relativeTo, item.FullName);

        /// <summary>
        /// Return the relative path to <paramref name="item"/>
        /// based on the <paramref name="relativeTo"/> root path, or
        /// <paramref name="item"/>'s FullName if not relative.
        /// <br/>See also: <seealso cref="Path.GetRelativePath(string, string)"/>
        /// </summary>
        public static string GetRelativePath(this FileInfo item, DirectoryInfo relativeTo) =>
            Path.GetRelativePath(relativeTo.FullName , item.FullName);

        /// <inheritdoc cref="GetRelativePath(FileInfo,DirectoryInfo)"/>
        public static string GetRelativePath(this IFileInfo item, IDirectoryInfo relativeTo) =>
            item.FileSystem.Path.GetRelativePath(relativeTo.FullName, item.FullName);

        /// <summary>
        /// Return the relative path to <paramref name="item"/>
        /// based on the <paramref name="relativeTo"/> root path, or
        /// <paramref name="item"/>'s FullName if not relative.
        /// <br/>See also: <seealso cref="Path.GetRelativePath(string, string)"/>
        /// </summary>
        public static string GetRelativePath(this DirectoryInfo item, DirectoryInfo relativeTo) =>
            Path.GetRelativePath(relativeTo.FullName, item.FullName);

        /// <inheritdoc cref="GetRelativePath(DirectoryInfo,DirectoryInfo)"/>
        public static string GetRelativePath(this IDirectoryInfo item, IDirectoryInfo relativeTo) =>
            item.FileSystem.Path.GetRelativePath(relativeTo.FullName, item.FullName);

        /// <summary>
        /// Return absolute path for a root-relative path;
        /// using the specified root directory.
        /// </summary>
        /// <remarks>
        /// The inbuilt <see cref="FileSystemInfo.FullName"/> will
        /// resolve using <see cref="Directories.Current()"/>
        /// </remarks>
        public static FileInfo GetFullPath(this FileInfo file, DirectoryInfo root) =>
            new FileInfo(Path.GetFullPath(file.ToString(), root.ToString()));
    }
}
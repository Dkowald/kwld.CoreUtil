using System;
using System.IO;
using System.IO.Abstractions;
using System.Linq;

namespace kwld.CoreUtil.FileSystem
{
    /// <summary>
    /// Set of useful helpers for file system.
    /// </summary>
    public static class HelperExtensions
    {
        /// <summary>
        /// Get child file info.
        /// </summary>
        public static FileInfo GetFile(this DirectoryInfo dir, params string[] subPath)
        {
            if (subPath.Any() != true)
            {
                throw new ArgumentException("Sub path cannot be empty", nameof(subPath));
            }

            var path = Path.Combine(dir.FullName, Path.Combine(subPath));

            return new FileInfo(path);
        }

        /// <inheritdoc cref="GetFile(DirectoryInfo,string[])"/>
        public static IFileInfo GetFile(this IDirectoryInfo dir, params string[] subPath)
        {
            if(subPath.Length < 1)
                throw new ArgumentException("Sub path cannot be empty", nameof(subPath));
            
            var path = dir.FileSystem.Path.Combine(dir.FullName, Path.Combine(subPath));
            return dir.FileSystem.FileInfo.New(path);
        }

        /// <summary>
        /// Get child folder.
        /// </summary>
        public static DirectoryInfo GetFolder(this DirectoryInfo dir, params string[] subPath)
        {
            if (!subPath.Any()) { return dir; }

            var path = Path.Combine(dir.FullName, Path.Combine(subPath));

            return new DirectoryInfo(path);
        }

        /// <inheritdoc cref="GetFolder(DirectoryInfo,string[])"/>
        public static IDirectoryInfo GetFolder(this IDirectoryInfo dir, params string[] subPath)
        {
            if (!subPath.Any()) { return dir; }

            var path = dir.FileSystem.Path.Combine(dir.FullName, dir.FileSystem.Path.Combine(subPath));

            return dir.FileSystem.DirectoryInfo.New(path);
        }

        /// <summary>
        /// Set the file LastWrite to current time; creating the file if needed.
        /// </summary>
        public static FileInfo Touch(this FileInfo file, Func<DateTime>? clock = null)
        {
            file.Refresh();

            if (!file.Exists)
            {
                file.Directory?.Create();
                file.Create().Dispose();
            }

            if (clock != null)
            { file.LastWriteTimeUtc = clock().ToUniversalTime(); }

            return file;
        }

        /// <inheritdoc cref="Touch(FileInfo,Func{DateTime}?)"/>
        public static IFileInfo Touch(this IFileInfo file, Func<DateTime>? clock = null)
        {
            file.Refresh();

            if (!file.Exists)
            {
                file.Directory?.Create();
                file.Create().Dispose();
            }

            if (clock != null)
            { file.LastWriteTimeUtc = clock().ToUniversalTime(); }

            return file;
        }

        /// <summary>
        /// Sets the directory LastWrite to current time; creating if needed.
        /// </summary>
        public static DirectoryInfo Touch(this DirectoryInfo dir, Func<DateTime>? clock = null)
        {
            dir.Refresh();

            if (!dir.Exists) { dir.Create(); }

            if (clock != null)
            { dir.LastWriteTimeUtc = clock().ToUniversalTime(); }

            return dir;
        }

        /// <inheritdoc cref="Touch(DirectoryInfo,Func{DateTime}?)"/>
        public static IDirectoryInfo Touch(this IDirectoryInfo dir, Func<DateTime>? clock = null)
        {
            dir.Refresh();

            if (!dir.Exists) { dir.Create(); }

            if (clock != null)
            { dir.LastWriteTimeUtc = clock().ToUniversalTime(); }

            return dir;
        }

        /// <summary>
        /// Checks the file-system to determine if item exists.
        /// </summary>
        public static bool Exists(this FileSystemInfo? item)
        {
            if (item is null) return false;

            item.Refresh();
            return item.Exists;
        }

        /// <inheritdoc cref="Exists(FileSystemInfo?)"/>
        public static bool Exists(this IFileSystemInfo? item)
        {
            if (item is null) return false;

            item.Refresh();
            return item.Exists;
        }

        /// <summary>
        /// Convert the file system object to a corresponding file:// uri.
        /// </summary>
        public static Uri AsUri(this FileSystemInfo item) => 
            item is DirectoryInfo dir ? new Uri(
                    dir.FullName + 
                    (dir.FullName.EndsWith('/')? string.Empty :"/")) : 
                new Uri(item.FullName);

        /// <inheritdoc cref="AsUri(FileSystemInfo)"/>
        public static Uri AsUri(this IFileSystemInfo item) =>
            item is IDirectoryInfo dir ? new Uri(
                    dir.FullName +
                    (dir.FullName.EndsWith('/') ? string.Empty : "/")) :
                new Uri(item.FullName);

        /// <summary>
        /// Set the current item as current directory.
        /// If <paramref name="item"/> is a file, uses its containing directory.
        /// </summary>
        public static PushD PushD(this IFileSystemInfo item)
        {
            if (item is IDirectoryInfo dir)
                return new PushD(dir);

            if (item is IFileInfo file)
            {
                dir = file.Directory ??
                      throw new Exception("File item has no containing directory");
                return new PushD(dir);
            }

            throw new Exception("FileSystem item is neither a directory or file");
        }

        public static PushD PushD(this FileSystemInfo item)
        {
            if (item is DirectoryInfo dir)
                return new PushD(dir);

            if (item is FileInfo file)
            {
                dir = file.Directory ??
                      throw new Exception("File item has no containing directory");
                return new PushD(dir);
            }

            throw new Exception("FileSystem item is neither a directory or file");
        }
    }
}
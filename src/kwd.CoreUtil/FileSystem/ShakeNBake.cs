using System;
using System.IO;
using System.Linq;

namespace kwd.CoreUtil.FileSystem
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
            if (dir == null) {throw new ArgumentNullException(nameof(dir));}

            if (subPath?.Any() != true) { throw new ArgumentNullException(nameof(subPath));}

            var path = Path.Combine(dir.FullName, Path.Combine(subPath));

            return new FileInfo(path);
        }

        /// <summary>
        /// Get child folder.
        /// </summary>
        public static DirectoryInfo GetFolder(this DirectoryInfo dir, params string[] subPath)
        {
            if (dir == null) {throw new ArgumentNullException(nameof(dir));}

            if (subPath?.Any() != true) { return dir; }

            var path = Path.Combine(dir.FullName, Path.Combine(subPath));

            return new DirectoryInfo(path);
        }

        /// <summary>
        /// Set the file LastWrite to current time; creating the file if needed.
        /// </summary>
        public static FileInfo Touch(this FileInfo file, Func<DateTime>? clock = null)
        {
            if (file == null) throw new ArgumentNullException(nameof(file));

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
            if (dir == null) throw new ArgumentNullException(nameof(dir));
            
            dir.Refresh();

            if (!dir.Exists) { dir.Create(); }

            if (clock != null)
            { dir.LastWriteTimeUtc = clock().ToUniversalTime(); }

            return dir;
        }
        
        /// <summary>
        /// Checks the file-system to determine if item exists.
        /// </summary>
        public static bool Exists(this FileSystemInfo item)
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
                    (dir.FullName.EndsWith("/")? string.Empty :"/")) : 
                new Uri(item.FullName);
    }
}
using System;
using System.IO;

namespace kwd.CoreUtil.FileSystem
{
    /// <summary>
    /// Extension for <see cref="FileInfo"/>
    /// </summary>
    public static class FileSystemInfoExtensions
    {
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
        /// Removes the directory and all its content (files and directories).
        /// </summary>
        public static DirectoryInfo EnsureDelete(this DirectoryInfo dir)
        {
            if (dir == null) throw new ArgumentNullException(nameof(dir));

            dir.Refresh();
            if (dir.Exists) { dir.Delete(true);}

            return dir;
        }

        /// <summary>
        /// Remove a file.
        /// </summary>
        public static FileInfo EnsureDelete(this FileInfo file)
        {
            if(file == null) throw new ArgumentNullException(nameof(file));

            file.Refresh();

            if (file.Exists) { file.Delete(); file.Refresh();}

            return file;
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
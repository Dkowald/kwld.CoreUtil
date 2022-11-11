using System.IO;
using System.IO.Abstractions;

namespace kwd.CoreUtil.FileSystem
{
    /// <summary>
    /// Set of extensions for <see cref="FileInfo"/>
    /// </summary>
    public static class ChainExtensions
    {
        /// <summary>
        /// Create file if it doesn't exist.
        /// </summary>
        public static FileInfo EnsureExists(this FileInfo file)
        {
            file.Refresh();

            if (!file.Exists)
            {
                file.Directory?.Create();
                file.Create().Dispose();
                file.Refresh();
            }

            return file;
        }

        /// <inheritdoc cref="EnsureExists(FileInfo)"/>
        public static IFileInfo EnsureExists(this IFileInfo file)
        {
            file.Refresh();

            if (!file.Exists)
            {
                file.Directory?.Create();
                file.Create().Dispose();
                file.Refresh();
            }

            return file;
        }

        /// <summary>
        /// Create Directory if not exist.
        /// </summary>
        public static DirectoryInfo EnsureExists(this DirectoryInfo dir)
        {
            dir.Create();
            dir.Refresh();
            return dir;
        }

        /// <inheritdoc cref="EnsureExists(DirectoryInfo)"/>
        public static IDirectoryInfo EnsureExists(this IDirectoryInfo dir)
        {
            dir.Create();
            dir.Refresh();
            return dir;
        }

        /// <summary>
        /// Removes the directory and all its content (files and directories).
        /// </summary>
        public static DirectoryInfo EnsureDelete(this DirectoryInfo dir)
        {
            dir.Refresh();
            if (dir.Exists)
            {
                dir.Delete(true);
                dir.Refresh();
            }

            return dir;
        }

        /// <inheritdoc cref="EnsureDelete(DirectoryInfo)"/>
        public static IDirectoryInfo EnsureDelete(this IDirectoryInfo dir)
        {
            dir.Refresh();
            if (dir.Exists)
            {
                dir.Delete(true);
                dir.Refresh();
            }

            return dir;
        }

        /// <summary>
        /// Remove a file.
        /// </summary>
        public static FileInfo EnsureDelete(this FileInfo file)
        {
            file.Refresh();

            if (file.Exists)
            {
                file.Delete(); 
                file.Refresh();
            }

            return file;
        }

        /// <inheritdoc cref="EnsureDelete(FileInfo)"/>
        public static IFileInfo EnsureDelete(this IFileInfo file)
        {
            file.Refresh();

            if (file.Exists)
            {
                file.Delete();
                file.Refresh();
            }

            return file;
        }

        /// <inheritdoc cref="EnsureEmpty(IDirectoryInfo)"/>
        public static DirectoryInfo EnsureEmpty(this DirectoryInfo dir)
        {
            dir.EnsureDelete();
            dir.EnsureExists();
            return dir;
        }

        /// <summary>
        /// Ensure the directory exists, and is empty.
        /// </summary>
        public static IDirectoryInfo EnsureEmpty(this IDirectoryInfo dir)
        {
            dir.EnsureDelete();
            dir.EnsureExists();
            return dir;
        }

        /// <summary>
        /// Ensure the file exists and is empty
        /// </summary>
        public static FileInfo EnsureEmpty(this FileInfo dir)
        {
            dir.EnsureDelete();
            dir.EnsureExists();
            return dir;
        }

        /// <inheritdoc cref="EnsureEmpty(FileInfo)"/>
        public static IFileInfo EnsureEmpty(this IFileInfo dir)
        {
            dir.EnsureDelete();
            dir.EnsureExists();
            return dir;
        }
    }
}
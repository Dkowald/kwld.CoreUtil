using System.IO;

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

        /// <summary>
        /// Create Directory if not exist.
        /// </summary>
        public static DirectoryInfo EnsureExists(this DirectoryInfo dir)
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
    }
}
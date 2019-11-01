using System;
using System.IO;
using System.Linq;

namespace kwd.CoreUtil.FileSystem
{
    /// <summary>
    /// Extensions for <see cref="FileInfo"/> and <see cref="DirectoryInfo"/>
    /// </summary>
    public static class FileDirectoryExtensions
    {
        /// <summary>
        /// Move existing file to specified directory, optionally overwriting target if it exists.
        /// </summary>
        public static FileInfo MoveTo(this FileInfo file, DirectoryInfo dir, bool overwrite = false)
        {
            if (file == null) throw new ArgumentNullException(nameof(file));
            if (dir == null) throw new ArgumentNullException(nameof(dir));

            var result = new FileInfo(Path.Combine(dir.FullName, file.Name));

            if (result.Exists && overwrite) { result.Delete(); }

            File.Move(file.FullName, result.FullName);

            return result;
        }

        /// <summary>
        /// Copy existing file to specific directory, optionally overwriting target if it exists.
        /// </summary>
        public static FileInfo CopyTo(this FileInfo file, DirectoryInfo dir, bool overwrite = false)
        {
            if (file == null) throw new ArgumentNullException(nameof(file));
            if (dir == null) throw new ArgumentNullException(nameof(dir));

            var result = new FileInfo(Path.Combine(dir.FullName, file.Name));
         
            //Create folder if need.
            file.Refresh();
            if(file.Exists) { result.Directory?.Create(); }

            File.Copy(file.FullName, result.FullName, overwrite);

            return result;
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
        /// <param name="file"></param>
        /// <returns></returns>
        public static FileInfo EnsureDelete(this FileInfo file)
        {
            if(file == null) throw new ArgumentNullException(nameof(file));

            file.Refresh();

            if (file.Exists) { file.Delete(); file.Refresh();}

            return file;
        }

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
        /// Delete child folders that contain no files.
        /// </summary>
        public static DirectoryInfo Prune(this DirectoryInfo dir)
        {
            if (dir == null) throw new ArgumentNullException();

            void RecursivePrune(DirectoryInfo subDir)
            {
                subDir.Refresh();
                if (!subDir.Exists) { return; }

                foreach (var subSubDir in subDir.GetDirectories())
                {
                    RecursivePrune(subSubDir);
                }

                if (!subDir.GetFileSystemInfos().Any()) { subDir.Delete();}
            }

            RecursivePrune(dir);

            return dir;
        }
    }
}
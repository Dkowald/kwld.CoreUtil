using System;
using System.IO;

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
    }
}
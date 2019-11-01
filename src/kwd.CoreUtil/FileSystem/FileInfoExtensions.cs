using System.IO;

namespace kwd.CoreUtil.FileSystem
{
    public static class FileInfoExtensions
    {
        /// <summary>
        /// Create file if it doesn't exist.
        /// </summary>
        public static FileInfo EnsureCreate(this FileInfo file)
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
        /// Copy <paramref name="file"/> to <paramref name="targetFile"/> with optional <paramref name="overwrite"/>. <br />
        /// See <see cref="FileInfo.CopyTo(string)"/>
        /// </summary>
        public static FileInfo CopyTo(this FileInfo file, FileInfo targetFile, bool overwrite = false)
            => file.CopyTo(targetFile.FullName, overwrite);
    }
}
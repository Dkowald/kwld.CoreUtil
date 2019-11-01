using System;
using System.IO;
using System.Linq;

namespace kwd.CoreUtil.FileSystem
{
    public static class DirectoryInfoExtensions
    {
        /// <summary>
        /// Create Directory if not exist.
        /// </summary>
        public static DirectoryInfo EnsureCreate(this DirectoryInfo dir)
        {
            dir.Create();
            dir.Refresh();
            return dir;
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

        /// <summary>
        /// Copy <paramref name="srcDir"/> into <paramref name="dir"/>.
        /// </summary>
        public static DirectoryInfo CopyTo(this DirectoryInfo srcDir, DirectoryInfo outDir, bool overwrite = true)
        {
            foreach (var item in srcDir.EnumerateFiles("*", SearchOption.AllDirectories))
            {
                var target = outDir.GetFile(item.GetRelativePath(srcDir));

                item.CopyTo(target, overwrite);
            }

            return outDir;
        }
    }
}
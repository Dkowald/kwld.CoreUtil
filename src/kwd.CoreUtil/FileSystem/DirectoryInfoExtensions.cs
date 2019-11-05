using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace kwd.CoreUtil.FileSystem
{
    /// <summary>
    /// Set of extensions for <see cref="DirectoryInfo"/>.
    /// </summary>
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
        /// Copy <paramref name="srcDir"/> into <paramref name="outDir"/>.
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

        /// <summary>
        /// Return the set of files in <paramref name="targetDir"/>
        /// that have corresponding file (relative path) in <paramref name="srcDir"/>.
        /// </summary>
        public static IEnumerable<FileInfo> TreeSameFiles(this DirectoryInfo srcDir, DirectoryInfo targetDir)
        {   
            var mappedFiles = srcDir.EnumerateFiles("*", SearchOption.AllDirectories)
                .Select(x => targetDir.GetFile(x.GetRelativePath(srcDir)));

            return mappedFiles.Where(x => x.Exists);
        }

        /// <summary>
        /// Retrieve list of files from <paramref name="targetDir"/>
        /// compared to files (relative path) in <paramref name="srcDir"/>.
        /// </summary>
        public static (IReadOnlyCollection<FileInfo> Created,
            IReadOnlyCollection<FileInfo> Updated,
            IReadOnlyCollection<FileInfo> Deleted ) TreeCUD(this DirectoryInfo srcDir, DirectoryInfo targetDir)
        {
            var mapped = srcDir.EnumerateFiles("*", SearchOption.AllDirectories)
                .Select(x => new {
                    src = x,
                    dest = targetDir.GetFile(x.GetRelativePath(srcDir))
                }).ToArray();

            var other = targetDir.EnumerateFiles("*", SearchOption.AllDirectories);

            var created = other
                .Where(o => !mapped.Any(s => s.dest.FullName == o.FullName))
                .ToArray();

            var updated = mapped.Where(x =>
                x.src.Exists && x.dest.Exists &&
                x.src.LastWriteTimeUtc != x.dest.LastWriteTimeUtc)
                .Select(x => x.dest).ToArray();

            var deleted = mapped.Where(x => x.src.Exists && !x.dest.Exists)
                .Select(x => x.dest).ToArray();

            return (created, updated, deleted);
        }

        /// <summary>
        /// Merges new and (optionally) updated files from
        /// <paramref name="src"/> to <paramref name="target"/>.
        /// Returns <paramref name="target"/>.
        /// </summary>
        public static DirectoryInfo Merge(this DirectoryInfo target, DirectoryInfo src, bool copyUpdated = true)
        {
            var mapped = src.EnumerateFiles("*", SearchOption.AllDirectories)
                .Select(x => new
                {
                    src = x,
                    dest = target.GetFile(x.GetRelativePath(src))
                });
            
            var toCopy = mapped.Where(x => 
                !x.dest.Exists ||
                (copyUpdated &&
                x.src.LastWriteTimeUtc > x.dest.LastWriteTimeUtc) );

            foreach (var item in toCopy)
            {
                item.src.CopyTo(item.dest, true);
            }

            return target;
        }

        /// <summary>
        /// All files in <paramref name="src"/> are copied to <paramref name="target"/>.
        /// Keeping their relative paths.
        /// </summary>
        public static DirectoryInfo MergeForce(this DirectoryInfo target, DirectoryInfo src)
        {
            var mapped = src.EnumerateFiles("*", SearchOption.AllDirectories)
                .Select(x => new
                {
                    src = x,
                    dest = target.GetFile(x.GetRelativePath(src))
                });

            foreach (var item in mapped)
            {
                item.src.CopyTo(item.dest, true);
            }

            return target;
        }
    }
}
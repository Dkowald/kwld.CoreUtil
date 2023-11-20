using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;

namespace kwld.CoreUtil.FileSystem
{
    /// <summary>
    /// Extensions that operate over items in a Directory
    /// </summary>
    public static class TreeExtensions
    {
        /// <summary>
        /// Return the set of files in <paramref name="target"/>
        /// that have corresponding file (relative path) in <paramref name="source"/>.
        /// </summary>
        public static IEnumerable<FileInfo> TreeSameFiles(this DirectoryInfo source, DirectoryInfo target)
        {   
            var mappedFiles = source.EnumerateFiles("*", SearchOption.AllDirectories)
                .Select(x => target.GetFile(x.GetRelativePath(source)));

            return mappedFiles.Where(x => x.Exists);
        }

        /// <inheritdoc cref="TreeSameFiles(DirectoryInfo,DirectoryInfo)"/>
        public static IEnumerable<IFileInfo> TreeSameFiles(this IDirectoryInfo source, IDirectoryInfo target)
        {
            var mappedFiles = source.EnumerateFiles("*", SearchOption.AllDirectories)
                .Select(x => target.GetFile(x.GetRelativePath(source)));

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

        /// <inheritdoc cref="TreeCUD(DirectoryInfo,DirectoryInfo)"/>
        public static (IReadOnlyCollection<IFileInfo> Created,
            IReadOnlyCollection<IFileInfo> Updated,
            IReadOnlyCollection<IFileInfo> Deleted) TreeCUD(this IDirectoryInfo srcDir, IDirectoryInfo targetDir)
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
        /// <paramref name="source"/> to <paramref name="destination"/>.
        /// Returns <paramref name="destination"/>.
        /// </summary>
        public static DirectoryInfo Merge(this DirectoryInfo source, DirectoryInfo destination, bool copyUpdated = true)
        {
            var mapped = source.EnumerateFiles("*", SearchOption.AllDirectories)
                .Select(x => new
                {
                    src = x,
                    dest = destination.GetFile(x.GetRelativePath(source))
                });
            
            var toCopy = mapped.Where(x => 
                !x.dest.Exists ||
                (copyUpdated &&
                 x.src.LastWriteTimeUtc > x.dest.LastWriteTimeUtc) );

            foreach (var item in toCopy)
            {
                item.src.CopyTo(item.dest, true);
            }

            return destination;
        }

        /// <inheritdoc cref="Merge(DirectoryInfo,DirectoryInfo,bool)"/>
        public static IDirectoryInfo Merge(this IDirectoryInfo source, IDirectoryInfo destination, bool copyUpdated = true)
        {
            var mapped = source.EnumerateFiles("*", SearchOption.AllDirectories)
                .Select(x => new
                {
                    src = x,
                    dest = destination.GetFile(x.GetRelativePath(source))
                });

            var toCopy = mapped.Where(x =>
                !x.dest.Exists ||
                (copyUpdated &&
                 x.src.LastWriteTimeUtc > x.dest.LastWriteTimeUtc));

            foreach (var item in toCopy)
            {
                item.src.CopyTo(item.dest, true);
            }

            return destination;
        }

        /// <summary>
        /// All files in <paramref name="source"/> are copied to <paramref name="destination"/>.
        /// Keeping their relative paths.
        /// </summary>
        public static DirectoryInfo MergeForce(this DirectoryInfo source, DirectoryInfo destination)
        {
            var mapped = source.EnumerateFiles("*", SearchOption.AllDirectories)
                .Select(x => new
                {
                    src = x,
                    dest = destination.GetFile(x.GetRelativePath(source))
                });

            foreach (var item in mapped)
            {
                item.src.CopyTo(item.dest, true);
            }

            return destination;
        }

        /// <inheritdoc cref="MergeForce(DirectoryInfo,DirectoryInfo)"/>
        public static IDirectoryInfo MergeForce(this IDirectoryInfo source, IDirectoryInfo destination)
        {
            var mapped = source.EnumerateFiles("*", SearchOption.AllDirectories)
                .Select(x => new
                {
                    src = x,
                    dest = destination.GetFile(x.GetRelativePath(source))
                });

            foreach (var item in mapped)
            {
                item.src.CopyTo(item.dest, true);
            }

            return destination;
        }

        /// <summary>
        /// Delete child folders that contain no files.
        /// </summary>
        public static DirectoryInfo Prune(this DirectoryInfo dir)
        {
            static void RecursivePrune(DirectoryInfo subDir)
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

        /// <inheritdoc cref="Prune(DirectoryInfo)"/>
        public static IDirectoryInfo Prune(this IDirectoryInfo dir)
        {
            static void RecursivePrune(IDirectoryInfo subDir)
            {
                subDir.Refresh();
                if (!subDir.Exists) { return; }

                foreach (var subSubDir in subDir.GetDirectories())
                {
                    RecursivePrune(subSubDir);
                }

                if (!subDir.GetFileSystemInfos().Any()) { subDir.Delete(); }
            }

            RecursivePrune(dir);

            return dir;
        }
    }
}
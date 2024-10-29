using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;

namespace kwld.CoreUtil.FileSystem
{
    /// <summary>
    /// Extensions to expand and normalize paths.
    /// </summary>
    public static class ResolvePathExtensions
    {
        /// <summary>
        /// Test if <see cref="DirectoryInfo"/> is case-sensitive.
        /// </summary>
        /// <remarks>
        /// This is NOT a bullet-proof test;
        /// though it should work fine in most real-world situations.
        /// It mangles the case for the given folder name; and
        /// returns true if not found.
        /// </remarks>
        /// <exception cref="ArgumentException">
        /// Raised if provided <paramref name="fileSystemItem"/> doesn't exist,
        /// or doesn't contain a letter.
        /// </exception>
        public static bool IsCaseSensitive(this FileSystemInfo fileSystemItem)
        {
            var fullPath = fileSystemItem.FullName;

            fileSystemItem.Refresh();
            if (!fileSystemItem.Exists || !fullPath.Any(char.IsLetter))
            {
                throw new ArgumentException(
                    "Test item must exist, and have a letter in the name", 
                    nameof(fileSystemItem));
            }

            var altPath = CaseDifferingPath(fullPath);

            if(fileSystemItem is FileInfo)
                return !File.Exists(altPath);
            if(fileSystemItem is DirectoryInfo)
                return !Directory.Exists(altPath);

            throw new ArgumentException(
                $"File System item must be either {nameof(FileInfo)} or {nameof(DirectoryInfo)}",
                nameof(fileSystemItem));
        }

        /// <inheritdoc cref="IsCaseSensitive(FileSystemInfo)"/>
        public static bool IsCaseSensitive(this IFileSystemInfo fileSystemItem)
        {
            var fullPath = fileSystemItem.FullName;

            fileSystemItem.Refresh();
            if (!fileSystemItem.Exists || !fullPath.Any(char.IsLetter))
            {
                throw new ArgumentException(
                    "Test item must exist, and have a letter in the name", 
                    nameof(fileSystemItem));
            }

            var altPath = CaseDifferingPath(fullPath);

            var fileSystem = fileSystemItem.FileSystem;
            if (fileSystemItem is IFileInfo)
                return !fileSystem.File.Exists(altPath);

            if(fileSystemItem is IDirectoryInfo)
                return !fileSystem.Directory.Exists(altPath);

            throw new ArgumentException(
                $"File System item must be either {nameof(IFileInfo)} or {nameof(IDirectoryInfo)}",
                nameof(fileSystemItem));
        }

        /// <inheritdoc cref="IsCaseSensitive(FileSystemInfo)"/>
        public static bool IsCaseSensitive(this IFileSystem files) =>
            IsCaseSensitive(files.Current());

        /// <summary>
        /// Splits the given string into a set of path segments.
        /// </summary>
        public static string[] PathSplit(string path)
            => path.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

        /// <summary>
        /// Path to a directory, sub path matches file system case.
        /// using first sub-path match for case sensitive file systems.
        /// FullName always ends with a trailing <see cref="Path.DirectorySeparatorChar"/>
        /// </summary>
        /// <param name="root">Start point, is Not mapped to match-case.</param>
        /// <param name="path">path segments, attempt to match each segment to existing items case</param>
        public static DirectoryInfo FindFolder(this DirectoryInfo root, params string[] path)
        {
            if (path.Length == 0) { return root; }

            var cur = root.FullName;

            var segments = new Queue<string>(path.SelectMany(PathSplit));

            while (segments.Count > 0)
            {
                if (!Directory.Exists(cur)) { break; }

                var part = segments.Dequeue();

                var subPath = Directory.EnumerateDirectories(cur)
                    .FirstOrDefault(x => Path.GetFileName(x).Equals(part, StringComparison.OrdinalIgnoreCase));

                //get path that matches, or just next.
                cur = Path.Combine(cur, subPath ?? part);
            }

            //current doesn't exist, just use the rest as-is
            cur = Path.GetFullPath(
                Path.Combine(new[] { cur }.Union(segments).ToArray()));

            //normalize a directory to end with trailing /
            if (!cur.EndsWith(Path.DirectorySeparatorChar))
            { cur += Path.DirectorySeparatorChar; }

            return new DirectoryInfo(cur);
        }

        /// <inheritdoc cref="FindFolder(DirectoryInfo,string[])"/>
        public static IDirectoryInfo FindFolder(this IDirectoryInfo root, params string[] path)
        {
            if (path.Length == 0) { return root; }

            var cur = root.FullName;

            var segments = new Queue<string>(path.SelectMany(PathSplit));

            while (segments.Count > 0)
            {
                if (!Directory.Exists(cur)) { break; }

                var part = segments.Dequeue();

                var subPath = Directory.EnumerateDirectories(cur)
                    .FirstOrDefault(x => root.FileSystem.Path.GetFileName(x).Equals(part, StringComparison.OrdinalIgnoreCase));

                //get path that matches, or just next.
                cur = root.FileSystem.Path.Combine(cur, subPath ?? part);
            }

            //current doesn't exist, just use the rest as-is
            cur = root.FileSystem.Path.GetFullPath(
                root.FileSystem.Path.Combine(new[] { cur }.Union(segments).ToArray()));

            //normalize a directory to end with trailing /
            if (!cur.EndsWith(root.FileSystem.Path.DirectorySeparatorChar))
            { cur += root.FileSystem.Path.DirectorySeparatorChar; }

            return root.FileSystem.DirectoryInfo.New(cur);
        }

        /// <summary>
        /// Locate file, matching the file-system case as best as possible.
        /// Last entry in <paramref name="subPathAndFilename"/> is the file name.
        /// <seealso cref="FindFolder(DirectoryInfo, string[])"/>.
        /// </summary>
        public static FileInfo FindFile(this DirectoryInfo root, params string[] subPathAndFilename)
        {
            var subPaths = subPathAndFilename.SelectMany(PathSplit).ToArray();

            if (subPaths.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(subPathAndFilename), "Must have at-least file name");

            var dir = root.FindFolder(subPaths.SkipLast(1).ToArray());
            var fileName = subPaths.Last();

            var path = Directory.Exists(dir.FullName)
                ? Directory.EnumerateFiles(dir.FullName)
                    .FirstOrDefault(x => Path.GetFileName(x)
                        .Equals(fileName, StringComparison.OrdinalIgnoreCase))
                : null;

            path ??= Path.Combine(dir.FullName, fileName);

            return new FileInfo(path);
        }

        /// <inheritdoc cref="FindFile(DirectoryInfo,string[])"/>
        public static IFileInfo FindFile(this IDirectoryInfo root, params string[] subPathAndFilename)
        {
            var subPaths = subPathAndFilename.SelectMany(PathSplit).ToArray();

            if (subPaths.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(subPathAndFilename), "Must have at-least file name");

            var dir = root.FindFolder(subPaths.SkipLast(1).ToArray());
            var fileName = subPaths.Last();

            var path = Directory.Exists(dir.FullName)
                ? Directory.EnumerateFiles(dir.FullName)
                    .FirstOrDefault(x => root.FileSystem.Path.GetFileName(x)
                        .Equals(fileName, StringComparison.OrdinalIgnoreCase))
                : null;

            path ??= root.FileSystem.Path.Combine(dir.FullName, fileName);

            return root.FileSystem.FileInfo.New(path);
        }

        /// <summary>
        /// Expand a given path; resolving environment variables;
        /// and relative path.
        /// </summary>
        /// <param name="item">The target file system item</param>
        /// <param name="root">The root folder to expand relative paths with</param>
        /// <param name="replaceEnvironment">True to expand all %ENVIRONMENT_VARIABLE%s</param>
        public static IFileInfo Expand(this IFileInfo item, IDirectoryInfo root,
            bool replaceEnvironment = true)
        {
            var name = item.ToString() ?? string.Empty;

            var itemPath = replaceEnvironment ?
                Environment.ExpandEnvironmentVariables(name) :
                name;

            var expandedPath = Path.GetFullPath(Path.Combine(root.FullName, itemPath));
            return item.FileSystem.FileInfo.New(expandedPath);
        }

        /// <summary>
        /// Expand a given path; resolving environment variables;
        /// and relative path. Uses current directory as root path
        /// </summary>
        /// <param name="item">The target file system item</param>
        /// <param name="replaceEnvironment">True to expand all %ENVIRONMENT_VARIABLE%s</param>
        public static IFileInfo Expand(this IFileInfo item, bool replaceEnvironment = true)
            => Expand(item, item.FileSystem.Current(), replaceEnvironment);

        /// <inheritdoc cref="Expand(IFileInfo,IDirectoryInfo,bool)"/>
        public static FileInfo Expand(this FileInfo item, DirectoryInfo root,
            bool replaceEnvironment = true)
        {
            var itemPath = replaceEnvironment ?
                Environment.ExpandEnvironmentVariables(item.ToString()) :
                item.ToString();

            var expandedPath = Path.GetFullPath(Path.Combine(root.FullName, itemPath));
            return new FileInfo(expandedPath);
        }

        /// <inheritdoc cref="Expand(IFileInfo,bool)"/>
        public static FileInfo Expand(this FileInfo item, bool replaceEnvironment = true)
            => Expand(item, new DirectoryInfo(Directory.GetCurrentDirectory()), replaceEnvironment);

        /// <inheritdoc cref="Expand(IFileInfo,IDirectoryInfo,bool)"/>
        public static IDirectoryInfo Expand(this IDirectoryInfo item, IDirectoryInfo root,
            bool replaceEnvironment = true)
        {
            var name = item.ToString() ?? string.Empty;

            var itemPath = replaceEnvironment ?
                Environment.ExpandEnvironmentVariables(name) : name;

            var expandedPath = item.FileSystem.Path.GetFullPath(
                item.FileSystem.Path.Combine(root.FullName, itemPath));
            return item.FileSystem.DirectoryInfo.New(expandedPath);
        }

        /// <inheritdoc cref="Expand(IFileInfo,bool)"/>
        public static IDirectoryInfo Expand(this IDirectoryInfo item, bool replaceEnvironment = true)
            => Expand(item, item.FileSystem.Current(), replaceEnvironment);

        /// <inheritdoc cref="Expand(IFileInfo,IDirectoryInfo,bool)"/>
        public static DirectoryInfo Expand(this DirectoryInfo item, DirectoryInfo root,
            bool replaceEnvironment = true)
        {
            var itemPath = replaceEnvironment ?
                Environment.ExpandEnvironmentVariables(item.ToString()) :
                item.ToString();

            var expandedPath = Path.GetFullPath(Path.Combine(root.FullName, itemPath));
            return new DirectoryInfo(expandedPath);
        }

        /// <inheritdoc cref="Expand(IFileInfo,bool)"/>
        public static DirectoryInfo Expand(this DirectoryInfo item, bool replaceEnvironment = true)
            => Expand(item, new DirectoryInfo(Directory.GetCurrentDirectory()), replaceEnvironment);

        internal static string CaseDifferingPath(string path)
        {
            var result = path.Any(char.IsUpper) ? path.ToLower() : path.ToUpper();

            return result;
        }
    }
}
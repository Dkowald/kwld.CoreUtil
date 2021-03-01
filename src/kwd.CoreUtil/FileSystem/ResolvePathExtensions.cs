using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using kwd.CoreUtil.Strings;

namespace kwd.CoreUtil.FileSystem
{
    /// <summary>
    /// Extensions to expand and normalize paths.
    /// </summary>
    public static class ResolvePathExtensions
    {
        private static readonly EnumerationOptions CaseIgnorantOptions = new EnumerationOptions
        {
            MatchCasing = MatchCasing.CaseInsensitive,
            AttributesToSkip = 0,
        };

        /// <summary>
        /// Test if <see cref="DirectoryInfo"/> is case-sensitive.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Raised if provided <paramref name="dir"/> doesn't exist,
        /// or doesn't contain a letter.
        /// </exception>
        public static bool IsCaseSensitive(this DirectoryInfo dir)
        {
            var fullPath = dir.FullName;

            dir.Refresh();
            if (!dir.Exists || fullPath.Any(char.IsLetter))
            {
                throw new ArgumentException("Test directory must exist, and have a letter in the name", nameof(dir));
            }

            var altPath = fullPath.Any(char.IsUpper) ? fullPath.ToLower() : fullPath.ToUpper();

            var result = !Directory.Exists(altPath);

            return result;
        }

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

        /// <summary>
        /// Locate file, matching the file-system case as best as possible.
        /// Last entry in <paramref name="subPathAndFilename"/> is the file name.
        /// <seealso cref="FindFolder(DirectoryInfo, string[])"/>.
        /// </summary>
        public static FileInfo FindFile(this DirectoryInfo root, params string[] subPathAndFilename)
        {
            var subPaths = subPathAndFilename.SelectMany(PathSplit).ToArray();

            if(subPaths.Length == 0)
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
    }
}
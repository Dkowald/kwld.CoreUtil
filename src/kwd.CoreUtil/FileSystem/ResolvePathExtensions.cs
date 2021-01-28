using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
        /// Path to a directory, sub path matches file system case.
        /// using first sub-path match for case sensitive file systems.
        /// FullName always ends with a trailing <see cref="Path.DirectorySeparatorChar"/>
        /// </summary>
        /// <param name="root">Start point, is Not mapped to match-case.</param>
        /// <param name="path">path segments, attempt to match each segment to existing items case</param>
        public static DirectoryInfo CaseMatchDir(this DirectoryInfo root, string path)
            => root.CaseMatchDir(path.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));

        /// <summary>
        /// Path to a directory, sub path matches file system case.
        /// using first sub-path match for case sensitive file systems.
        /// FullName always ends with a trailing <see cref="Path.DirectorySeparatorChar"/>
        /// </summary>
        /// <param name="root">Start point, is Not mapped to match-case.</param>
        /// <param name="path">path segments, attempt to match each segment to existing items case</param>
        public static DirectoryInfo CaseMatchDir(this DirectoryInfo root, params string[] path)
        {
            if (path.Length == 0) { return root; }

            var cur = root.FullName;

            var segments = new Queue<string>(path);

            while (segments.Count > 0)
            {
                if (!Directory.Exists(cur)) { break; }

                var part = segments.Dequeue();
                
                //assume next is simple combine.
                var next = Path.GetFullPath(Path.Combine(cur, part));

                //get path that matches, or just next.
                cur = Directory.GetDirectories(cur, part, CaseIgnorantOptions)
                        .FirstOrDefault() ?? next;
            }

            //current doesn't exist, just use the rest as-is
            if(segments.Any())
            { cur = Path.GetFullPath(
                Path.Combine(new[] { cur }.Union(segments).ToArray())); }

            if (!cur.EndsWith(Path.DirectorySeparatorChar))
            { cur += Path.DirectorySeparatorChar; }

            return new DirectoryInfo(cur);
        }

        /// <summary>
        /// Locate file, matching the file-system case as best as possible.
        /// Last entry in <paramref name="subPathAndFilename"/> is the file name.
        /// <seealso cref="CaseMatchDir(DirectoryInfo,string)"/>.
        /// </summary>
        public static FileInfo CaseMatchFile(this DirectoryInfo root, params string[] subPathAndFilename)
        {
            if (subPathAndFilename.Length == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(subPathAndFilename), "Must have at-least file name");
            }
            var dir = root.CaseMatchDir(subPathAndFilename.SkipLast(1).ToArray());
            var fileName = subPathAndFilename.Last();

            var path = Directory.Exists(dir.FullName)
                ? dir.GetFiles(fileName, CaseIgnorantOptions).FirstOrDefault()?.FullName
                : null;
            
            path ??= Path.Combine(dir.FullName, fileName);

            return new FileInfo(path);
        }

        /// <summary>
        /// Resolve a full path with given root.
        /// Expands environment variables.
        /// Expands relative paths.
        /// Uses <see cref="CaseMatchDir(DirectoryInfo,string)"/> to match result to file system.
        /// </summary>
        public static DirectoryInfo ResolveDir(this DirectoryInfo root, params string[] path)
        {
            var segments = path.Select(Environment.ExpandEnvironmentVariables)
                .ToArray();

            return CaseMatchDir(root, segments);
        }

        /// <summary>
        /// Resolve file path. Expanding environment variables,
        /// and matching file system case.
        /// </summary>
        public static FileInfo ResolveFile(this DirectoryInfo root, params string[] pathAndFilename)
        {
            var segments = pathAndFilename.Select(Environment.ExpandEnvironmentVariables)
                .ToArray();

            return CaseMatchFile(root, segments);
        }
    }
}
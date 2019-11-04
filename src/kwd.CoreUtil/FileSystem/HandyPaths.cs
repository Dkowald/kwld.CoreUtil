using System;
using System.IO;

namespace kwd.CoreUtil.FileSystem
{
    /// <summary>
    /// Helpers for commonly used folders.
    /// </summary>
    public static class HandyPaths
    {
        /// <summary>
        /// Use environment to determine current user home folder
        /// for linux or windows.
        /// </summary>
        /// <remarks>
        /// Uses $HOME where possible; else $USERPROFILE.
        /// </remarks>
        public static DirectoryInfo Home()
        {
            var home = Environment.GetEnvironmentVariable("HOME") ??
                Environment.GetEnvironmentVariable("USERPROFILE") ??
                throw new Exception("Cannot determine user home folder");

            return new DirectoryInfo(home);
        }
    }
}
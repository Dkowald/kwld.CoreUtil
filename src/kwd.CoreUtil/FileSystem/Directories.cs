using System;
using System.IO;
using System.Reflection;

namespace kwd.CoreUtil.FileSystem
{
    /// <summary>
    /// Helpers for commonly used folders.
    /// </summary>
    public static class Directories
    {
        /// <summary>
        /// The current directory
        /// </summary>
        public static DirectoryInfo Current()
            =>new DirectoryInfo(Directory.GetCurrentDirectory());
        
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

        /// <summary>
        /// Users temporary folder.
        /// </summary>
        public static DirectoryInfo Temp()
            => new DirectoryInfo(Path.GetTempPath());

        /// <summary>
        /// Get directory for current assembly.
        /// </summary>
        public static DirectoryInfo AssemblyFolder()
            => AssemblyFolder(typeof(Directories));

        /// <summary>
        /// Get directory for assembly containing type <typeparamref name="T"/>.
        /// </summary>
        public static DirectoryInfo AssemblyFolder<T>()
            => AssemblyFolder(typeof(T));

        /// <summary>
        /// Get directory for assembly containing type <paramref name="type"/>.
        /// </summary>
        public static DirectoryInfo AssemblyFolder(Type type)
            => new DirectoryInfo(
                Path.GetDirectoryName(type.Assembly.Location) ?? "");

        /// <summary>
        /// Returns the standard folder when a .net project file would exist for
        /// the calling assembly.
        /// </summary>
        public static DirectoryInfo Project()
        {
            var callerPath =
                Path.GetFullPath(
                Path.Combine(
                    Path.GetDirectoryName(Assembly.GetCallingAssembly().Location) ?? "",
                    "../../../"));

            return new DirectoryInfo(callerPath);
        }
    }
}
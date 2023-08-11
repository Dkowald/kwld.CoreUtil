using System;
using System.IO;
using System.IO.Abstractions;
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

        /// <inheritdoc cref="Current()"/>
        public static IDirectoryInfo Current(this IFileSystem files)
            => files.DirectoryInfo.New(files.Directory.GetCurrentDirectory());

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

        /// <inheritdoc cref="Home()"/>
        public static IDirectoryInfo Home(this IFileSystem files)
        {
            var home = Environment.GetEnvironmentVariable("HOME") ??
                       Environment.GetEnvironmentVariable("USERPROFILE") ??
                       throw new Exception("Cannot determine user home folder");
            return files.DirectoryInfo.New(home);
        }

        /// <summary>
        /// Users temporary folder.
        /// </summary>
        public static DirectoryInfo Temp()
            => new DirectoryInfo(Path.GetTempPath());

        /// <inheritdoc cref="Temp()"/>
        public static IDirectoryInfo Temp(this IFileSystem files)
            => files.DirectoryInfo.New(files.Path.GetTempPath());

        /// <inheritdoc cref="Path.GetTempFileName"/>
        public static FileInfo TempFile() =>
            new FileInfo(Path.GetTempFileName());

        /// <inheritdoc cref="Path.GetTempFileName"/>
        public static IFileInfo TempFile(this IFileSystem files)
            => files.FileInfo.New(files.Path.GetTempFileName());

        /// <summary>
        /// Get directory for assembly containing type <typeparamref name="T"/>.
        /// </summary>
        public static DirectoryInfo AssemblyFolder<T>()
            => AssemblyFolder(typeof(T));

        /// <inheritdoc cref="AssemblyFolder{T}()"/>
        public static IDirectoryInfo AssemblyFolder<T>(this IFileSystem files)
            => AssemblyFolder(files, typeof(T));

        /// <summary>
        /// Get directory for assembly containing type <paramref name="type"/>.
        /// </summary>
        public static DirectoryInfo AssemblyFolder(Type type)
            => new DirectoryInfo(
                Path.GetDirectoryName(type.Assembly.Location) ?? "");

        /// <inheritdoc cref="AssemblyFolder(Type)"/>
        public static IDirectoryInfo AssemblyFolder(this IFileSystem files, Type type)
            => files.DirectoryInfo.New(files.Path.GetDirectoryName(type.Assembly.Location) ??
                                       throw new Exception("Assembly has no Directory"));

        /// <summary>
        /// Returns the standard folder when a .net sdk project file would exist for
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

        /// <inheritdoc cref="Project()"/>
        public static IDirectoryInfo Project(this IFileSystem files)
        {
            var callerPath =
                files.Path.GetFullPath(
                    files.Path.Combine(
                        files.Path.GetDirectoryName(Assembly.GetCallingAssembly().Location) ?? "",
                        "../../../"));
            
            return files.DirectoryInfo.New(callerPath);
        }
  }
}
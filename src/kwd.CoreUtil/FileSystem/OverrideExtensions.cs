using System;
using System.IO;
using System.IO.Abstractions;

namespace kwd.CoreUtil.FileSystem
{
    /// <summary>
    /// Extension for overloads on FileSystemInfo types.
    /// </summary>
    public static class OverrideExtensions
    {
        /// <summary>
        /// Copy <paramref name="source"/> to <paramref name="destination"/>,
        /// with optional <paramref name="overwrite"/>
        /// </summary>
        /// <returns><paramref name="destination"/></returns>
        public static FileInfo CopyTo(this FileInfo source, FileInfo destination, bool overwrite = false)
        {
            destination.Directory?.Create();

            if (overwrite) source.CopyTo(destination.FullName, true);
            else source.CopyTo(destination.FullName);

            return destination;
        }

        /// <inheritdoc cref="CopyTo(FileInfo,FileInfo,bool)"/>
        public static IFileInfo CopyTo(this IFileInfo source, IFileInfo destination, bool overwrite = false)
        {
            destination.Directory?.Create();

            if (overwrite) source.CopyTo(destination.FullName, true);
            else source.CopyTo(destination.FullName);

            return destination;
        }

        /// <summary>
        /// Move <paramref name="source"/> to <paramref name="destination"/>,
        /// with optionally <paramref name="overwrite"/>.
        /// </summary>
        /// <returns>Destination file</returns>
        public static FileInfo MoveTo(this FileInfo source, FileInfo destination, bool overwrite = false)
        {
            destination.Directory?.Create();

            if (overwrite)
            {
                destination.EnsureDelete();
                source.MoveTo(destination.FullName);
            }
            else source.MoveTo(destination.FullName);

            destination.Refresh();
            return destination;
        }

        /// <inheritdoc cref="MoveTo(FileInfo,FileInfo,bool)"/>
        public static IFileInfo MoveTo(this IFileInfo source, IFileInfo destination, bool overwrite = false)
        {
            destination.Directory?.Create();

            if (overwrite)
            {
                destination.EnsureDelete();
                source.MoveTo(destination.FullName);
            }
            else source.MoveTo(destination.FullName);

            destination.Refresh();
            return destination;
        }

        /// <summary>
        /// Move <paramref name="source"/> to <paramref name="destination"/> with
        /// optional <paramref name="destinationBackup"/> backup file and optional
        /// <paramref name="ignoreErrors"/>.
        /// </summary>
        /// <returns><paramref name="destination"/></returns>
        public static FileInfo Replace(this FileInfo source, FileInfo destination, FileInfo? destinationBackup, bool ignoreErrors = true)
        {
            destination.Directory?.Create();

            source.Replace(destination.FullName, destinationBackup?.FullName, ignoreErrors);

            destination.Refresh();
            return destination;
        }
        
        /// <inheritdoc cref="Replace(FileInfo,FileInfo,FileInfo?,bool)"/>
        public static IFileInfo Replace(this IFileInfo source, IFileInfo destination, IFileInfo? destinationBackup, bool ignoreErrors = true)
        {
            destination.Directory?.Create();

            source.Replace(destination.FullName, destinationBackup?.FullName, ignoreErrors);

            destination.Refresh();
            return destination;
        }

        /// <summary>
        /// Move <paramref name="source"/> to <paramref name="destination"/> with
        /// optional <paramref name="ignoreErrors"/>.
        /// </summary>
        /// <returns><paramref name="destination"/></returns>
        public static FileInfo Replace(this FileInfo source, FileInfo destination, bool ignoreErrors = true)
            => source.Replace(destination, null, ignoreErrors);

        /// <inheritdoc cref="Replace(FileInfo,FileInfo,bool)"/>
        public static IFileInfo Replace(this IFileInfo source, IFileInfo destination, bool ignoreErrors = true)
            => source.Replace(destination, null, ignoreErrors);

        /// <summary>
        /// Move directory <paramref name="source"/> to <paramref name="destination"/> directory.
        /// </summary>
        public static DirectoryInfo MoveTo(this DirectoryInfo source, DirectoryInfo destination)
        {
            source.MoveTo(destination.FullName);

            destination.Refresh();
            return destination;
        }

        /// <inheritdoc cref="MoveTo(DirectoryInfo,DirectoryInfo)"/>
        public static IDirectoryInfo MoveTo(this IDirectoryInfo source, IDirectoryInfo destination)
        {
            source.MoveTo(destination.FullName);

            destination.Refresh();
            return destination;
        }

        /// <summary>
        /// Move existing file to specified directory, optionally overwriting target if it exists.
        /// </summary>
        public static FileInfo MoveTo(this FileInfo file, DirectoryInfo dir, bool overwrite = false)
        {
            var result = new FileInfo(Path.Combine(dir.FullName, file.Name));

            if (result.Exists && overwrite) { result.Delete(); }

            result.Directory?.Create();
            
            file.MoveTo(result, overwrite);

            return result;
        }

        /// <inheritdoc cref="MoveTo(FileInfo,DirectoryInfo,bool)"/>
        public static IFileInfo MoveTo(this IFileInfo file, IDirectoryInfo dir, bool overwrite = false)
        {
            var result = file.FileSystem.FileInfo.New(
                file.FileSystem.Path.Combine(dir.FullName, file.Name));

            if (result.Exists && overwrite) { result.Delete(); }

            result.Directory?.Create();

            file.MoveTo(result, overwrite);

            return result;
        }

        /// <summary>
        /// Copy existing file to specific directory, optionally overwriting target if it exists.
        /// </summary>
        public static FileInfo CopyTo(this FileInfo file, DirectoryInfo dir, bool overwrite = false)
        {
            if(!file.Exists())
                throw new ArgumentException("Source files must exist", nameof(file));

            var result = new FileInfo(Path.Combine(dir.FullName, file.Name));
         
            result.Directory?.Create();

            File.Copy(file.FullName, result.FullName, overwrite);

            return result;
        }

        /// <inheritdoc cref="CopyTo(FileInfo,DirectoryInfo,bool)"/>
        public static IFileInfo CopyTo(this IFileInfo file, IDirectoryInfo dir, bool overwrite = false)
        {
            if (!file.Exists())
                throw new ArgumentException("Source files must exist", nameof(file));

            var result = file.FileSystem.FileInfo.New(
                file.FileSystem.Path.Combine(dir.FullName, file.Name));

            result.Directory?.Create();

            file.FileSystem.File.Copy(file.FullName, result.FullName, overwrite);

            return result;
        }
    }
}
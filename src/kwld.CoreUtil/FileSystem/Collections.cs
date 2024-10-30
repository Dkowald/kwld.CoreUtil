using System;
using System.IO;
using System.IO.Abstractions;

namespace kwld.CoreUtil.FileSystem
{
    /// <summary>
    /// File system extensions to work with items as a collection.
    /// </summary>
    public static class Collections
    {
        /// <summary>
        /// Recursive enumerate all items in the directory,
        /// </summary>
        /// <param name="root">Root folder from which to search</param>
        /// <param name="pattern">optional search pattern to limit items by</param>
        /// <param name="forEach">optional action to be applied to every found item</param>
        public static IFileSystemInfo[] All(this IDirectoryInfo root, 
            string pattern = "*", 
            Action<IFileSystemInfo>? forEach = null)
        {
            var items = root.GetFileSystemInfos(pattern, SearchOption.AllDirectories);

            if (forEach != null)
                foreach (var item in items)
                { forEach(item); }
         
            return items;
        }

        /// <inheritdoc cref="All(IDirectoryInfo,string,System.Action{IFileSystemInfo}?)"/>
        public static FileSystemInfo[] All(this DirectoryInfo root, 
            string pattern = "*", 
            Action<FileSystemInfo>? forEach = null)
        {
            var items = root.GetFileSystemInfos(pattern, SearchOption.AllDirectories);

            if (forEach != null)
                foreach (var item in items)
                { forEach(item); }
         
            return items;
        }

        /// <summary>
        /// Recursive enumerate files in a directory
        /// </summary>
        /// <param name="root">Root folder from which to search</param>
        /// <param name="pattern">optional search pattern to limit items by</param>
        /// <param name="forEach">optional action to be applied to every found item</param>
        public static IFileInfo[] AllFiles(this IDirectoryInfo root, string pattern = "*", Action<IFileInfo>? forEach = null)
        {
            var files = root.GetFiles(pattern, SearchOption.AllDirectories);

            if (forEach != null)
                foreach (var item in files)
                { forEach(item);} 

            return files;
        }

        /// <inheritdoc cref="AllFiles(IDirectoryInfo,string,System.Action{IFileInfo}?)"/>
        public static FileInfo[] AllFiles(this DirectoryInfo root, string pattern = "*", Action<FileInfo>? forEach = null)
        {
            var files = root.GetFiles(pattern, SearchOption.AllDirectories);

            if (forEach != null)
                foreach (var item in files)
                { forEach(item);} 

            return files;
        }

        /// <summary>
        /// Recursive enumerate directories in a parent directory
        /// </summary>
        /// <param name="root">Root folder from which to search</param>
        /// <param name="pattern">optional search pattern to limit items by</param>
        /// <param name="forEach">optional action to be applied to every found item</param>
        public static IDirectoryInfo[] AllFolders(this IDirectoryInfo root, string pattern = "*",
            Action<IDirectoryInfo>? forEach = null)
        {
            var items = root.GetDirectories(pattern, SearchOption.AllDirectories);

            if(forEach != null)
                foreach (var item in items)
                { forEach(item); }

            return items;
        }

        /// <inheritdoc cref="AllFolders(IDirectoryInfo,string,System.Action{IDirectoryInfo}?)"/>
        public static DirectoryInfo[] AllFolders(this DirectoryInfo root, string pattern = "*",
            Action<DirectoryInfo>? forEach = null)
        {
            var items = root.GetDirectories(pattern, SearchOption.AllDirectories);

            if(forEach != null)
                foreach (var item in items)
                { forEach(item); }

            return items;
        }
    }
}

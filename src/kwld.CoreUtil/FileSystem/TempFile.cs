using System;
using System.IO;
using System.IO.Abstractions;

namespace kwld.CoreUtil.FileSystem
{
    /// <summary>
    /// A file or folder that will be deleted when this object leaves scope.
    /// </summary>
    public class TempFile : IDisposable
    {
        private readonly IDirectoryInfo? _folder;
        private readonly IFileInfo? _file;
        private readonly FileInfo? _fileSys;
        private readonly DirectoryInfo? _folderSys;

        /// <summary>
        /// Delete <paramref name="file"/> when disposing.
        /// </summary>
        public TempFile(IFileInfo file) {
            _file = file;
        }

        /// <inheritdoc cref="TempFile"/>
        public TempFile(FileInfo file)
        {
            _fileSys = file;
        }

        /// <summary>
        /// Delete <paramref name="folder"/> when disposing.
        /// </summary>
        /// <param name="folder"></param>
        public TempFile(IDirectoryInfo folder) {
            _folder = folder;
        }

        /// <inheritdoc cref="TempFile"/>
        public TempFile(DirectoryInfo folder)
        {
            _folderSys = folder;
        }

        /// <inheritdoc cref="IDisposable.Dispose"/>
        public void Dispose()
        {
            _file?.EnsureDelete();
            _folder?.EnsureDelete();

            _fileSys?.EnsureDelete();
            _folderSys?.EnsureDelete();
        }
    }
}

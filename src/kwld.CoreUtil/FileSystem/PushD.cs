using System;
using System.IO;
using System.IO.Abstractions;

namespace kwld.CoreUtil.FileSystem
{
    /// <summary>
    /// Set the current directory, reverting
    /// to previous when this leaves scope.
    /// </summary>
    public class PushD : IDisposable
    {
        private readonly IDirectoryInfo? _previous;
        private readonly string? _previousPath;

        /// <inheritdoc cref="PushD"/>
        public PushD(IDirectoryInfo folder)
        {
            _previous = folder.FileSystem.Current();

            folder.SetCurrentDirectory();
        }

        public PushD(DirectoryInfo folder)
        {
            _previousPath = Directory.GetCurrentDirectory();
            folder.SetCurrentDirectory();
        }

        /// <inheritdoc cref="IDisposable.Dispose"/>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            _previous?.SetCurrentDirectory();
            
            if(_previousPath != null)
                Directory.SetCurrentDirectory(_previousPath);
        }
    }
}
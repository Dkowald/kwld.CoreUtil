using System;
using System.IO.Abstractions;

namespace kwd.CoreUtil.FileSystem
{
    /// <summary>
    /// Set the current directory, reverting
    /// to previous when this leaves scope.
    /// </summary>
    public class PushD : IDisposable
    {
        private readonly IDirectoryInfo _previous;

        /// <inheritdoc cref="PushD"/>
        public PushD(IDirectoryInfo folder)
        {
            _previous = folder.FileSystem.Current();

            folder.SetCurrentDirectory();
        }

        /// <inheritdoc cref="IDisposable.Dispose"/>
        public void Dispose()
        {
            _previous.SetCurrentDirectory();
        }
    }
}
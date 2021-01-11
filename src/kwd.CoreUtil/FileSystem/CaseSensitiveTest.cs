using System;
using System.IO;

namespace kwd.CoreUtil.FileSystem
{
    /// <summary>
    /// Simple test for case-sensitive file system.
    /// </summary>
    public class CaseSensitiveTest : ICaseSensitiveTest
    {
        private readonly FileInfo _testFilePath;
        private readonly FileInfo _altTestFilePath;

        /// <summary>
        /// Create case sensitive test object, using
        /// target directory: <paramref name="testDirectory"/>.
        /// </summary>
        public CaseSensitiveTest(DirectoryInfo testDirectory)
        {
            testDirectory.Refresh();
            if (!testDirectory.Exists)
            {
                throw new ArgumentException("Case sensitive test folder must exist.", nameof(testDirectory));
            }

            _testFilePath = testDirectory.GetFile($".{nameof(CaseSensitiveTest)}");

            _altTestFilePath = new FileInfo(_testFilePath.FullName.ToLower());
        }
        
        /// <summary>True if file system detected as case-sensitive</summary>
        public bool IsCaseSensitive => CheckCaseSensitivity();

        /// <summary>
        /// ReSet (delete) the test file.
        /// </summary>
        /// <remarks>
        /// Useful during install / uninstall flows.
        /// </remarks>
        public CaseSensitiveTest Reset()
        {
            _testFilePath.EnsureDelete();
            _altTestFilePath.EnsureDelete();
            return this;
        }

        private bool CheckCaseSensitivity()
        {
            if (!_testFilePath.Exists)
            {
                _testFilePath.Touch();
                _testFilePath.Attributes = FileAttributes.Hidden;
                _altTestFilePath.Refresh();
            }
            
            return _altTestFilePath.Exists;
        }
    }
}
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FromDirectoryExtensions = kwld.CoreUtil.FileSystem.FromDirectoryExtensions;

namespace kwld.CoreUtil.Tests.FileSystem
{
    [TestClass]
    public class FromDirectoryExtensionsTests
    {
        [TestMethod]
        public void HasAllMethods()
        {
            //file methods, that don't make sense as an extension.
            var irrelevant = new[]
            {
                "GetParent", "CreateDirectory", "Exists",
                "SetCreationTime", "SetCreationTimeUtc",
                "GetCreationTime", "GetCreationTimeUtc",
                "SetLastWriteTime", "SetLastWriteTimeUtc",
                "GetLastWriteTime", "GetLastWriteTimeUtc",
                "SetLastAccessTime", "SetLastAccessTimeUtc",
                "GetLastAccessTime", "GetLastAccessTimeUtc",

                "GetFiles", "GetDirectories", "GetFileSystemEntries",
                "EnumerateDirectories", "EnumerateFiles", "EnumerateFileSystemEntries",
                "GetDirectoryRoot", "GetCurrentDirectory",
                "Move", "Delete", "GetLogicalDrives",

                "CreateSymbolicLink", "ResolveLinkTarget"
            };

            var available = typeof(Directory).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .DistinctBy(op => op.Name)
                .Select(x => x.Name)
                .Except(irrelevant)
                .ToArray();

            var implemented = typeof(FromDirectoryExtensions).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .DistinctBy(op => op.Name)
                .Select(x => x.Name)
                .ToArray();

            var extras = implemented.Except(available);
            Assert.IsFalse(extras.Any());
            
            var missed = available.Except(implemented);
            Assert.IsFalse(missed.Any());
        }
    }
}

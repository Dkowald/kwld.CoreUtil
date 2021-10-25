using System.IO;

using kwd.CoreUtil.FileSystem;
using kwd.CoreUtil.Tests.TestHelpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace kwd.CoreUtil.Tests.FileSystem
{
    [TestClass]
    public class ResolvePathExtensionsTests
    {
        [TestMethod]
        public void IsCaseSensitive_()
        {
            var dir = Files.Project;

            dir.IsCaseSensitive();
            Assert.IsTrue(true, "Can call");
        }
        
        [TestMethod]
        public void FindFolder_()
        {
            var testDir = Files.AppData.Get(nameof(ResolvePathExtensionsTests));
            testDir.EnsureDelete().EnsureExists();

            var path = nameof(ResolvePathExtensionsTests).ToLower();

            var result = Files.AppData.FindFolder(path);

            var expected = nameof(ResolvePathExtensionsTests) + Path.DirectorySeparatorChar;

            Assert.IsTrue(result.FullName.EndsWith(expected));
        }

        [TestMethod]
        public void FindFile_()
        {
            var testDir = Files.AppData.Get(nameof(ResolvePathExtensionsTests));
            var testFile = testDir.GetFile("Test.txt");
            
            testFile.EnsureDelete().EnsureExists();

            var result = Files.AppData.FindFile(nameof(ResolvePathExtensionsTests).ToUpper(), testFile.Name.ToLower());

            var expected = Path.Combine(nameof(ResolvePathExtensionsTests), "Test.txt");

            Assert.IsTrue(result.FullName.EndsWith(expected));
        }
    }
}
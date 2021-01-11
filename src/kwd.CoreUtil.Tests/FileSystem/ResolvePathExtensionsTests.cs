using System;
using System.IO;
using System.Linq;
using kwd.CoreUtil.FileSystem;
using kwd.CoreUtil.Tests.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace kwd.CoreUtil.Tests.FileSystem
{
    [TestClass]
    public class ResolvePathExtensionsTests
    {
        [TestMethod]
        public void ResolveFile_()
        {
            var testFile = Files.AppData.GetFile(nameof(ResolvePathExtensionsTests), "Tests.txt");
            testFile.EnsureDelete().EnsureExists();

            Environment.SetEnvironmentVariable("FILENAME", testFile.Name.ToLower());

            var expected = nameof(ResolvePathExtensionsTests) +
                           Path.DirectorySeparatorChar +
                           testFile.Name;

            var result = Files.AppData.ResolveFile(nameof(ResolvePathExtensionsTests).ToUpper(), "%FILENAME%");

            Assert.IsTrue(result.FullName.EndsWith(expected));
        }

        [TestMethod]
        public void ResolveDir_RelativeWithEnv()
        {
            var testDir = Files.AppData.Get(nameof(ResolvePathExtensionsTests));
            testDir.EnsureDelete().EnsureExists();

            Directory.GetDirectories(Files.AppData.FullName.ToLower(), "*", new EnumerationOptions
            {
                MatchCasing = MatchCasing.CaseInsensitive,
            });

            Environment.SetEnvironmentVariable("TestPath", "./"+ nameof(ResolvePathExtensionsTests).ToUpper());
            
            var result = Files.AppData.ResolveDir("%TestPath%");

            var expected = nameof(ResolvePathExtensionsTests) + Path.DirectorySeparatorChar;

            Assert.IsTrue(result.FullName.EndsWith(expected));
        }

        [TestMethod]
        public void CaseMatchDir_RelativePath()
        {
            Files.AppData.Get(nameof(ResolvePathExtensionsTests)).EnsureDelete().EnsureExists();

            var result = Files.AppData.CaseMatchDir("../", "app_data", nameof(ResolvePathExtensionsTests).ToUpper(), "notReal", "../");

            var expected = nameof(ResolvePathExtensionsTests) + Path.DirectorySeparatorChar;
            Assert.IsTrue(result.FullName.EndsWith(expected));
        }

        [TestMethod]
        public void CaseMatchDir_()
        {
            var testDir = Files.AppData.Get(nameof(ResolvePathExtensionsTests));
            testDir.EnsureDelete().EnsureExists();

            var path = nameof(ResolvePathExtensionsTests).ToLower();

            var result = Files.AppData.CaseMatchDir(path);

            var expected = nameof(ResolvePathExtensionsTests) + Path.DirectorySeparatorChar;

            Assert.IsTrue(result.FullName.EndsWith(expected));
        }

        [TestMethod]
        public void CaseMatchFile_()
        {
            var testDir = Files.AppData.Get(nameof(ResolvePathExtensionsTests));
            var testFile = testDir.GetFile("Test.txt");
            
            testFile.EnsureDelete().EnsureExists();

            var result = Files.AppData.CaseMatchFile(nameof(ResolvePathExtensionsTests).ToUpper(), testFile.Name.ToLower());

            var expected = Path.Combine(nameof(ResolvePathExtensionsTests), "Test.txt");

            Assert.IsTrue(result.FullName.EndsWith(expected));
        }
    }
}
using System;
using System.IO;
using kwld.CoreUtil.FileSystem;
using kwld.CoreUtil.Tests.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Testably.Abstractions.Testing;

namespace kwld.CoreUtil.Tests.FileSystem
{
    [TestClass]
    public class ResolvePathExtensionsTests
    {
        [TestMethod]
        public void Expand_()
        {
            var files = new MockFileSystem(o => o.SimulatingOperatingSystem(SimulationMode.Windows));
            
            Environment.SetEnvironmentVariable("TEST", "MyPlace");
            
            files.Directory.CreateDirectory("c:/etc/");
            files.Directory.SetCurrentDirectory("c:/etc");

            var target = files.FileInfo.New("./%TEST%/config.data");

            var rootWithCurrent = target.Expand();

            var expected = @"c:\etc\MyPlace\config.data";
            Assert.AreEqual(expected, rootWithCurrent.FullName, "Expand and use current dir");

            var otherRoot = files.DirectoryInfo.New(@"c:\myapp\user-data\");

            expected = @"c:\myapp\user-data\MyPlace\config.data";
            var result = target.Expand(otherRoot);
            Assert.AreEqual(expected, result.FullName, "Expand with custom root");

            var dirTarget = files.DirectoryInfo.New("../settings/general/");

            expected = @"c:\myapp\settings\general\";
            var expandedDir = dirTarget.Expand(otherRoot);
            Assert.AreEqual(expected, expandedDir.FullName);
        }

        [TestMethod]
        public void IsCaseSensitive_True()
        {
            var files = new MockFileSystem(o => o.SimulatingOperatingSystem(SimulationMode.Linux));
            var dir = files.DirectoryInfo.New("/user/me/Profile");
            files.Directory.CreateDirectory(dir.FullName);

            var result = dir.IsCaseSensitive();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsCaseSensitive_False()
        {
            var files = new MockFileSystem(o => o.SimulatingOperatingSystem(SimulationMode.MacOS));
            var file = files.FileInfo.New("c:/temp/user/Data/Profile.txt");
            files.Directory.CreateDirectory(file.DirectoryName!);
            files.File.WriteAllText(file.FullName, "data");

            var result = file.IsCaseSensitive();
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsCaseSensitive_FileSystem()
        {
            var files = new MockFileSystem(o => o.SimulatingOperatingSystem(SimulationMode.Linux));
            var dir = files.Directory.CreateDirectory("/user/me/Profile");
            files.Directory.SetCurrentDirectory(dir.FullName);
            
            Assert.IsTrue(files.IsCaseSensitive());
        }

        [TestMethod]
        public void FindFolder_()
        {
            var testDir = Files.AppData.GetFolder(nameof(ResolvePathExtensionsTests));
            testDir.EnsureDelete().EnsureExists();

            var path = nameof(ResolvePathExtensionsTests).ToLower();

            var result = Files.AppData.FindFolder(path);

            var expected = nameof(ResolvePathExtensionsTests) + Path.DirectorySeparatorChar;

            Assert.IsTrue(result.FullName.EndsWith(expected));
        }

        [TestMethod]
        public void FindFile_()
        {
            var testDir = Files.AppData.GetFolder(nameof(ResolvePathExtensionsTests));
            var testFile = testDir.GetFile("Test.txt");
            
            testFile.EnsureDelete().EnsureExists();

            var result = Files.AppData.FindFile(nameof(ResolvePathExtensionsTests).ToUpper(), testFile.Name.ToLower());

            var expected = Path.Combine(nameof(ResolvePathExtensionsTests), "Test.txt");

            Assert.IsTrue(result.FullName.EndsWith(expected));
        }

        [TestMethod]
        public void CaseDifferingPath_()
        {
            var src = "/SomeWhere/myFile";
            var result = ResolvePathExtensions.CaseDifferingPath(src);
            Assert.AreNotEqual(src, result);

            src = "/somewhere/myfile";
            result = ResolvePathExtensions.CaseDifferingPath(src);
            Assert.AreNotEqual(src, result);

            src = "/SOMEWHERE/MYFILE";
            result = ResolvePathExtensions.CaseDifferingPath(src);
            Assert.AreNotEqual(src, result);
        }
    }
}
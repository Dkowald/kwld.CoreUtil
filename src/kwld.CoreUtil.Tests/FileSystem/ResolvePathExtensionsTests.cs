using System;
using System.IO;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using kwld.CoreUtil.FileSystem;
using kwld.CoreUtil.Tests.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace kwld.CoreUtil.Tests.FileSystem
{
    [TestClass]
    public class ResolvePathExtensionsTests
    {
        [TestMethod]
        public void Expand_()
        {
            var files = new MockFileSystem();

            //Volume name on windows, '/' on linux.
            var sysRoot = files.Current().Root.FullName;

            Environment.SetEnvironmentVariable("TEST", "MyPlace");
            var target = files.FileInfo.New("./%TEST%/config.data");
            
            files.DirectoryInfo.New(sysRoot +"etc").SetCurrentDirectory();

            var rootWithCurrent = target.Expand();

            var expected = sysRoot + "etc/MyPlace/config.data".Replace('/', Path.DirectorySeparatorChar);
            Assert.AreEqual(expected, rootWithCurrent.FullName, "Expand and use current dir");

            var otherRoot = files.DirectoryInfo.New(sysRoot + "myapp/user-data/");

            expected = sysRoot + "myapp/user-data/MyPlace/config.data".Replace('/', Path.DirectorySeparatorChar);
            Assert.AreEqual(expected, target.Expand(otherRoot).ToString(), "Expand with custom root");

            var dirTarget = files.DirectoryInfo.New("../settings/general/");

            expected = sysRoot + "myapp/settings/general/".Replace('/', Path.DirectorySeparatorChar);
            Assert.AreEqual(expected, dirTarget.Expand(otherRoot).ToString());
        }

        [TestMethod]
        public void IsCaseSensitive_True()
        {
            var mockDirInfo = Substitute.For<IDirectoryInfo>();

            mockDirInfo.Exists.Returns(true);
            mockDirInfo.FullName.Returns("Fullname");

            var files = Substitute.For<IFileSystem>();
            mockDirInfo.FileSystem.Returns(files);

            var mockDirectory = Substitute.For<IDirectory>();
            files.Directory.Returns(mockDirectory);

            mockDirectory.Exists(default)
                .ReturnsForAnyArgs(x => x.Arg<string>() == "Fullname");

            var result = mockDirInfo.IsCaseSensitive();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsCaseSensitive_False()
        {
            var mockDirInfo = Substitute.For<IDirectoryInfo>();

            mockDirInfo.Exists.Returns(true);
            mockDirInfo.FullName.Returns("Fullname");

            var files = Substitute.For<IFileSystem>();
            mockDirInfo.FileSystem.Returns(files);

            var mockDirectory = Substitute.For<IDirectory>();
            files.Directory.Returns(mockDirectory);

            mockDirectory.Exists(default)
                .ReturnsForAnyArgs(x => string.Equals(x.Arg<string>(), "Fullname", StringComparison.OrdinalIgnoreCase));

            var result = mockDirInfo.IsCaseSensitive();
            Assert.IsFalse(result);
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
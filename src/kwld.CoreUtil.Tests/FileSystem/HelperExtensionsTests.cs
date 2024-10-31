using System;
using System.IO;
using kwld.CoreUtil.FileSystem;
using kwld.CoreUtil.Tests.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace kwld.CoreUtil.Tests.FileSystem
{
    [TestClass]
    public class HelperExtensionsTests
    {
        [TestMethod]
        public void Touch_CustomDate()
        {
            var file = Files.AppData.GetFile(nameof(HelperExtensionsTests), "touch.txt");
            var when = new DateTime(2000, 10, 01, 0, 0, 0, DateTimeKind.Utc);
            
            file.Touch(() => when);

            var lastWrite = File.GetLastWriteTimeUtc(file.FullName);

            Assert.AreEqual(when, lastWrite);
        }

        [TestMethod]
        public void AsUri_FolderEnding()
        {
            var target = Directories.AssemblyFolder(typeof(HelperExtensionsTests));

            var result = target.AsUri();

            Assert.IsTrue(result.ToString().EndsWith('/'), 
                "Folder should end with slash");
        }
    }
}
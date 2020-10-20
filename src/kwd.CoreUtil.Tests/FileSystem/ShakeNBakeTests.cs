using System;
using System.IO;
using kwd.CoreUtil.FileSystem;
using kwd.CoreUtil.Tests.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace kwd.CoreUtil.Tests.FileSystem
{
    [TestClass]
    public class ShakeNBakeTests
    {
        [TestMethod]
        public void Touch_CustomDate()
        {
            var file = Files.AppData.GetFile(nameof(ShakeNBakeTests), "touch.txt");
            var when = new DateTime(2000, 10, 01, 0, 0, 0, DateTimeKind.Utc);
            
            file.Touch(() => when);

            var lastWrite = File.GetLastWriteTimeUtc(file.FullName);

            Assert.AreEqual(when, lastWrite);
        }

        [TestMethod]
        public void AsUri_FolderEnding()
        {
            var target = new DirectoryInfo("c:/temp");

            var result = target.AsUri();

            Assert.IsTrue(result.ToString().EndsWith('/'), 
                "Folder should end with slash");
        }
    }
}
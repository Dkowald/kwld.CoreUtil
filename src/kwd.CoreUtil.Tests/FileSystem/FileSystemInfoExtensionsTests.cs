using System.IO;

using kwd.CoreUtil.FileSystem;
using kwd.CoreUtil.Tests.TestHelpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace kwd.CoreUtil.Tests.FileSystem
{
    [TestClass]
    public class FileSystemInfoExtensionsTests
    {
        [TestMethod]
        public void CreateFile()
        {
            var folder = Path.Combine(Files.AppData.FullName, nameof(CreateFile));

            if(Directory.Exists(folder))
                Directory.Delete(folder, true);

            var path = Path.Combine(folder, "test.txt");
            var target = new FileInfo(path);
            
            Assert.ThrowsException<DirectoryNotFoundException>(() => target.Create().Dispose(), 
                "Normal create fails if folder not exist");

            Assert.IsTrue(target.EnsureCreate().Exists, "Creates file and folder structure");
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

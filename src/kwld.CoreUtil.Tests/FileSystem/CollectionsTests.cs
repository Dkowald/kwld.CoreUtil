using System.IO.Abstractions;
using kwld.CoreUtil.FileSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace kwld.CoreUtil.Tests.FileSystem
{
    [TestClass]
    public class CollectionsTests
    {
        private readonly IDirectoryInfo _root =
            new System.IO.Abstractions.FileSystem()
            .Project().GetFolder("App_Data", nameof(CollectionsTests));

        [TestMethod]
        public void AllFiles_()
        {
            var root = _root.GetFolder(nameof(AllFiles_))
                .EnsureEmpty();

            var dir = root.GetFolder("ReadOnlyFiles");
          
            var file = dir.GetFile("A.txt").Touch();
            file.IsReadOnly = true;

            try
            {
                root.EnsureEmpty(deleteReadOnly: false);
                Assert.Fail("Expected fail since some content is read-only");
            }
            catch{/*ignored*/}

            root.AllFiles(forEach: f => f.IsReadOnly = false);

            root.EnsureEmpty(deleteReadOnly: false);
        }

        [TestMethod]
        public void AllFolders_()
        {
            var files = _root.GetFolder(nameof(AllFolders_))
                .EnsureDelete();

            files.GetFolder("Proj1", "bin", "unit_Tests").EnsureExists();
            files.GetFolder("other", "sample", "Tests", "subTests").EnsureExists();
            
            var testFolders = files.AllFolders("*Test*");

            Assert.AreEqual(3, testFolders.Length);
        }

        [TestMethod]
        public void All_()
        {
            var files = _root.GetFolder(nameof(All_));
            files.EnsureEmpty();

            files.GetFile("Sample/bin/info.md").EnsureExists();
            files.GetFile("Sample/obj/info.md").EnsureExists();

            var folderCount = 0;
            var fileCount = 0;
            files.All("*in*", forEach: x =>{if(x is IFileInfo) fileCount++; else folderCount++;});
            
            Assert.AreEqual(1, folderCount);
            Assert.AreEqual(2, fileCount);
        }
    }
}

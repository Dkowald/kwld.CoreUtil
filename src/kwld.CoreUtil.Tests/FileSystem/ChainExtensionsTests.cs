using kwld.CoreUtil.FileSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace kwld.CoreUtil.Tests.FileSystem
{
    [TestClass]
    public class ChainExtensionsTests
    {
        [TestMethod]
        public void EnsureEmpty_WhenFolderLocked()
        {
            var files = new System.IO.Abstractions.FileSystem();

            var target = files.Project().GetFolder("App_Data", "EmptyMe").EnsureExists();

            target.GetFile("a.txt").Touch();
            
            //lock via set current dir.
            using var _ = target.PushD();
            
            target.EnsureEmpty();

            var childItems = target.GetFileSystemInfos().Length;
            Assert.AreEqual(0, childItems);
        }
    }
}

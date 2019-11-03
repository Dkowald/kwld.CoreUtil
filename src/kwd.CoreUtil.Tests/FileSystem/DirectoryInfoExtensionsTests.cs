using System.Linq;
using kwd.CoreUtil.FileSystem;
using kwd.CoreUtil.Tests.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace kwd.CoreUtil.Tests.FileSystem
{
    [TestClass]
    public class DirectoryInfoExtensionsTests
    {
        [TestMethod]
        public void Prune_Success()
        {
            var target = Files.AppData.Get(nameof(Prune_Success));

            target.EnsureDelete().EnsureCreate();

            target.Get("sub1").EnsureCreate();
            target.Get("sub2").EnsureCreate();
            target.Get("keep").GetFile("test.txt").EnsureCreate();

            target.Prune();

            var remainingFolder = target.EnumerateDirectories().ToList()
                .Single();

            Assert.AreEqual("keep", remainingFolder.Name, 
                "Keeps the single folder with a file");
        }
    }
}
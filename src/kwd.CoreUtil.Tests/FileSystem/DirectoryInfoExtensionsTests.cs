using System;
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
        public void TreeCUD_Success()
        {
            var root = Files.AppData.GetFolder(nameof(TreeCUD_Success)).EnsureDelete();

            var src = root.GetFolder("src");
            var other = root.GetFolder("other");

            var same = src.GetFile("same.txt").EnsureCreate();
            other.GetFile("same.txt").Touch(() => same.LastWriteTimeUtc);

            src.GetFile("updated.txt").Touch(() => DateTime.UtcNow.AddDays(-1));
            other.GetFile("updated.txt").EnsureCreate();

            src.GetFile("deleted.txt").EnsureCreate();

            other.GetFile("created.txt").EnsureCreate();

            var (created, updated, deleted) = src.TreeCUD(other);

            Assert.AreEqual("created.txt", created.Single().Name);
            Assert.AreEqual("updated.txt", updated.Single().Name);
            Assert.AreEqual("deleted.txt", deleted.Single().Name);
        }

        [TestMethod]
        public void TreeSameFiles_Success()
        {
            var root = Files.AppData.GetFolder(nameof(TreeSameFiles_Success))
                .EnsureDelete();
            var dir1 = root.GetFolder("dir1");
            var dir2 = root.GetFolder("dir2");

            dir1.GetFolder("same").GetFile("test.txt").EnsureCreate();
            dir1.GetFile("removed.txt").EnsureCreate();

            dir2.GetFolder("same").GetFile("test.txt").EnsureCreate();
            dir2.GetFolder("added").GetFile("added.txt").EnsureCreate();

            var matchedFiles = dir1.TreeSameFiles(dir2).ToList();
            Assert.AreEqual(1, matchedFiles.Count, "Found the single match");
            var same = matchedFiles.Single();
            Assert.AreEqual("same\\test.txt", same.GetRelativePath(dir2));
            
        }

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
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
        public void Merge_WithUpdates()
        {
            var root = Files.AppData.GetFolder(nameof(Merge_WithUpdates))
                .EnsureDelete();

            var src = root.GetFolder("src");
            var dest = root.GetFolder("dest");

            src.GetFile("new.txt").EnsureCreate();

            src.GetFile("updated.txt").EnsureCreate();

            var oldDate = DateTime.UtcNow.AddDays(-1);
            dest.GetFile("updated.txt").Touch(() => oldDate);

            dest.Merge(src);
            Assert.IsTrue(dest.GetFile("updated.txt").LastWriteTimeUtc > oldDate, 
                "Got the updated file");
        }

        [TestMethod]
        public void Merge_NewFilesOnly()
        {
            var root = Files.AppData.GetFolder(nameof(Merge_NewFilesOnly))
                .EnsureDelete();

            var src = root.GetFolder("src");
            var dest = root.GetFolder("dest");

            src.GetFile("newFile.txt").EnsureCreate();

            src.GetFile("other", "same.txt").EnsureCreate();

            var oldFileDate = DateTime.UtcNow.AddDays(-1);
            dest.GetFile("other", "same.txt").Touch(() => oldFileDate);

            dest.GetFile("existingFile.txt").EnsureCreate();
            dest.Merge(src, false);

            Assert.IsTrue(dest.GetFile("existingFile.txt").Exists, "Keeps my current file");

            Assert.IsTrue(dest.GetFile("newFile.txt").Exists, "Got new file");

            Assert.AreEqual(oldFileDate, dest.GetFile("other/same.txt").LastWriteTimeUtc,
                "Keep my old copy");
        }

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
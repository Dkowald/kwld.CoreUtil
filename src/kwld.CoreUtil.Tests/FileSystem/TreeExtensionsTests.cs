using System;
using System.IO;
using System.Linq;
using kwld.CoreUtil.FileSystem;
using kwld.CoreUtil.Tests.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace kwld.CoreUtil.Tests.FileSystem
{
    [TestClass]
    public class TreeExtensionsTests
    {
        [TestMethod]
        public void Merge_WithUpdates()
        {
            var root = Files.AppData.GetFolder(nameof(Merge_WithUpdates))
                .EnsureDelete();
            
            var src = root.GetFolder("src");
            var dest = root.GetFolder("dest");

            src.GetFile("sub1", "new.txt").EnsureExists();

            src.GetFile("updated.txt").EnsureExists();

            var oldDate = DateTime.UtcNow.AddDays(-1);
            dest.GetFile("updated.txt").Touch(() => oldDate);

            src.Merge(dest);

            Assert.IsTrue(dest.GetFile("updated.txt").LastWriteTimeUtc > oldDate, 
                "Got the updated file");

            Assert.IsTrue(dest.GetFile("sub1", "new.txt").Exists, "New file coped");
        }

        [TestMethod]
        public void Merge_NewFilesOnly()
        {
            var root = Files.AppData.GetFolder(nameof(Merge_NewFilesOnly))
                .EnsureDelete();

            var src = root.GetFolder("src");
            var dest = root.GetFolder("dest");

            src.GetFile("newFile.txt").EnsureExists();

            src.GetFile("other", "same.txt").EnsureExists();

            var oldFileDate = DateTime.UtcNow.AddDays(-1);
            dest.GetFile("other", "same.txt").Touch(() => oldFileDate);

            dest.GetFile("existingFile.txt").EnsureExists();
            src.Merge(dest, false);

            Assert.IsTrue(dest.GetFile("existingFile.txt").Exists, "Keeps my current file");

            Assert.IsTrue(dest.GetFile("newFile.txt").Exists, "Got new file");

            Assert.AreEqual(oldFileDate, dest.GetFile("other/same.txt").LastWriteTimeUtc,
                "Keep my old copy");
        }

        [TestMethod]
        public void TreeDiff_Success()
        {
            var root = Files.AppData.GetFolder(nameof(TreeDiff_Success)).EnsureDelete();

            var src = root.GetFolder("src");
            var other = root.GetFolder("other");

            var same = src.GetFile("same.txt").EnsureExists();
            other.GetFile("same.txt").Touch(() => same.LastWriteTimeUtc);

            src.GetFile("updated.txt").Touch(() => DateTime.UtcNow.AddDays(-1));
            other.GetFile("updated.txt").EnsureExists();

            src.GetFile("deleted.txt").EnsureExists();

            other.GetFile("created.txt").EnsureExists();

            var (created, updated, deleted) = src.TreeDiff(other);

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

            dir1.GetFolder("same").GetFile("test.txt").EnsureExists();
            dir1.GetFile("removed.txt").EnsureExists();

            dir2.GetFolder("same").GetFile("test.txt").EnsureExists();
            dir2.GetFolder("added").GetFile("added.txt").EnsureExists();

            var matchedFiles = dir1.TreeSameFiles(dir2).ToList();

            Assert.AreEqual(1, matchedFiles.Count, "Found the single match");
            
            var same = matchedFiles.Single();
            Assert.AreEqual(Path.Combine("same", "test.txt"), same.GetRelativePath(dir2));
        }

        [TestMethod]
        public void Prune_Success()
        {
            var target = Files.AppData.GetFolder(nameof(Prune_Success));

            target.EnsureDelete().EnsureExists();

            target.GetFolder("sub1").EnsureExists();
            target.GetFolder("sub2").EnsureExists();
            target.GetFile("keep","test.txt").EnsureExists();

            target.Prune();

            var remainingFolder = target.EnumerateDirectories().ToList()
                .Single();

            Assert.AreEqual("keep", remainingFolder.Name,
                "Keeps the single folder with a file");
        }
    }
}
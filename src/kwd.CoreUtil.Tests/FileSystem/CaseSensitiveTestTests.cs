using System.Linq;
using kwd.CoreUtil.FileSystem;
using kwd.CoreUtil.Tests.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace kwd.CoreUtil.Tests.FileSystem
{
    [TestClass]
    public class CaseSensitiveTestTests
    {
        [TestMethod]
        public void Reset_()
        {
            var path = Files.AppData.Get(nameof(CaseSensitiveTestTests));
            path.EnsureDelete().EnsureExists();

            var target = new CaseSensitiveTest(path);

            Assert.IsFalse(path.EnumerateFiles().Any(), "Lazy test file creation");

            var _ = target.IsCaseSensitive;

            Assert.IsTrue(path.EnumerateFiles().Any(), "Test file created");

            target.Reset();

            Assert.IsFalse(path.EnumerateFiles().Any(), "Clean up test file.");
        }
    }
}
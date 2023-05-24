using System.IO;
using kwd.CoreUtil.FileSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace kwd.CoreUtil.Tests.FileSystem
{
    [TestClass]
    public class FromPathExtensionsTests
    {
        [TestMethod]
        public void GetFullPath_()
        {
            var sysRoot = Directories.Current().Root;

            Directories.Temp().SetCurrentDirectory();

            var target = new FileInfo("../other");
            
            var root = sysRoot.GetFolder("./user/home");

            var expected = sysRoot.GetFolder("./user/other");

            Assert.AreEqual(expected.FullName, target.GetFullPath(root).FullName,
                "Uses the provided root, not the current directory");
        }
    }
}

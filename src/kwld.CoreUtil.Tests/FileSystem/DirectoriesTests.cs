using System;
using System.IO;
using kwld.CoreUtil.FileSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace kwld.CoreUtil.Tests.FileSystem
{
    [TestClass]
    public class DirectoriesTests
    {
        [TestMethod]
        public void Project_FromCaller()
        {
            var projectDirectory = Directories.Project();

            Assert.IsTrue(projectDirectory.Exists);

            var projectFile = projectDirectory.GetFile("kwld.CoreUtil.Tests.csproj");
            Assert.IsTrue(projectFile.Exists);
        }

        [TestMethod]
        public void Home_PreferHomeEnvironmentVariable()
        {
            var tmp = new DirectoryInfo("c:/temp/test");
            Environment.SetEnvironmentVariable("USERPROFILE", tmp.GetFile("other").FullName);
            Environment.SetEnvironmentVariable("HOME", tmp.FullName);
            
            var home = Directories.Home();
            Assert.AreEqual(tmp.FullName, home.FullName, "Use $HOME");
        }
    }
}
﻿using kwd.CoreUtil.FileSystem;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace kwd.CoreUtil.Tests.build;

[TestClass]
public class DownloadFilesTests
{
    [TestMethod]
    public void DownloadFile_()
    {
        var expectedAssetFile = Directories.Project().GetFile("App_Data", "Readme.md");

        Assert.IsTrue(expectedAssetFile.Exists, "Downloaded file as part of build.");
    }
}
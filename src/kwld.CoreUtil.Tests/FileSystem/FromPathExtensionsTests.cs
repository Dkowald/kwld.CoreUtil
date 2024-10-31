using System;
using System.IO;
using System.Linq;
using System.Reflection;
using kwld.CoreUtil.FileSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace kwld.CoreUtil.Tests.FileSystem
{
  [TestClass]
  public class FromPathExtensionsTests
  {
    [TestMethod]
    public void HasAllMethods()
    {
      //path methods, that don't make sense as an extension.
      var irrelevant = new[]
      {
          "GetDirectoryName",
          "GetFileName",
          "IsPathFullyQualified",
          "Combine",
          "Join", "TryJoin",
          "TrimEndingDirectorySeparator", "EndsInDirectorySeparator",
          "GetInvalidFileNameChars", "GetInvalidPathChars",
          "IsPathRooted", "GetPathRoot",
          "GetRandomFileName","GetTempPath","GetTempFileName",
          "Exists"
        };
      Path.GetTempFileName();
      Path.GetRandomFileName();
      var available = typeof(Path).GetMethods(BindingFlags.Public | BindingFlags.Static)
        .DistinctBy(op => op.Name)
        .Select(x => x.Name)
        .Except(irrelevant)
        .ToArray();
      
      var implemented = typeof(FromPathExtensions).GetMethods(BindingFlags.Public | BindingFlags.Static)
        .DistinctBy(op => op.Name)
        .Select(x => x.Name)
        .ToArray();
      
      var extras = implemented.Except(available);
      Assert.IsFalse(extras.Any());
      
      var missed = available.Except(implemented);
      Assert.IsFalse(missed.Any());
    }

    [TestMethod]
    public void GetFullPath_()
    {
      var sysRoot = Directories.Project().Root;

      using var tmp = Directories.Temp().PushD();
      
      var target = new FileInfo("../other");

      var root = sysRoot.GetFolder("./user/home");

      var expected = sysRoot.GetFolder("./user/other");

      Assert.AreEqual(expected.FullName, target.GetFullPath(root).FullName,
          "Uses the provided root, not the current directory");
    }
  }
}

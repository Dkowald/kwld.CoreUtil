using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FromFileExtensions = kwld.CoreUtil.FileSystem.FromFileExtensions;

namespace kwld.CoreUtil.Tests.FileSystem
{
    [TestClass]
  public class FromFileExtensionsTests
  {
    [TestMethod]
    public void HasAllMethods()
    {
      //file methods, that don't make sense as an extension.
      var irrelevant = new []
      {
        "Open", "OpenText", "CreateText",
        "Copy", "Create", "Delete", "Exists",
        "SetCreationTime", "SetCreationTimeUtc",
        "GetCreationTime", "GetCreationTimeUtc",
        "SetLastAccessTime", "SetLastAccessTimeUtc",
        "GetLastAccessTime", "GetLastAccessTimeUtc",
        "SetLastWriteTime", "SetLastWriteTimeUtc",
        "GetLastWriteTime", "GetLastWriteTimeUtc",
        "CreateSymbolicLink", "ResolveLinkTarget",
        "GetAttributes", "SetAttributes",
        "OpenRead", "OpenWrite", "Replace",
        "Move", "Encrypt", "Decrypt",
        "GetUnixFileMode", "SetUnixFileMode"
      };
      
      var available = typeof(File).GetMethods(BindingFlags.Public | BindingFlags.Static)
        .DistinctBy(op => op.Name)
        .Select(x => x.Name)
        .Except(irrelevant)
        .ToArray();

      var implemented = typeof(FromFileExtensions).GetMethods(BindingFlags.Public | BindingFlags.Static)
        .DistinctBy(op => op.Name)
        .Select(x => x.Name)
        .ToArray();

      var extras = implemented.Except(available);
      Assert.IsFalse(extras.Any());

      var missed = available.Except(implemented);
      Assert.IsFalse(missed.Any());
    }
  }
}

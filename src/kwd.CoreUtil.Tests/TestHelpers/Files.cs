using System.IO;
using kwd.CoreUtil.FileSystem;

namespace kwd.CoreUtil.Tests.TestHelpers
{
    public static class Files
    {
        public static DirectoryInfo Project =>
            Directories.AssemblyFolder().GetFolder("../../../");

        public static DirectoryInfo AppData =>
            new DirectoryInfo(Path.Combine(Project.FullName, "App_Data"));
    }
}

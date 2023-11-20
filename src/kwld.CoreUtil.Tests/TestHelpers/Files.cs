using System.IO;
using kwld.CoreUtil.FileSystem;

namespace kwld.CoreUtil.Tests.TestHelpers
{
    public static class Files
    {
        public static DirectoryInfo Project =>
            Directories.AssemblyFolder(typeof(Files)).GetFolder("../../../");

        public static DirectoryInfo AppData =>
            new DirectoryInfo(Path.Combine(Project.FullName, "App_Data"));
    }
}

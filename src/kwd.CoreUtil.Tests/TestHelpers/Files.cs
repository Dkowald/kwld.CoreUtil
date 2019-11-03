using System.IO;

namespace kwd.CoreUtil.Tests.TestHelpers
{
    public static class Files
    {
        public static DirectoryInfo Project =>
            new DirectoryInfo(
                Path.GetFullPath("../../../", 
                    Path.GetDirectoryName(typeof(Files).Assembly.Location)??""));

        public static DirectoryInfo AppData =>
            new DirectoryInfo(Path.Combine(Project.FullName, "App_Data"));
    }
}

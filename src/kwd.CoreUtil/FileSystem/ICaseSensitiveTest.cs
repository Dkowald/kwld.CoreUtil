namespace kwd.CoreUtil.FileSystem
{
    /// <summary>
    /// Test case-sensitive 
    /// </summary>
    public interface ICaseSensitiveTest
    {
        /// <summary>True if file system detected as case-sensitive</summary>
        bool IsCaseSensitive { get; }
    }
}
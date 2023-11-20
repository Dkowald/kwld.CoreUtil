#if NET7_0_OR_GREATER
namespace kwld.CoreUtil.Strings;

/// <summary>
/// <inheritdoc cref="IDataString"/>
/// </summary>
/// <typeparam name="TSelf">The same class implementing this interface</typeparam>
public interface IDataString<TSelf> : IDataString
    where TSelf: class, IDataString<TSelf>
{
    /// <summary>
    /// DataString MUST provide a TryParse static member for serialization.
    /// </summary>
    static abstract TSelf? TryParse(string? data);
}
#endif
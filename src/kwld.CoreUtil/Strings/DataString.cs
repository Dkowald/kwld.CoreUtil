#if NET6_0_OR_GREATER

namespace kwld.CoreUtil.Strings;

/// <summary>
/// A class that implements the DataString pattern
/// <list type="bullet">
/// <item>Should be a record type (immutable)</item>
/// <item>MUST overload ToString() to return a value that could be used by TryParse</item>
/// <item>MUST provide a T? TryParse(string? data) static member</item>
/// <item>Should provide a constructor that takes ony a string: T(string data).</item>
/// <item>Should provide an implicit cast to string.</item>
/// </list>
/// </summary>
/// <remarks>
/// Identifies a class to replace a raw string with a parsed / formatted DataString.
/// <br/>
/// use [return: NotNullIfNotNull(nameof(item))] with nullable string cast.
/// </remarks>
public interface IDataString
{
    /// <summary>
    /// Returns the string version of the <see cref="IDataString"/>,
    /// defaults to ToString()
    /// </summary>
    string Value => ToString() ?? string.Empty;
}
#endif

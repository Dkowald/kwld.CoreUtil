using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using kwld.CoreUtil.Strings;

namespace kwld.CoreUtil.Tests.String.samples;

/// <summary>
/// <list type="bullet">
/// <item>cannot be empty</item>
/// <item>length &lt; 20</item>
/// <item>only contains alpha-numeric</item>
/// <item>convert to lowercase only</item>
/// <item>trimmed</item>
/// </list>
/// </summary>
/// <remarks>
/// Demo simple data string.
/// </remarks>
public record UserName : IDataString
{
    private readonly string _data;

    private static (string? error, string? cleanData) TryRead(string data)
    {
        data = data.Trim();

        if (data.Length == 0)
            return ("cannot be empty", null);

        if (data.Length > 20)
            return ("length < 20", null);

        if(!data.All(char.IsLetterOrDigit))
            return ("only contains alpha-numeric", null);
        
        return (null, data.ToLower());
    }

    private UserName(string data, bool isChecked)
    {
        if (!isChecked)
        {
            var (error, cleanData) = TryRead(data);

            if (cleanData is null)
                throw new ArgumentException(error, nameof(data));

            data = cleanData;
        }

        _data = data;
    }

    public static UserName? TryParse(string? data)
    {
        if (data is null) return null;

        var (_, cleanData) = TryRead(data);

        return cleanData is null ? null : new(cleanData, true);
    }

    [return: NotNullIfNotNull(nameof(item))]
    public static implicit operator string? (UserName? item) => item?._data;

    /// <inheritdoc cref="UserName"/>
    public UserName(string data):this(data, false){}

    public override string ToString() => _data;
}
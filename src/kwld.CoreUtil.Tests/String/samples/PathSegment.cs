using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using kwld.CoreUtil.Strings;

namespace kwld.CoreUtil.Tests.String.samples;

/// <summary>
/// Constrain to simplified file path segment.
/// <list type="bullet">
/// <item>length &lt; 16</item>
/// <item>cannot be empty</item>
/// <item>only contains lowercase alpha-numeric and '-' and '_'</item>
/// <item>start and end with alpha-numeric</item>
/// <item>Lead and trail whitespace trimmed</item>
/// </list>
/// </summary>
/// <remarks>
/// Demo a constrained and cleaned DatString.
/// </remarks>
public record PathSegment : IDataString
{
    private readonly string _data;

    private static (string? error, string? value) Read(string data)
    {
        data = data.Trim();

        if (data.Length > 16) return ("length < 16", null);

        if (data.IsNullOrWhiteSpace()) return ("cannot be empty", null);

        if (data.Any(c => !char.IsLetterOrDigit(c) && c != '-' && c != '_'))
            return ("only contains lowercase alpha-numeric and '-' and '_'", null);

        if (!char.IsLetterOrDigit(data[0]) || !char.IsLetterOrDigit(data[^1]))
            return ("start and end with alpha-numeric", null);

        return (null, data);
    }

    private PathSegment(string data, bool isChecked)
    {
        if (!isChecked)
        {
            var (error, value) = Read(data);

            data = value ?? 
                   throw new ArgumentException(error, nameof(data));
        }

        _data = data;
    }

    public static PathSegment? TryParse(string? data)
    {
        if (data is null) return null;
        
        var (_, cleanValue) = Read(data);

        return cleanValue is null ? null : new PathSegment(cleanValue, true);
    }

    [return: NotNullIfNotNull(nameof(item))]
    public static implicit operator string? (PathSegment? item) => item?.ToString();

    /// <inheritdoc cref="PathSegment"/>
    public PathSegment(string data): this(data, false){}

    public override string ToString() => _data;
}
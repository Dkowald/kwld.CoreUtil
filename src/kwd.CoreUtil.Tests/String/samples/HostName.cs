using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using kwd.CoreUtil.Strings;

namespace kwd.CoreUtil.Tests.String.samples;

/// <summary>
/// string that represents a host name.
/// <list type="bullet">
/// <item>series of <see cref="UserName"/> separated by '.'</item>
/// <item>must have at-least one part.</item>
/// <item>input trimmed, and empty name parts removed.</item>
/// </list>
/// </summary>
/// <remarks>
/// Demo to use other <see cref="IDataString"/>'s and to add constraint
/// on list.
/// </remarks>
public record HostName : IDataString
{
    private readonly UserName[] _parts;
    private readonly string _value;

    private static (string? error, UserName[]? parts) TryRead(string data)
    {
        var parts = data.Split('.', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(UserName.TryParse)
            .ToArray();

        if (parts.Any(x => x is null))
            return ("invalid hostname part found", null);

        if (parts.Length == 0)
            return ("must have at-least one part.", null);

        return (null, parts.OfType<UserName>().ToArray());
    }

    public static HostName? TryParse(string? data)
    {
        if (data is null) return null;

        var (_, parts) = TryRead(data);
        return parts is null ? null : new(parts);
    }

    [return: NotNullIfNotNull(nameof(item))]
    public static implicit operator string? (HostName? item) => item?.ToString();
        
    /// <inheritdoc cref="HostName"/>
    public HostName(UserName[] nameParts)
    {
        if (nameParts.Length == 0)
            throw new ArgumentException("Must have at-least one part");

        _parts = nameParts;
        _value = string.Join('.', _parts.Select(x => x.ToString()));
    }

    /// <inheritdoc cref="HostName"/>
    public HostName(string data)
    {
        var (error, parts) = TryRead(data);

        if (parts is null)
            throw new ArgumentException(error, nameof(data));

        _parts = parts;
        _value = string.Join('.', _parts.Select(x => x.ToString()));
    }

    public UserName[] NameParts
    {
        get => _parts;
        init =>
            _parts = value.Length == 0
                ? throw new ArgumentException("must have at-least one part.", nameof(NameParts))
                : value;
    }

    public virtual bool Equals(HostName? other)
        => other?.ToString() == ToString();

    public override int GetHashCode() => _value.GetHashCode();

    public override string ToString() => _value;
}
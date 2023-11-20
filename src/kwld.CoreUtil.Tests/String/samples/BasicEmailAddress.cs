#if NET7_0_OR_GREATER

using System;
using System.Diagnostics.CodeAnalysis;
using kwld.CoreUtil.Strings;

namespace kwld.CoreUtil.Tests.String.samples;

/// <summary>
/// A email address : <see cref="UserName"/>@<see cref="HostName"/>.
/// <list type="bullet">
/// <item>Must be of form <see cref="UserName"/>@<see cref="HostName"/></item>
/// <item>trimmed</item>
/// </list>
/// </summary>
/// <remarks>
/// Demo to show combining other <see cref="IDataString"/>'s to parse a string.
/// </remarks>
public record BasicEmailAddress(UserName User, HostName Host) : IDataString<BasicEmailAddress>
{
    private static (string? error, UserName? user, HostName? host) Read(string data)
    {
        var parts = data.Split('@');
        if (parts.Length != 2)
            return ("must be of form 'userName'@'hostName'}", null, null);

        var userName = UserName.TryParse(parts[0]);
        if (userName is null)
            return ("invalid user name", null, null);

        var hostName = HostName.TryParse(parts[1]);
        if (hostName is null)
             return ("invalid host name", null, null);

        return new(null, userName, hostName);
    }
    
    public static BasicEmailAddress?  TryParse(string? data)
    {
        if (data is null) return null;

        var (_, user, host) = Read(data);

        return user is not null && host is not null ? new(user, host) : null;
    }

    [return: NotNullIfNotNull(nameof(item))]
    public static implicit operator string? (BasicEmailAddress? item) => item?.ToString();

    /// <inheritdoc cref="BasicEmailAddress"/>
    public BasicEmailAddress(string data):this(null!, null!)
    {
        var (error, user, host) = Read(data);

        if (user is null || host is null)
            throw new ArgumentException(error, nameof(data));

        User = user;
        Host = host;
    }

    public override string ToString() => $"{User}@{Host}";
}
#endif
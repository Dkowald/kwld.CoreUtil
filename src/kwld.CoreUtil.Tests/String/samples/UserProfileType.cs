using System.Diagnostics.CodeAnalysis;
using kwld.CoreUtil.Strings;

namespace kwld.CoreUtil.Tests.String.samples;

/// <summary>
/// A user profile type
/// <list type="bullet">
/// <item>must be one of 'guest', 'registered' or 'admin'</item>
/// <item>trimmed and lower-cased</item>
/// </list>
/// </summary>
/// <remarks>
/// Demo DataString with OneOf constraint.
/// </remarks>
public record UserProfileType : IDataString
{
    private readonly string _value;
    
    public static UserProfileType? TryParse(string? data)
    {
        if (data is null) return null;
        
        if (data.Same(Guest)){ return Guest; }

        if (data.Same(Registered)){ return Registered; }
        
        if (data.Same(Admin)){ return Admin; }

        return null;
    }

    [return: NotNullIfNotNull(nameof(item))]
    public static implicit operator string?(UserProfileType? item) => item?.ToString();

    public static readonly UserProfileType Guest = new("guest");
    public static readonly UserProfileType Registered = new("registered");
    public static readonly UserProfileType Admin = new("admin");

    private UserProfileType(string data)
    {
        _value = data;
    }

    public override string ToString() => _value;
}
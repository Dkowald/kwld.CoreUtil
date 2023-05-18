#if NET7_0_OR_GREATER
namespace kwd.CoreUtil.Tests.String.samples;

/// <summary>
/// A simple user profile using a set of data strings
/// </summary>
public record UserProfile(UserName Name, BasicEmailAddress ContactEmail, UserProfileType Registration);

#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kwld.CoreUtil.Strings
{
    /// <summary>
    /// Extensions to build a string from parts.
    /// </summary>
    public static class StringBuildExtensions
    {
        /// <summary>
        /// Use a char as a separator to join string parts.
        /// </summary>
        public static string Combine(this char separator, IEnumerable<string> parts)
            => new StringBuilder().AppendJoin(separator, parts).ToString();

        /// <summary>
        /// Use a char as a separator to join string parts.
        /// </summary>
        public static string Combine(this char separator, params string[] parts)
            => new StringBuilder().AppendJoin(separator, parts).ToString();

        /// <summary>
        /// Use a string as a separator to join string parts.
        /// </summary>
        public static string Combine(this string separator, IEnumerable<string> parts)
            => new StringBuilder().AppendJoin(separator, parts).ToString();

        /// <summary>
        /// Use a string as a separator to join string parts.
        /// </summary>
        public static string Combine(this string separator, params string[] parts)
            => new StringBuilder().AppendJoin(separator, parts).ToString();

        /// <summary>
        /// AppendJoin up-to 4 char spans, with optional <paramref name="separator"/>
        /// </summary>
        public static string Combine(this char separator,
            ReadOnlySpan<char> span1,
            ReadOnlySpan<char> span2,
            ReadOnlySpan<char> span3 = default,
            ReadOnlySpan<char> span4 = default)
        {
            var build = new StringBuilder(
                    span1.Length + span2.Length +
                    span3.Length + span4.Length +
                    3);
            build.Append(span1);
            
            if (!span2.IsEmpty && build.Length > 0)
                build.Append(separator);
            build.Append(span2);

            if (!span3.IsEmpty && build.Length > 0)
                build.Append(separator);
            build.Append(span3);

            if (!span4.IsEmpty && build.Length > 0)
                build.Append(separator);
            build.Append(span4);

            return build.ToString();
        }

        /// <summary>
        /// AppendJoin up-to 4 char spans, with optional <paramref name="separator"/>
        /// </summary>
        public static string Combine(this string separator,
            ReadOnlySpan<char> span1,
            ReadOnlySpan<char> span2,
            ReadOnlySpan<char> span3 = default,
            ReadOnlySpan<char> span4 = default)
        {
            var build = new StringBuilder(
                span1.Length + span2.Length +
                span3.Length + span4.Length +
                3);
            build.Append(span1);

            if (!span2.IsEmpty && build.Length > 0)
                build.Append(separator);
            build.Append(span2);

            if (!span3.IsEmpty && build.Length > 0)
                build.Append(separator);
            build.Append(span3);

            if (!span4.IsEmpty && build.Length > 0)
                build.Append(separator);
            build.Append(span4);

            return build.ToString();
        }

        /// <summary>
        /// Convert to a ASCII only string, stripping non-ascii chars
        /// </summary>
        public static string AsASCII(this string lhs)
        {
            if (lhs.All(c => c <= 127))
                return lhs;

            return new string(lhs.Where(c => c <= 127).ToArray());
        }

        /// <summary>
        /// Ensures the string ends with <paramref name="postfix"/>,
        /// appending if need.
        /// </summary>
        public static string EnsurePostfix(this string target, char postfix)
            => target.EndsWith(postfix) ? target : target + postfix;

        /// <summary>
        /// Ensures the string starts with <paramref name="prefix"/>,
        /// pre-pending if need.
        /// </summary>
        public static string EnsurePrefix(this string target, char prefix)
            => target.StartsWith(prefix) ? target : prefix + target;

        /// <summary>
        /// Checks a string is not empty or whitespace, returning <paramref name="default"/>
        /// if it is.
        /// </summary>
        public static string DefaultTo(this string? source, string @default)
            => string.IsNullOrWhiteSpace(source) ? @default : source;
    }
}
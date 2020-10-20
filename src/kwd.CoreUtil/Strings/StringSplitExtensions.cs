using System;
using System.Collections.Generic;

namespace kwd.CoreUtil.Strings
{
    /// <summary>
    /// String extensions to find bits of strings.
    /// </summary>
    public static class StringSplitExtensions
    {
        /// <summary>
        /// Extract sequences of characters (words) wrapped in <paramref name="isDelimiter"/> character(s),
        /// see <see cref="NextWord"/>.
        /// </summary>
        public static IReadOnlyCollection<string> Words(this string lhs, Func<char, bool>? isDelimiter = null)
        {
            var result = new List<string>();
            var txt = lhs.AsSpan();
            while (txt.Length > 0)
            {
                var word = txt.NextWord(out txt, isDelimiter);

                if (word.Length > 0)
                    result.Add(word.ToString());
            }

            return result;
        }

        /// <summary>
        /// Finds the next word (returns empty span if not found).
        /// The <paramref name="isDelimiter"/> defaults to <see cref="Char.IsWhiteSpace(char)"/>
        /// </summary>
        public static ReadOnlySpan<char> NextWord(this ReadOnlySpan<char> txt, out ReadOnlySpan<char> rest, Func<char, bool>? isDelimiter = null)
        {
            isDelimiter ??= char.IsWhiteSpace;

            var start = -1;
            for (var i = 0; i < txt.Length; i++)
            {
                if (isDelimiter(txt[i]))
                {
                    if (start > -1)
                    {
                        rest = txt.Slice(i);
                        return txt.Slice(start, i - start);
                    }
                    continue;
                }

                if (start < 0) { start = i; }
            }

            rest = ReadOnlySpan<char>.Empty;

            return start > -1 ? txt.Slice(start) : ReadOnlySpan<char>.Empty;
        }

        /// <summary>
        /// Predicate based IndexOf; return -1 if not found.
        /// </summary>
        public static int IndexOf(this ReadOnlySpan<char> data, Func<char, bool> predicate)
        {
            for (var i = 0; i < data.Length; i++)
                if (predicate(data[i])) return i;

            return -1;
        }

        /// <summary>
        /// Trim leading prefix from string, if it has one.
        /// </summary>
        public static string TrimStart(this string lhs, string trimString, bool ignoreCase = true)
        {

            while (lhs.StartsWith(trimString, 
                ignoreCase?StringComparison.OrdinalIgnoreCase:StringComparison.CurrentCulture))
                lhs = lhs.Substring(trimString.Length);

            return lhs;
        }
         
        /// <summary>
        /// Trim trailing postfix from string, if it has one.
        /// </summary>
        public static string TrimEnd(this string lhs, string trimString, bool ignoreCase = true)
        {
            while (lhs.EndsWith(trimString,
                ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.CurrentCulture))
                lhs = lhs.Substring(0, lhs.Length-trimString.Length);

            return lhs;
        }

        /// <summary>
        /// Trim <paramref name="trimString"/> from the start and end of a string.
        /// </summary>
        public static string Trim(this string lhs, string trimString, bool ignoreCase)
            => lhs.TrimStart(trimString, ignoreCase).TrimEnd(trimString, ignoreCase);
    }
}
using System.Collections.Generic;

namespace LinzLinienEfa.Common.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveAnyPrefixes(this string str, IEnumerable<string> prefixes)
        {
            foreach (var prefix in prefixes)
            {
                if (str.Contains(prefix))
                {
                    return str.Substring(prefix.Length);
                }
            }
            return str;
        }

        public static string ReplaceAll(this string str, IDictionary<string, string> replacements)
        {
            foreach (var replacement in replacements)
            {
                str = str.Replace(replacement.Key, replacement.Value);
            }
            return str;
        }
    }
}
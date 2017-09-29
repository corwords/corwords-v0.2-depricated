using System.Linq;
using System.Text.RegularExpressions;

namespace Corwords.Web.Core.Extensions
{
    public static class StringExtensions
    {
        public static string SlugEncode(this string value)
        {
            return Regex.Replace(value.Replace(" ", "_"), @"[^A-Za-z0-9_]+", "");
        }

        public static string[] SplitAndTrim(this string value)
        {
            return value.Split(',').Select(s => s.Trim()).Where(s => s != string.Empty).ToArray();
        }

        public static string StripHtml(this string value)
        {
            return Regex.Replace(value ?? "", @"<[^>]+>|&nbsp;", "").Trim().Replace("  ", " ");
        }
    }
}
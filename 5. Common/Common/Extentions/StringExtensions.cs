using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Extentions
{
    public static class StringExtensions
    {
        public static string GetValueOrDefault(this string value, string defaultValue)
        {
            return value.IsNullOrWhiteSpace() ? defaultValue : value;
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
    }
}

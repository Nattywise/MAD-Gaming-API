using System;

namespace MadUtils.Extensions
{
    public static class Extensions
    {

        /// <summary>
        /// Return only the string trimmed and in lower case
        /// </summary>
        /// <returns></returns>
        public static string ToTrimLower(this string value)
        {
            return value == null ? "" : value.Trim().ToLower();
        }

        public static bool IsGuid(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            Guid guid;
            return Guid.TryParse(input, out guid);
        }
    }
}

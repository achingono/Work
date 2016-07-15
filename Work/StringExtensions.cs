using System;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
//using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using System.Collections.Generic;

namespace Work
{
    public static class StringExtensions
    {
        /// <summary>
        ///     Creates a <see cref="PluralizationService"/>
        ///     that applies language rules that correspond to the specified <see cref="CultureInfo"/>.
        /// </summary>
        //private static PluralizationService service = PluralizationService.CreateService(new CultureInfo("en-US"));

        /// <summary>
        /// Returns the plural form of the specified word
        /// </summary>
        /// <param name="value">The word to be made plural.</param>
        /// <returns>A System.String that is the plural form of the input parameter.</returns>
        //public static string ToPlural(this string value)
        //{
        //    return service.Pluralize(value);
        //}

        /// <summary>
        /// Returns the singular form of the specified word.
        /// </summary>
        /// <param name="value">The the word to be made singular.</param>
        /// <returns>The singular form of the input parameter.</returns>
        //public static string ToSingular(this string value)
        //{
        //    return service.Singularize(value);
        //}

        /// <summary>
        /// Formats a string to the name format
        /// </summary>
        /// <param name="input"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string ToName(this string input, int length = 50)
        {
            if (length < 1)
                throw new ArgumentException("Length can not less than or equal to zero.", "length");

            var options = RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase;

            input = Regex.Replace(input.Trim(), @"\s+(a|the)\s+", " ", options);
            input = Regex.Replace(input.Trim(), @"[^\w\d]", "-", options);
            input = Regex.Replace(input, @"-{2,}", "-", options);

            input = input.Trim(new char[] { '-' });

            // crop words at the end of the string if it is too long
            while (input.Length > length && length > 0 && input.LastIndexOf("-") > 0)
                input = input.Substring(0, input.LastIndexOf("-") - 1);

            input = input.Trim(new char[] { '-' });

            // if string is still too long, crop the additional characters
            if (input.Length > length && length > 0)
                input = input.Substring(0, length);

            return input.ToLower();
        }

        /// <summary>
        /// Formats a string to description format
        /// </summary>
        /// <param name="input"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string ToDescription(this string input, int length = 400)
        {
            if (length < 1)
                throw new ArgumentException("Length can not less than or equal to zero.", "length");

            input = input.StripHTML();
            if (input.Length > length)
            {
                input = input.Substring(0, length);

                int i = input.LastIndexOf(" ");
                if (i > 0) input = input.Substring(0, i - 1);
            }
            return input;
        }

        /// <summary>
        /// Determines whether two specified <see cref="String"/> objects have the same case-insensitive value.
        /// </summary>
        /// <param name="source">The first string to compare, or null.</param>
        /// <param name="compare">The second string to compare, or null.</param>
        /// <returns>true if the value of the two string contains the same case-insensitive value; otherwise false</returns>
        public static bool Matches(this string source, string compare)
        {
            return String.Equals(source, compare, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Determines whether two specified <see cref="String"/> objects have the same case-insensitive value ignoring whitespace.
        /// </summary>
        /// <param name="source">The first string to compare, or null.</param>
        /// <param name="compare">The second string to compare, or null.</param>
        /// <returns>true if the value of the two string contains the same case-insensitive value; otherwise false</returns>
        public static bool MatchesTrimmed(this string source, string compare)
        {
            return String.Equals(source.Trim(), compare.Trim(), StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Indicates whether the specified regular expression finds a match in the specified input string
        /// </summary>
        /// <param name="inputString">The string to search for a match.</param>
        /// <param name="matchPattern">The regular expression pattern to match.</param>
        /// <returns>true if the regular expression finds a match; otherwise, false.</returns>
        public static bool MatchesRegex(this string inputString, string matchPattern)
        {
            return Regex.IsMatch(inputString, matchPattern,
                RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
        }

        /// <summary>
        /// Strips the last specified chars from a string.
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <param name="removeFromEnd">The remove from end.</param>
        /// <returns></returns>
        public static string Chop(this string sourceString, int removeFromEnd)
        {
            string result = sourceString;
            if ((removeFromEnd > 0) && (sourceString.Length > removeFromEnd - 1))
                result = result.Remove(sourceString.Length - removeFromEnd, removeFromEnd);
            return result;
        }

        /// <summary>
        /// Removes the specified chars from the beginning of a string.
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <param name="removeFromBeginning">The remove from beginning.</param>
        /// <returns></returns>
        public static string Clip(this string sourceString, int removeFromBeginning)
        {
            string result = sourceString;
            if (sourceString.Length > removeFromBeginning)
                result = result.Remove(0, removeFromBeginning);
            return result;
        }

        /// <summary>
        /// Removes chars from the beginning of a string, up to the specified string
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <param name="removeUpTo">The remove up to.</param>
        /// <returns></returns>
        public static string Clip(this string sourceString, string removeUpTo)
        {
            int removeFromBeginning = sourceString.IndexOf(removeUpTo);
            string result = sourceString;

            if (sourceString.Length > removeFromBeginning && removeFromBeginning > 0)
                result = result.Remove(0, removeFromBeginning);

            return result;
        }

        /// <summary>
        /// Strips all HTML tags from a string and replaces the tags with the specified replacement
        /// </summary>
        /// <param name="htmlString">The HTML string.</param>
        /// <param name="htmlPlaceHolder">The HTML place holder.</param>
        /// <returns></returns>
        public static string StripHTML(this string htmlString, string htmlPlaceHolder = "")
        {
            //const string pattern = @"<(.|\n)*?>";
            //string sOut = Regex.Replace(htmlString, pattern, htmlPlaceHolder);
            string sOut = StripTags(htmlString, htmlPlaceHolder);

            sOut = sOut.Replace("&nbsp;", String.Empty);
            sOut = sOut.Replace("&amp;", "&");
            sOut = sOut.Replace("&gt;", ">");
            sOut = sOut.Replace("&lt;", "<");

            return sOut;
        }

        /// <summary>
        /// Removes formatting tags and returns plain text content
        /// </summary>
        /// <param name="input">The string with tags</param>
        /// <returns>Plain text</returns>
        public static string StripTags(this string input, string replacement = " ")
        {
            var options = RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase;
            return Regex.Replace(
                Regex.Replace(
                    Regex.Replace(input, "<[^>]+>", replacement, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                    @"\z", " ", options),
                @"\s{2,}", " ", options);
        }

        /// <summary>
        /// Convert a string to a Enum value
        /// </summary>
        /// <typeparam name="T">The type of enum</typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value)
        {
            T enumValue = default(T);
            Type enumType = typeof(T);
            foreach (FieldInfo field in enumType.GetFields())
            {
                if (field.Name.Matches(value))
                {
                    enumValue = (T)field.GetValue(null);
                }
            }

            return enumValue;
        }

        /// <summary>
        /// Computes the hash value for the specified string value using the specified salt
        /// </summary>
        /// <param name="value">The input to compute hash for</param>
        /// <param name="salt">The salt used to add complexity to the hash</param>
        /// <returns>The computed hash code</returns>
        public static byte[] Hash(this string value, string salt)
        {
            return Encoding.UTF8.GetBytes(value).Hash(Encoding.UTF8.GetBytes(salt));
        }

        /// <summary>
        /// Computes the hash value for the specified string value using the specified salt
        /// </summary>
        /// <param name="value">The input to compute hash for</param>
        /// <param name="salt">The salt used to add complexity to the hash</param>
        /// <returns>The computed hash code</returns>
        public static byte[] Hash(this string value, byte[] salt)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return new byte[] { };
            }
            return Encoding.UTF8.GetBytes(value).Hash(salt);
        }

        /// <summary>
        /// Convert a CamelCased string to seperate words
        /// </summary>
        /// <param name="value">The CameCased string</param>
        /// <returns>The string with seperate words</returns>
        public static string FromCamelCase(this string value)
        {
            return Regex.Replace(value, RegexPattern.CAMEL_CASE, "$1 ");
        }

        /// <summary>
        /// Convert a PascalCased string to seperate words
        /// </summary>
        /// <param name="value">The CameCased string</param>
        /// <returns>The string with seperate words</returns>
        public static string FromPascalCase(this string value)
        {
            return Regex.Replace(value, RegexPattern.PASCAL_CASE, "$1 ");
        }

        /// <summary>
        /// Makes the initial caps.
        /// </summary>
        /// <param name="value">The word.</param>
        /// <returns></returns>
        public static string ToTitleCase(this string value)
        {
            return System.Globalization.CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(value);
        }

        /// <summary>
        /// Get all the indices for the occurrences of the character
        /// </summary>
        /// <param name="value"></param>
        /// <param name="character"></param>
        /// <returns></returns>
        public static IEnumerable<int> IndicesOf(this string value, char character)
        {
            for (var i = 0; i < value.Length; i++)
            {
                if (value[i] == character)
                {
                    yield return i;
                }
            }

            yield return value.Length;
        }
    }
}
